using System.Reactive.Linq;
using DotMockator.Core.Definitions;
using DotMockator.Core.Generator;
using KafkaFlow;
using KafkaFlow.Producers;

namespace DotMockator.Transport.Kafka.Producer;

public class DotMockatorKafkaProducer<TMock>
{
    protected readonly IMessageProducer Producer;
    public Definition Definition { get; }

    public DotMockatorKafkaProducer(IMessageProducer producer)
    {
        Definition = DefinitionExtractor.ExtractDefinition(typeof(TMock));
        Producer = producer;
    }

    public async Task ProduceMessages(string topic, int amount)
    {
        var observable = MockatorGenerator.GenerateObservable<TMock>(amount, Definition);

        async void OnNext(TMock message)
        {
            await Producer.ProduceAsync(topic, Guid.NewGuid().ToString(), message);
        }

        observable.Subscribe(OnNext);
    }

    public async Task ProduceMessageBatch(string topic, int amount, int batchSize)
    {
        var observable = MockatorGenerator.GenerateObservable<TMock>(amount, Definition);
        var batchedObservable = observable.Buffer(batchSize);

        async void OnNext(IList<TMock> next) => await OnNextBatch(topic, next);

        batchedObservable.Subscribe(OnNext);
    }

    protected virtual async Task OnNextBatch(string topic,
        IEnumerable<TMock> message)
    {
        var result = await Producer.BatchProduceAsync(message.Select(x => new BatchProduceItem(
            topic,
            Guid.NewGuid().ToString(),
            x,
            null
        )).ToList());
    }
}