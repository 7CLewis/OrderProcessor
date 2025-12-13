namespace OrderProcessor.Producer.Entities;
public class TransportCompany
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ContactPhone { get; set; } = string.Empty;
    public Address HeadquartersAddress { get; set; } = new Address();
}
