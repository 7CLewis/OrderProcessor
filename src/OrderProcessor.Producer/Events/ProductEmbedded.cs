namespace OrderProcessor.Producer.Events;

public record ProductEmbedded(
    string Name,
    string Description,
    string PricePerUnit,
    int Quantity
);