namespace OrderProcessor.Producer.Events;
public interface IEventPublisher<T>
    where T : class
{
    Task PublishOrderCreatedAsync(T entity, CancellationToken ct = default);
}
