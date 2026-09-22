namespace HomeLibrary.AL.DTOs;

/// <summary>
/// Списочная модель книги.
/// </summary>
public class BookListDto
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
}
