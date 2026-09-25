using HomeLibrary.AL.DTOs;
using HomeLibrary.AL.Models;

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
    Task<OperationResult> CreateAsync(
        BookPostDto value, CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает список книг.
    /// </summary>
    /// <param name="page">Номер страницы.</param>
    /// <param name="perPage">Количество элементов на странице.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task<PagedListResponseDto<BookListDto>> GetListAsync(
        int page, 
        int perPage,
        string? searchString,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает книгу по идентификатору.
    /// </summary>
    /// <param name="id">Id.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task<BookDto> GetByIdAsync(
        long id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновляет книгу.
    /// </summary>
    /// <param name="id">Id.</param>
    /// <param name="value">Модель для обновления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task<OperationResult> UpdateAsync(
        long id, 
        BookPutDto value,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаляет книгу.
    /// </summary>
    /// <param name="id">Id.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}
