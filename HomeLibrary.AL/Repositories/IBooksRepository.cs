using HomeLibrary.AL.DTOs;
using HomeLibrary.Domain.Entities;

namespace HomeLibrary.AL.Repositories;

/// <summary>
/// Репозиторий книг.
/// </summary>
public interface IBooksRepository
{
    /// <summary>
    /// Создает книгу.
    /// </summary>
    /// <param name="newBook">Модель книги.</param>
    Task<Book> CreateAsync(Book newBook);

    /// <summary>
    /// Возвращает список книг.
    /// </summary>
    /// <param name="page">Номер страницы.</param>
    /// <param name="perPage">Количество элементов на странице.</param>
    /// <param name="searchString">Строка поиска.</param>
    Task<PagedListResponseDto<Book>> GetAllAsync(
        int page, int perPage, string? searchString);

    /// <summary>
    /// Возвращает книгу по Id.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    Task<Book?> GetByIdAsync(long id);

    /// <summary>
    /// Обновляет книгу.
    /// </summary>
    /// <param name="book">Модель книги.</param>
    Task UpdateAsync(Book book);
}
