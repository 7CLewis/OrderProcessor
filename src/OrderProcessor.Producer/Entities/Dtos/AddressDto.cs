namespace OrderProcessor.Producer.Entities.Dtos;

public sealed record AddressDto
{
    public required string Line1 { get; init; }
    public string? Line2 { get; init; }
    public required string City { get; init; }
    public required string StateOrProvince { get; init; }
    public required string PostalCode { get; init; }
    public required string Country { get; init; }
}
