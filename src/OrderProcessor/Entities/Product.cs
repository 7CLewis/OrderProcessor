namespace OrderProcessor.Producer.Entities;
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string UnitOfMeasure { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public string PricePerUnit { get; set; } = string.Empty;
    public DateTime CreatedDateTime { get; set; }
}
