namespace OrderProcessor.Consumer.Events;

public record OrderFeedbackEvent(
    string OrderId,
    string CorrelationId,
    string Status,
    string Producer,
    DateTimeOffset FeedbackCreatedUtc,
    string? Message
);
