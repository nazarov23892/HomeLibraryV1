using HomeLibrary.AL.Repositories;
using HomeLibrary.Domain.Entities;

namespace HomeLibrary.DAL.Concrete;

/// <summary>
/// Репозиторий книг.
/// </summary>
public class BooksRepository : IBooksRepository
{
    /// <inheritdoc/>>
    public Task<IEnumerable<Book>> GetAllAsync()
    {
        var list = new List<Book>();
        for (var i = 0; i < 11; i++)
        {
            list.Add(
                new Book()
                {
                    Id = 1 + i,
                    Title = $"book-{1 + i}",
                    Author = new Author()
                    {
                        Id = 1 + i,
                        Name = $"author-{1 + i}",
                    },
                    PublishYear = 2000 + i,
                }
            );
        }
        return Task.FromResult((IEnumerable<Book>)list);
    }
}
