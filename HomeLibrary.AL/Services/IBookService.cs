using HomeLibrary.AL.DTOs;

namespace HomeLibrary.AL.Services;

/// <summary>
/// Сервис работы с функционалом книг.
/// </summary>
public interface IBookService
{
    /// <summary>
    /// Возвращает список книг.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task<IEnumerable<BookListDto>> GetListsAsync(
        CancellationToken cancellationToken);
}
