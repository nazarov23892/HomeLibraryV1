using HomeLibrary.Domain.Entities;

namespace HomeLibrary.AL.Repositories;

/// <summary>
/// Репозиторий книг.
/// </summary>
public interface IBooksRepository
{
    /// <summary>
    /// Возвращает список книг.
    /// </summary>
    Task<IEnumerable<Book>> GetAllAsync();
}
