namespace MovieTracker.Domain.Common.Results;

public sealed record Error(string Code, string Message)
{
    public static Error None { get; } = new(string.Empty, string.Empty);
    public static Error Exist { get; } = new("Duplicated", "The specified resource already exists.");
    public static Error NotFound { get; } = new("NotFound", "The specified resource was not found.");
    public static Error NotImplemented { get; } = new("NotImplemented", "Functionality is not implemented.");

    public override string ToString() => $"{Code}: {Message}";
}
