using HomeLibrary.AL.DTOs;
using HomeLibrary.AL.Repositories;

namespace HomeLibrary.AL.Services.Concrete;

/// <summary>
/// Сервис работы с функционалом книг.
/// </summary>
public class BookService : IBookService
{
    readonly IBooksRepository _booksRepository;

    public BookService(IBooksRepository booksRepository)
    {
        _booksRepository = booksRepository;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<BookListDto>> GetListsAsync(
        CancellationToken cancellationToken)
    {
        var books = await _booksRepository.GetAllAsync();
        var dtos = books.Select(
            b => new BookListDto()
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author?.Name ?? string.Empty,
                PublishYear = b.PublishYear,
            }).ToList();    
        return dtos;
    }
}
