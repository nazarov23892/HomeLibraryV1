using HomeLibrary.Domain;
using System.ComponentModel.DataAnnotations;

namespace HomeLibrary.AL.DTOs;

/// <summary>
/// Модель книги для обновления.
/// </summary>
public class BookPutDto
{
    /// <summary>
    /// Название.
    /// </summary>
    [Required]
    [MinLength(AppConstants.BookTitleMinLength)]
    [MaxLength(AppConstants.BookTitleMaxLength)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Автор.
    /// </summary>
    [Required]
    [MinLength(AppConstants.AuthorNameMinLength)]
    [MaxLength(AppConstants.AuthorNameMaxLength)]
    public string Author { get; set; } = string.Empty;

    /// <summary>
    /// Год издания.
    /// </summary>
    [Range(AppConstants.PublishYearMinValue, AppConstants.PublishYearMaxValue)]
    public int PublishYear { get; set; }

    /// <summary>
    /// Оглавление.
    /// </summary>
    /// <remarks>Формат XML.</remarks>
    [Required]
    [MaxLength(AppConstants.TableOfContentsMaxLength)]
    public string TableOfContents { get; set; } = string.Empty;
}
