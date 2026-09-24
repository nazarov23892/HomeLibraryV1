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
        BookPostDto value, CancellationToken cancellationToken)
    {
        var author = await _authorsRepository.FindByNameAsync(
            value.Author);

        author ??= await _authorsRepository.CreateAsync(
                new Author() { Name = value.Author });

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
    public async Task<PagedListResponseDto<BookListDto>> GetListAsync(
        int page, 
        int perPage,
        string? searchString,
        CancellationToken cancellationToken)
    {
        var pagedBooks = await _booksRepository.GetAllAsync(page, perPage, searchString);
        var dtos = pagedBooks.Items.Select(
            b => new BookListDto()
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author?.Name ?? string.Empty,
                PublishYear = b.PublishYear,
            }).ToList();
        var result = new PagedListResponseDto<BookListDto>()
        {
            Page = pagedBooks.Page,
            PerPage = pagedBooks.PerPage,
            Items = dtos,
            TotalCount = pagedBooks.TotalCount,
        };
        return result;
    }

    /// <inheritdoc/>
    public async Task<BookDto> GetByIdAsync(
        long id, CancellationToken cancellationToken)
    {
        var book = await _booksRepository.GetByIdAsync(id)
            ?? throw new NotFoundException($"Book {id} not found.");
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

    /// <inheritdoc/>
    public async Task UpdateAsync(
        long id, BookPutDto value, CancellationToken cancellationToken = default)
    {
        var author = await _authorsRepository.FindByNameAsync(
            value.Author);

        author ??= await _authorsRepository.CreateAsync(
                new Author() { Name = value.Author });

        var book = new Book()
        {
            Id = id,
            Title = value.Title,
            PublishYear = value.PublishYear,
            TableOfContents = value.TableOfContents,
            AuthorId = author.Id,
        };
        await _booksRepository.UpdateAsync(book);
    }
}
