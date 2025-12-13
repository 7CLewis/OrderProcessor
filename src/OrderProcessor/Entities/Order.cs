namespace OrderProcessor.Producer.Entities;

public class Order
{
    public int Id { get; set; }
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
    public Address SenderAddress { get; set; } = new Address();
    public Address ReceiverAddress { get; set; } = new Address();
    public List<Product> Products { get; set; } = [];
    public TransportCompany TransportCompany { get; set; } = new TransportCompany();
    public int TransportCompanyId { get; set; }
}

