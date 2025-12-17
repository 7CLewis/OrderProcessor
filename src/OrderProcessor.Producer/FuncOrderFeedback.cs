using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using OrderProcessor.Producer.Entities;
using OrderProcessor.Producer.Events;
using System.Text.Json;

namespace OrderProcessor.Producer;
public class FuncOrderFeedback
{
    private readonly EventHubProducerClient _feedbackProducer;
    private readonly OrderProcessorProducerDbContext _dbContext;
    private readonly ILogger<FuncOrderFeedback> _logger;

    public FuncOrderFeedback(
        EventHubProducerClient feedbackProducer,
        OrderProcessorProducerDbContext dbContext,
        ILogger<FuncOrderFeedback> logger)
    {
        _feedbackProducer = feedbackProducer;
        _dbContext = dbContext;
        _logger = logger;
    }

    [Function(nameof(HandleOrderFeedbackEvents))]
    public async Task HandleOrderFeedbackEvents(
        [EventHubTrigger(
            eventHubName: "orders-feedback",
            Connection = "EventHubConnection")]
        EventData[] events)
    {
        foreach (var eventData in events)
        {
            var payload = eventData.EventBody.ToString();

            _logger.LogInformation(
                "Received order feedback event: {payload}",
                payload
            );

            var orderFeedback = JsonSerializer.Deserialize<OrderFeedbackEvent>(
                eventData.EventBody.ToString()    
            );

            if (orderFeedback == null)
            {
                _logger.LogInformation(
                    "Unable to deserialize order feedback event: {payload}",
                    payload
                );
                return;
            }

            if (orderFeedback?.Status != "Succeeded")
            {
                _logger.LogInformation(
                    "Received order feedback event {messageId} with status {orderFeedbackStatus} for OrderId {orderId} that needs to be investigated",
                    orderFeedback.CorrelationId,
                    orderFeedback.Status,
                    orderFeedback.OrderId
                );
                return;
            }

            await UpdateOrderStatus(orderFeedback);

            _logger.LogInformation(
                "Updated order status for OrderId {orderId}",
                orderFeedback?.OrderId
            );
        }
    }

    private async Task UpdateOrderStatus(OrderFeedbackEvent orderFeedbackEvent)
    {
        var order = await _dbContext.Orders.FindAsync(int.Parse(orderFeedbackEvent.OrderId));

        if (order == null)
        {
            _logger.LogError(
                "Order with Id {orderId} not found in database",
                orderFeedbackEvent.OrderId
            );
            return;
        }

        order.OrderStatus = OrderStatus.Processed.ToString();

        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error updating order status for OrderId {orderId}",
                orderFeedbackEvent.OrderId
            );
        }
    }
}