using HomeLibrary.Domain.Entities;

namespace HomeLibrary.AL.Repositories;

/// <summary>
/// Репозиторий авторов.
/// </summary>
public interface IAuthorsRepository
{
    /// <summary>
    /// Возвращает автора по имени.
    /// </summary>
    /// <param name="name">Имя.</param>
    Task<Author?> FindByNameAsync(string name);

    /// <summary>
    /// Сохраняет автора.
    /// </summary>
    /// <param name="author">Модель автора.</param>
    Task<Author> CreateAsync(Author author);
}
