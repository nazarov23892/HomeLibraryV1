using System.Xml.Linq;

namespace HomeLibrary.AL.Services.Concrete;

/// <summary>
/// Конвертер XML.
/// </summary>
public class XmlConverter : IXmlConverter
{
    const string RootTagName = "content";
    const string RootTagOpening = $"<{RootTagName}>";
    const string RootTagClosing = $"</{RootTagName}>";

    /// <inheritdoc/>
    public string ConvertFromXml(string xml)
    {
        if (string.IsNullOrEmpty(xml))
            return string.Empty;
        var doc = XDocument.Parse(xml);
        var content = string.Concat(doc.Root!.Nodes().Select(n => n.ToString()));
        return content;
    }

    /// <inheritdoc/>
    public string ConvertToXml(string content)
        => $"{RootTagOpening}{Environment.NewLine}{content}{RootTagClosing}";
}
