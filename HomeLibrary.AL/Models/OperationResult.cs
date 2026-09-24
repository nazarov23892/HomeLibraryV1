namespace HomeLibrary.AL.Models;

public class OperationResult
{
    public bool IsSuccess { get; init; }
    public List<ValidationError> Errors { get; init; } = new();

    public static OperationResult Ok() => new() { IsSuccess = true };

    public static OperationResult Fail(
        string PropertyName, IEnumerable<string> errorMessages)
        => new()
        {
            IsSuccess = false,
            Errors = errorMessages.Select(
            m => new ValidationError(PropertyName, m))
            .ToList()
        };

    public record ValidationError(string PropertyName, string Message);
}