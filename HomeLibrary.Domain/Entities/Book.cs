namespace HomeLibrary.Domain.Entities;

public class Book
{
    public long Id { get; set; }

    /// <summary>
    /// Название.
    /// </summary>
    public required string Title { get; set; } 

    /// <summary>
    /// Идентификатор автора.
    /// </summary>
    public long AuthorId { get; set; }

    /// <summary>
    /// Автор.
    /// </summary>
    public Author? Author { get; set; }

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
