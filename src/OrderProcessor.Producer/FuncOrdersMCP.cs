using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Mcp;
using Microsoft.EntityFrameworkCore;
using OrderProcessor.Producer.Entities;
using OrderProcessor.Producer.Entities.Dtos;
using OrderProcessor.Producer.Events;
using static OrderProcessor.Producer.OrdersMCPToolInfo;

namespace OrderProcessor.Producer;

public class FuncOrdersMCP(
    OrderProcessorProducerDbContext dbContext,
    IEventPublisher<Order> eventPublisher
)
{
    private readonly OrderProcessorProducerDbContext dbContext = dbContext;
    private readonly IEventPublisher<Order> eventPublisher = eventPublisher;

    [Function(nameof(CreateOrder))]
    public async Task<McpResponse<int>> CreateOrder(
        [McpToolTrigger(CreateOrderToolName, CreateOrderToolDescription)] 
            ToolInvocationContext _,
        [McpToolProperty(ProductNamesPropertyName, ProductNamesPropertyDescription, true)] 
            IEnumerable<string> productNames,
        [McpToolProperty(SenderAddressLine1PropertyName, SenderAddressLine1PropertyDescription, true)] 
            string senderAddressLine1,
        [McpToolProperty(SenderAddressLine2PropertyName, SenderAddressLine2PropertyDescription, false)]
            string senderAddressLine2,
        [McpToolProperty(SenderAddressCityPropertyName, SenderAddressCityPropertyDescription, true)]
            string senderAddressCity,
        [McpToolProperty(SenderAddressStatePropertyName, SenderAddressStatePropertyDescription, true)]
            string senderAddressState,
        [McpToolProperty(SenderAddressPostalCodePropertyName, SenderAddressPostalCodePropertyDescription, true)]
            string senderAddressPostalCode,
        [McpToolProperty(SenderAddressCountryPropertyName, SenderAddressCountryPropertyDescription, true)]
            string senderAddressCountry,
        [McpToolProperty(ReceiverAddressLine1PropertyName, ReceiverAddressLine1PropertyDescription, true)]
            string receiverAddressLine1,
        [McpToolProperty(ReceiverAddressLine2PropertyName, ReceiverAddressLine2PropertyDescription, false)]
            string receiverAddressLine2,
        [McpToolProperty(ReceiverAddressCityPropertyName, ReceiverAddressCityPropertyDescription, true)]
            string receiverAddressCity,
        [McpToolProperty(ReceiverAddressStatePropertyName, ReceiverAddressStatePropertyDescription, true)]
            string receiverAddressState,
        [McpToolProperty(ReceiverAddressPostalCodePropertyName, ReceiverAddressPostalCodePropertyDescription, true)]
            string receiverAddressPostalCode,
        [McpToolProperty(ReceiverAddressCountryPropertyName, ReceiverAddressCountryPropertyDescription, true)]
            string receiverAddressCountry,
        [McpToolProperty(TransportCompanyNamePropertyName, TransportCompanyNamePropertyDescription, true)]
            string transportCompanyName
    )
    {
        var transportCompany = await dbContext.TransportCompanies
            .FirstOrDefaultAsync(tc => tc.Name == transportCompanyName);

        if (transportCompany == null)
        {
            return new McpResponse<int>()
            {
                Success = false,
                Error = new McpError(
                    "TransportCompanyNotFound",
                    $"Transport Company '{transportCompanyName}' not found."
                )
            };
        }

        var products = await dbContext.Products
            .Where(p => productNames.Contains(p.Name))
            .ToListAsync();

        if (products.Count != productNames.Count())
        {
            var missingProducts = string.Join(", ", productNames.Except(products.Select(p => p.Name)));
            return new McpResponse<int>()
            {
                Success = false,
                Error = new McpError(
                    "SomeProductsNotFound",
                    $"One or more products were not found in the database: {missingProducts}"
                )
            };
        }

        var order = new Order(
            new Address(
                senderAddressLine1,
                senderAddressLine2,
                senderAddressCity,
                senderAddressState,
                senderAddressPostalCode,
                senderAddressCountry
            ),
            new Address(
                receiverAddressLine1,
                receiverAddressLine2,
                receiverAddressCity,
                receiverAddressState,
                receiverAddressPostalCode,
                receiverAddressCountry
            ),
            products,
            transportCompany
        );

        try
        {
            dbContext.Orders.Add(order);
            await dbContext.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            return new McpResponse<int>()
            {
                Success = false,
                Error = new McpError(
                    "DatabaseError",
                    "Failed to create order in the database.",
                    ex.Message
                )
            };
        }

        try
        {
            // Create order event
            await eventPublisher.PublishOrderCreatedAsync(order);
        }
        catch(Exception ex)
        {
            return new McpResponse<int>()
            {
                Success = false,
                Error = new McpError(
                    "EventPublishingError",
                    "Order created but failed to publish order created event.",
                    ex.Message
                )
            };
        }

        return new McpResponse<int>()
        {
            Success = true,
            Data = order.Id
        };
    }

    [Function(nameof(GetProducts))]
    public async Task<IEnumerable<ProductDto>> GetProducts(
        [McpToolTrigger(GetProductsToolName, GetProductsToolDescription)]
            ToolInvocationContext _,
        [McpToolProperty(ProductNameFilterPropertyName, ProductNameFilterPropertyDescription, false)]
            string? productNameFilter
    )
    {
        var products = await dbContext.Products
            .Where(p => string.IsNullOrEmpty(productNameFilter) || p.Name.Contains(productNameFilter))
            .Select(p => new ProductDto
            {
                Name = p.Name,
                Description = p.Description,
                UnitOfMeasure = p.UnitOfMeasure,
                Quantity = p.Quantity,
                PricePerUnit = p.PricePerUnit
            })
            .ToListAsync();
        return products;
    }

    [Function(nameof(GetTransportCompanies))]
    public async Task<IEnumerable<TransportCompanyDto>> GetTransportCompanies(
        [McpToolTrigger(GetTransportCompaniesToolName, GetTransportCompaniesToolDescription)]
            ToolInvocationContext _,
        [McpToolProperty(TransportCompanyNameFilterPropertyName, TransportCompanyNameFilterPropertyDescription, false)]
            string? transportCompanyFilter
    )
    {
        var transportCompanies = await dbContext.TransportCompanies
            .Where(tc => string.IsNullOrEmpty(transportCompanyFilter) || tc.Name.Contains(transportCompanyFilter))
            .Select(tc => new TransportCompanyDto
            {
                Name = tc.Name,
                ContactPhone = tc.ContactPhone,
                HeadquartersAddress = new AddressDto
                {
                    Line1 = tc.HeadquartersAddress.Line1,
                    Line2 = tc.HeadquartersAddress.Line2,
                    City = tc.HeadquartersAddress.City,
                    StateOrProvince = tc.HeadquartersAddress.StateOrProvince,
                    PostalCode = tc.HeadquartersAddress.PostalCode,
                    Country = tc.HeadquartersAddress.Country
                }
            })
            .ToListAsync();
        return transportCompanies;
    }
}
