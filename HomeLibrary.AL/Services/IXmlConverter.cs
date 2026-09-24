namespace HomeLibrary.AL.Services;

/// <summary>
/// Конвертер XML.
/// </summary>
public interface IXmlConverter
{
    /// <summary>
    /// Возвращает XML-строку с указанными содержимым.
    /// </summary>
    /// <param name="content">Содержимое.</param>
    /// <returns>XML-строка.</returns>
    string ConvertToXml(string content);

    /// <summary>
    /// Возвращает содержимое.
    /// </summary>
    /// <param name="xml">XML-строка с содержимым.</param>
    /// <returns>Содерджимое.</returns>
    string ConvertFromXml(string xml);
}
