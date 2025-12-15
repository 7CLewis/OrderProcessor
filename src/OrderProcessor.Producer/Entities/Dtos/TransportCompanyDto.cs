namespace OrderProcessor.Producer.Entities.Dtos;

public sealed record TransportCompanyDto
{
    public required string Name { get; init; }
    public required string ContactPhone { get; init; }
    public required AddressDto HeadquartersAddress { get; init; }
}

