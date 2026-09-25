using HomeLibrary.AL.DTOs;
using HomeLibrary.AL.Exceptions;
using HomeLibrary.AL.Models;
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
    readonly IXmlConverter _xmlConverter;
    readonly IXmlValidator _xmlValidator;

    public BookService(
        IBooksRepository booksRepository,
        IAuthorsRepository authorsRepository,
        IXmlValidator xmlValidator,
        IXmlConverter xmlConverter)
    {
        _booksRepository = booksRepository;
        _authorsRepository = authorsRepository;
        _xmlConverter = xmlConverter;
        _xmlValidator = xmlValidator;
    }

    /// <inheritdoc/>
    public async Task<OperationResult> CreateAsync(
        BookPostDto value, CancellationToken cancellationToken)
    {
        var tableOfContentsXml = _xmlConverter.ConvertToXml(value.TableOfContents);
        if (!_xmlValidator.Validate(
            tableOfContentsXml, out var validationErrorList))
        {
            validationErrorList = validationErrorList.Count > 0
                ? validationErrorList
                : ["Invalid xml content"];

            return OperationResult.Fail(
                nameof(value.TableOfContents), validationErrorList);
        }

        var author = await _authorsRepository.FindByNameAsync(
            value.Author);

        author ??= await _authorsRepository.CreateAsync(
                new Author() { Name = value.Author });

        var book = new Book()
        {
            Title = value.Title,
            PublishYear = value.PublishYear,
            TableOfContents = tableOfContentsXml,
            AuthorId = author.Id,
        };
        _ = await _booksRepository.CreateAsync(book);
        return OperationResult.Ok(); ;
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

        var tableOfContents = _xmlConverter.ConvertFromXml(book.TableOfContents);
        var dto = new BookDto()
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author?.Name ?? string.Empty,
            PublishYear = book.PublishYear,
            TableOfContents = tableOfContents,
        };
        return dto;
    }

    /// <inheritdoc/>
    public async Task<OperationResult> UpdateAsync(
        long id,
        BookPutDto value,
        CancellationToken cancellationToken = default)
    {
        var tableOfContentsXml = _xmlConverter.ConvertToXml(value.TableOfContents);
        if (!_xmlValidator.Validate(
            tableOfContentsXml, out var validationErrorList))
        {
            validationErrorList = validationErrorList.Count > 0
                ? validationErrorList
                : ["Invalid xml content"];

            return OperationResult.Fail(
                nameof(value.TableOfContents), validationErrorList);
        }

        var author = await _authorsRepository.FindByNameAsync(
            value.Author);

        author ??= await _authorsRepository.CreateAsync(
                new Author() { Name = value.Author });

        var book = new Book()
        {
            Id = id,
            Title = value.Title,
            PublishYear = value.PublishYear,
            TableOfContents = tableOfContentsXml,
            AuthorId = author.Id,
        };
        await _booksRepository.UpdateAsync(book);
        return OperationResult.Ok();
    }

    /// <inheritdoc/>
    public  Task DeleteAsync(long id, CancellationToken cancellationToken = default)
        => _booksRepository.DeleteAsync(id);
}
