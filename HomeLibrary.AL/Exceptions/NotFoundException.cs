namespace HomeLibrary.AL.Exceptions;

/// <summary>
/// Исключение, возникающее при попытке получить несуществующую сущность.
/// </summary>
public class NotFoundException : Exception
{
    public NotFoundException(string message)
        : base(message)
    {
    }
}
