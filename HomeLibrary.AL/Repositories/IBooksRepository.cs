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
    Task<IEnumerable<Book>> GetAllAsync();

    /// <summary>
    /// Возвращает книгу по Id.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    Task<Book?> GetByIdAsync(long id);
}
