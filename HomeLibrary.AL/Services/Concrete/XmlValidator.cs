using System.Xml;

namespace HomeLibrary.AL.Services.Concrete;

/// <summary>
/// Сервис проверки валидности XML.
/// </summary>
public class XmlValidator : IXmlValidator
{
    /// <inheritdoc/>
    public bool Validate(string xml, out List<string> errors)
    {
        errors = [];
        if (string.IsNullOrWhiteSpace(xml))
        {
            errors.Add("XML is empty.");
            return false;
        }

        var settings = new XmlReaderSettings
        {
            DtdProcessing = DtdProcessing.Prohibit,
            XmlResolver = null,
        };

        var tmpErrors = new List<string>();
        settings.ValidationEventHandler += (sender, e) =>
        {
            tmpErrors.Add(
                $"Line {e.Exception.LineNumber}, pos {e.Exception.LinePosition}: {e.Message}");
        };
        errors.AddRange(tmpErrors);
        try
        {
            using var stringReader = new StringReader(xml);
            using var reader = XmlReader.Create(stringReader, settings);

            while (reader.Read()) { }

            return errors.Count == 0;
        }
        catch (XmlException ex)
        {
            errors.Add($"Line {ex.LineNumber}, pos {ex.LinePosition}: {ex.Message}");
            return false;
        }
        catch (Exception)
        {
            errors.Add("Unknown XML validation error");
            return false;
        }
    }
}
