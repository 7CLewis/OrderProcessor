namespace OrderProcessor.Producer.Events;

public record AddressEmbedded(
    string Line1,
    string? Line2,
    string City,
    string StateOrProvince,
    string PostalCode,
    string Country
);
