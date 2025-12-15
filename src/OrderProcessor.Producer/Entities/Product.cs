namespace OrderProcessor.Producer.Entities;
public class Product
{
    // For EF Core
    public Product() { }

    public Product(
        string name,
        string description,
        string unitOfMeasure,
        int quantity,
        string pricePerUnit)
    {
        Id = 0;
        Name = name;
        Description = description;
        UnitOfMeasure = unitOfMeasure;
        Quantity = quantity;
        PricePerUnit = pricePerUnit;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string UnitOfMeasure { get; set; }
    public int Quantity { get; set; }
    public string PricePerUnit { get; set; }
    public DateTime CreatedDateTime { get; set; } = DateTime.UtcNow;
}
