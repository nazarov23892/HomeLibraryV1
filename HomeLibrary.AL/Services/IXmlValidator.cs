namespace HomeLibrary.AL.Services;

/// <summary>
/// Сервис проверки валидности XML.
/// </summary>
public interface IXmlValidator
{
    /// <summary>
    /// Выполняет валидацию строки с XML содержимым.
    /// </summary>
    /// <param name="xml">Строка с документом XML.</param>
    /// <param name="errors">Список сообщений об ошибках.</param>
    /// <returns>true-валидация прошла успешо.</returns>
    bool Validate(string xml, out List<string> errors);
}
