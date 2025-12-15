namespace OrderProcessor.Producer.Events;

public record OrderEvent(
    Guid EventId,
    DateTime EventTimestamp,
    string OrderEventType,
    string Version,
    int OrderId,
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