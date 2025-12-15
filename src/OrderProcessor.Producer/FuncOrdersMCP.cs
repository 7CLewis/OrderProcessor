using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Mcp;
using Microsoft.EntityFrameworkCore;
using OrderProcessor.Producer.Entities;
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
    public async Task<string> CreateOrder(
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
            return $"Transport Company '{transportCompanyName}' not found.";
        }

        var products = await dbContext.Products
            .Where(p => productNames.Contains(p.Name))
            .ToListAsync();

        if (products.Count != productNames.Count())
        {
            return $"Some products not found: {string.Join(", ", productNames.Except(products.Select(p => p.Name))) }";
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
            return $"Failed to create order due to a database error: {ex.Message}";
        }

        try
        {
            // Create order event
            await eventPublisher.PublishOrderCreatedAsync(order);
        }
        catch(Exception ex) {
            return $"Order created but failed to publish order created event: {ex.Message}";
        }

        return "Order Created Successfully";
    }

    [Function(nameof(GetProducts))]
    public async Task<IEnumerable<Product>> GetProducts(
        [McpToolTrigger(GetProductsToolName, GetProductsToolDescription)]
            ToolInvocationContext _,
        [McpToolProperty(ProductNameFilterPropertyName, ProductNameFilterPropertyDescription, false)]
            string? productNameFilter
    )
    {
        var products = await dbContext.Products
            .Where(p => string.IsNullOrEmpty(productNameFilter) || p.Name.Contains(productNameFilter))
            .ToListAsync();
        return products;
    }

    [Function(nameof(GetTransportCompanies))]
    public async Task<IEnumerable<TransportCompany>> GetTransportCompanies(
        [McpToolTrigger(GetTransportCompaniesToolName, GetTransportCompaniesToolDescription)]
            ToolInvocationContext _,
        [McpToolProperty(TransportCompanyNameFilterPropertyName, TransportCompanyNameFilterPropertyDescription, false)]
            string? transportCompanyFilter
    )
    {
        var transportCompanies = await dbContext.TransportCompanies
            .Where(tc => string.IsNullOrEmpty(transportCompanyFilter) || tc.Name.Contains(transportCompanyFilter))
            .ToListAsync();
        return transportCompanies;
    }
}
