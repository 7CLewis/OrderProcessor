namespace OrderProcessor.Producer.Entities;

public sealed record McpResponse<T>
{
    public required bool Success { get; init; }

    public T? Data { get; init; }

    public McpError? Error { get; init; }

    public static McpResponse<T> Ok(T data) =>
        new()
        {
            Success = true,
            Data = data
        };

    public static McpResponse<T> Fail(string code, string message) =>
        new()
        {
            Success = false,
            Error = new McpError(code, message)
        };
}

public sealed record McpError(
    string Code,
    string Message,
    object? Details = null
);

