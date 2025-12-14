namespace OrderProcessor.Producer.Events;

public record OrderEvent(
    Guid EventId,
    DateTime EventTimestamp,
    OrderEventType EventType,
    string Version,
    Guid OrderId,
    DateTime CreatedUtc,
    AddressEmbedded SenderAddress,
    AddressEmbedded ReceiverAddress,
    IEnumerable<ProductEmbedded> Products,
    TransportCompanyEmbedded TransportCompany
);

public enum OrderEventType
{
    Created,
    Updated
}