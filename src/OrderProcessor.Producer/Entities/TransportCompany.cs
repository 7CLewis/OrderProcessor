namespace OrderProcessor.Producer.Entities;
public class TransportCompany
{
    // For EF Core
    public TransportCompany() { }

    public TransportCompany(string name, string contactPhone, Address headquartersAddress)
    {
        Name = name;
        ContactPhone = contactPhone;
        HeadquartersAddress = headquartersAddress;  
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string ContactPhone { get; set; }
    public Address HeadquartersAddress { get; set; }
}
