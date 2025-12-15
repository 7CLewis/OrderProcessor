namespace OrderProcessor.Producer.Entities.Dtos;

public sealed record ProductDto
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string UnitOfMeasure { get; init; }
    public required int Quantity { get; init; }
    public required string PricePerUnit { get; init; }
}