using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using OrderProcessor.Consumer.Events;

namespace OrderProcessor.Consumer;
public class FuncOrderHandler
{
    private readonly EventHubProducerClient _feedbackProducer;
    private readonly ILogger<FuncOrderHandler> _logger;

    public FuncOrderHandler(
        EventHubProducerClient feedbackProducer,
        ILogger<FuncOrderHandler> logger)
    {
        _feedbackProducer = feedbackProducer;
        _logger = logger;
    }

    [Function(nameof(HandleOrderEvents))]
    public async Task HandleOrderEvents(
        [EventHubTrigger(
            eventHubName: "orders",
            Connection = "EventHubConnection")]
        EventData[] orderEvents)
    {
        foreach (var orderEvent in orderEvents)
        {
            var payload = orderEvent.EventBody.ToString();

            _logger.LogInformation(
                "Received order event: {Payload}",
                payload
            );

            var orderId = orderEvent.Properties.TryGetValue("OrderId", out var oid)
                ? oid?.ToString()
                : "unknown";

            var orderEventType = orderEvent.Properties.TryGetValue("OrderEventType", out var status)
                ? status?.ToString()
                : "unknown";

            if (orderEventType != "Created")
            {
                _logger.LogInformation(
                    "Ignoring order event of type {OrderEventType} for OrderId {OrderId}",
                    orderEventType,
                    orderId
                );
                return;
            }

            var correlationId = orderEvent.MessageId ?? Guid.NewGuid().ToString();

            // Simulate async event handling
            await Task.Delay(TimeSpan.FromSeconds(10));

            var feedbackEvent = new OrderFeedbackEvent(
                orderId!,
                correlationId,
                "Succeeded",
                nameof(FuncOrderHandler),
                DateTimeOffset.UtcNow,
                "Processed"
            );

            await SendFeedbackAsync(feedbackEvent);

            _logger.LogInformation(
                "Feedback sent for OrderId {OrderId}",
                orderId
            );
        }
    }

    private async Task SendFeedbackAsync(OrderFeedbackEvent feedback)
    {
        using var batch = await _feedbackProducer.CreateBatchAsync();

        var eventData = new EventData(
            BinaryData.FromObjectAsJson(feedback)
        );

        eventData.Properties["CorrelationId"] = feedback.CorrelationId;
        eventData.Properties["OrderId"] = feedback.OrderId;

        if (!batch.TryAdd(eventData))
        {
            throw new InvalidOperationException($"Failed to add feedback event {feedback.CorrelationId} for order {feedback.OrderId} to batch.");
        }

        await _feedbackProducer.SendAsync(batch);
    }
}
