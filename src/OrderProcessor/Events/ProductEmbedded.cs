namespace OrderProcessor.Producer.Events;

public record ProductEmbedded(
    string Name,
    string Description,
    decimal Price,
    int Quantity
);