namespace OrderProcessor.Producer.Events;

public record TransportCompanyEmbedded(
    string Name,
    string ContactPhone,
    AddressEmbedded HeadquartersAddress
);