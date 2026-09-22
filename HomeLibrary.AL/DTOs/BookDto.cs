namespace HomeLibrary.AL.DTOs;

/// <summary>
/// Модель книги.
/// </summary>
public class BookDto
{
    public long Id { get; set; }

    /// <summary>
    /// Название.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Автор.
    /// </summary>
    public string Author { get; set; } = string.Empty;

    /// <summary>
    /// Год издания.
    /// </summary>
    public int PublishYear { get; set; }

    /// <summary>
    /// Оглавление.
    /// </summary>
    /// <remarks>Формат XML.</remarks>
    public string TableOfContents { get; set; } = string.Empty;
}
