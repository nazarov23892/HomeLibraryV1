using HomeLibrary.AL.DTOs;

namespace HomeLibrary.AL.Services;

/// <summary>
/// Сервис работы с функционалом книг.
/// </summary>
public interface IBookService
{
    /// <summary>
    /// Создает книгу.
    /// </summary>
    /// <param name="value">Модель для создания.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task<BookDto> CreateAsync(
        BookPutDto value, CancellationToken cancellationToken);

    /// <summary>
    /// Возвращает список книг.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task<IEnumerable<BookListDto>> GetListsAsync(
        CancellationToken cancellationToken);

    /// <summary>
    /// Возвращает книгу по идентификатору.
    /// </summary>
    /// <param name="id">Id.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task<BookDto?> GetByIdAsync(
        long id, CancellationToken cancellationToken);
}
