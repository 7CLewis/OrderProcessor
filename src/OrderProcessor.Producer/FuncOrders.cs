using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using OrderProcessor.Producer.Entities;
using OrderProcessor.Producer.Events;
using System.Text.Json;

namespace OrderProcessor.Producer;

public class FuncOrders : IEventPublisher<Order>
{
    private readonly EventHubProducerClient _producer;

    public FuncOrders(EventHubProducerClient producer)
    {
        _producer = producer;
    }

    public async Task PublishOrderCreatedAsync(Order order, CancellationToken ct = default)
    {
        var eventId = Guid.NewGuid();

        var orderEvent = new OrderEvent(
            eventId,
            DateTime.UtcNow,
            OrderEventType.Created.ToString(),
            "1.0",
            order.Id,
            order.CreatedUtc,
            new AddressEmbedded(
                order.SenderAddress.Line1,
                order.SenderAddress.Line2,
                order.SenderAddress.City,
                order.SenderAddress.StateOrProvince,
                order.SenderAddress.PostalCode,
                order.SenderAddress.Country
            ),
            new AddressEmbedded(
                order.ReceiverAddress.Line1,
                order.ReceiverAddress.Line2,
                order.ReceiverAddress.City,
                order.ReceiverAddress.StateOrProvince,
                order.ReceiverAddress.PostalCode,
                order.ReceiverAddress.Country
            ),
            order.Products.Select(p => new ProductEmbedded(
                p.Name,
                p.Description,
                p.PricePerUnit,
                p.Quantity
            )),
            new TransportCompanyEmbedded(
                order.TransportCompany.Name,
                order.TransportCompany.ContactPhone,
                new AddressEmbedded(
                    order.TransportCompany.HeadquartersAddress.Line1,
                    order.TransportCompany.HeadquartersAddress.Line2,
                    order.TransportCompany.HeadquartersAddress.City,
                    order.TransportCompany.HeadquartersAddress.StateOrProvince,
                    order.TransportCompany.HeadquartersAddress.PostalCode,
                    order.TransportCompany.HeadquartersAddress.Country
                )
            )
        );

        var json = JsonSerializer.Serialize(orderEvent);

        var eventData = new EventData(json);
        eventData.Properties["OrderEventType"] = OrderEventType.Created.ToString();
        eventData.Properties["OrderId"] = order.Id;

        using var batch = await _producer.CreateBatchAsync(ct);
        batch.TryAdd(eventData);

        await _producer.SendAsync(batch, ct);
    }
}
