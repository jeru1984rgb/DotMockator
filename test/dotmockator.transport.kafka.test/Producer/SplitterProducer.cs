using DotMockator.Transport.Kafka.Producer;
using KafkaFlow;
using KafkaFlow.Producers;

namespace DotMockator.Transport.Kafka.Test.Producer;

public class SplitterProducer<TMock> : DotMockatorKafkaProducer<TMock>
{
    private IMessageProducer _producer2;
    
    public SplitterProducer(IMessageProducer producerOne, IMessageProducer producerTwo) : base(producerOne)
    {
        _producer2 = producerTwo;
    }

    protected override async  Task OnNextBatch(string topic, IEnumerable<TMock> message)
    {
        await Producer.BatchProduceAsync(message.Select(x => new BatchProduceItem(
            topic,
            Guid.NewGuid().ToString(),
            x,
            null
        )).ToList());

        await _producer2.BatchProduceAsync(message.Select(x => new BatchProduceItem(
            "BackupTopic",
            Guid.NewGuid().ToString(),
            x,
            null
        )).ToList());
    }
}