namespace HomeLibrary.AL.Exceptions;

/// <summary>
/// Исключение, возникающее при попытке выполнить операцию с неверными входными данными.
/// </summary>
public class FailedPreconditionException : Exception
{
    public FailedPreconditionException(string message)
        : base(message)
    {

    }
}
