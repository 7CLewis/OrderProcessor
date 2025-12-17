namespace OrderProcessor.Producer.Entities;

public class Order
{
    // For EF Core
    public Order() { }

    public Order(
        OrderStatus orderStatus,
        Address senderAddress,
        Address receiverAddress,
        IEnumerable<Product> products,
        TransportCompany transportCompany)
    {
        OrderStatus = orderStatus.ToString();
        SenderAddress = senderAddress;
        ReceiverAddress = receiverAddress;
        Products = products;
        TransportCompany = transportCompany;
    }

    public int Id { get; set; }
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
    public string OrderStatus { get; set; }
    public Address SenderAddress { get; set; }
    public Address ReceiverAddress { get; set; }
    public IEnumerable<Product> Products { get; set; }
    public TransportCompany TransportCompany { get; set; }
    public int TransportCompanyId { get; set; }
}

public enum OrderStatus
{
    Created,
    Processed,
    Shipped,
    Delivered,
    Cancelled
}
