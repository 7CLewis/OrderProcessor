namespace OrderProcessor.Producer.Events;

public record OrderFeedbackEvent(
    string OrderId,
    string CorrelationId,
    string Status,
    string Producer,
    DateTimeOffset FeedbackCreatedUtc,
    string? Message
);
