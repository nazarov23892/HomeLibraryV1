using HomeLibrary.AL.DTOs;

namespace HomeLibrary.AL.Services.Concrete;

/// <summary>
/// Сервис работы с функционалом книг.
/// </summary>
public class BookService : IBookService
{
    /// <inheritdoc/>
    public Task<IEnumerable<BookListDto>> GetListsAsync(
        CancellationToken cancellationToken)
    {
        var list = new List<BookListDto>();
        for (var i = 0; i < 11; i++)
        {
            list.Add(
                new BookListDto()
                {
                    Id = 1 + i,
                    Title = $"book-{1 + i}",
                    Author = $"author-{1 + i}",
                    PublishYear = 2000 + i,
                }
            );
        }
        return Task.FromResult((IEnumerable<BookListDto>)list);
    }
}
