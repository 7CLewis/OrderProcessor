namespace OrderProcessor.Producer.Entities.Dtos;

public sealed record OrderDto
{
    public int Id { get; init; }
    public DateTime CreatedUtc { get; init; }
    public required string OrderStatus { get; init; }
    public required AddressDto SenderAddress { get; init; }
    public required AddressDto ReceiverAddress { get; init; }
    public required IEnumerable<ProductDto> Products { get; init; }
    public required TransportCompanyDto TransportCompany { get; init; }
}