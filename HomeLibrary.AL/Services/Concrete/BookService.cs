using HomeLibrary.AL.DTOs;
using HomeLibrary.AL.Exceptions;
using HomeLibrary.AL.Repositories;
using HomeLibrary.Domain.Entities;

namespace HomeLibrary.AL.Services.Concrete;

/// <summary>
/// Сервис работы с функционалом книг.
/// </summary>
public class BookService : IBookService
{
    readonly IBooksRepository _booksRepository;
    readonly IAuthorsRepository _authorsRepository;

    public BookService(
        IBooksRepository booksRepository,
        IAuthorsRepository authorsRepository)
    {
        _booksRepository = booksRepository;
        _authorsRepository = authorsRepository;
    }

    /// <inheritdoc/>
    public async Task<BookDto> CreateAsync(
        BookPutDto value, CancellationToken cancellationToken)
    {
        var author = await _authorsRepository.FindByNameAsync(
            value.Author)
            ?? throw new FailedPreconditionException(
                $"Author not found.");

        var book = new Book()
        {
            Title = value.Title,
            PublishYear = value.PublishYear,
            TableOfContents = value.TableOfContents,
            AuthorId = author.Id,
        };
        var createdBook = await _booksRepository.CreateAsync(book);
        var dto = new BookDto()
        {
            Id = createdBook.Id,
            Title = createdBook.Title,
            Author = createdBook.Author?.Name ?? string.Empty,
            PublishYear = createdBook.PublishYear,
            TableOfContents = createdBook.TableOfContents,
        };
        return dto;
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

    /// <inheritdoc/>
    public async Task<BookDto?> GetByIdAsync(
        long id, CancellationToken cancellationToken)
    {
        var book = await _booksRepository.GetByIdAsync(id);
        if (book == null)
            return null;
        var dto = new BookDto()
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author?.Name ?? string.Empty,
            PublishYear = book.PublishYear,
            TableOfContents = book.TableOfContents,
        };
        return dto;
    }
}
