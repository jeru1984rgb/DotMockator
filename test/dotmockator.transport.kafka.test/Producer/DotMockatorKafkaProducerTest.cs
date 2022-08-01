using DotMockator.Core.Test.TestData.Complex;
using KafkaFlow;
using KafkaFlow.Producers;
using KafkaFlow.Serializer;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DotMockator.Transport.Kafka.Test.Producer;

public class DotMockatorKafkaProducerTest
{
    private const string TopicName = "sample-topic";
    private const string BackupTopicName = "backup-sample-topic";
    private const string simpleProducer = "SimpleProducer";
    private const string backupProducer = "BackupProducer";

    private async Task<IProducerAccessor> GetProducerResolver()
    {
        var services = new ServiceCollection();


        services.AddKafka(
            kafka => kafka
                .AddCluster(
                    cluster => cluster
                        .WithBrokers(new[] {"localhost:9092"})
                        .AddProducer(
                            simpleProducer,
                            producer => producer
                                .DefaultTopic(TopicName)
                                .AddMiddlewares(m => m.AddSerializer<JsonCoreSerializer>())
                        )
                        .AddProducer(
                            backupProducer,
                            producer => producer
                                .DefaultTopic(BackupTopicName)
                                .AddMiddlewares(m => m.AddSerializer<JsonCoreSerializer>())
                        )
                )
        );

        var provider = services.BuildServiceProvider();

        var bus = provider.CreateKafkaBus();

        await bus.StartAsync();

        return provider
            .GetRequiredService<IProducerAccessor>();
    }

    [Fact]
    public async Task CanProduce()
    {
        var producerResolver = await GetProducerResolver();
        var mockProducer =
            new SplitterProducer<ComplexMockDefinitionWithAttribute>(producerResolver.GetProducer(simpleProducer),
                producerResolver.GetProducer(backupProducer));

        await mockProducer.ProduceMessageBatch(TopicName, 32000, 100);
    }
}