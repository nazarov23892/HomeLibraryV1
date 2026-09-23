using Dapper;
using HomeLibrary.AL.DTOs;
using HomeLibrary.AL.Repositories;
using HomeLibrary.DAL.Models;
using HomeLibrary.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace HomeLibrary.DAL.Concrete;

/// <summary>
/// Репозиторий книг.
/// </summary>
public class BooksRepository : IBooksRepository
{
    readonly DatabaseOptions _options;

    public BooksRepository(
        IOptions<DatabaseOptions> options)
    {
        _options = options.Value;
    }

    /// <inheritdoc/>>
    public async Task<Book> CreateAsync(Book newBook)
    {
        const string storedProcedure = "Books_Create";
        await using var connection = new SqlConnection(_options.ConnectionString);

        var results = await connection.QueryAsync<Book, Author, Book>(
            storedProcedure,
            map: (book, author) =>
            {
                book.Author = author;
                return book;
            },
            splitOn: nameof(Book.AuthorId),
            param: new
            {
                newBook.Title,
                newBook.PublishYear,
                newBook.TableOfContents,
                newBook.AuthorId,
            },
            commandType: CommandType.StoredProcedure);
        var book = results.Single();
        return book;
    }

    /// <inheritdoc/>>
    public async Task<PagedListResponseDto<Book>> GetAllAsync(int page, int perPage, string? searchString)
    {
        const string storedProcedure = "Books_Search";

        var parameters = new DynamicParameters();
        parameters.Add("@PageStartsZero", page - 1);
        parameters.Add("@PerPage", 10);
        parameters.Add("@TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
        parameters.Add("@SearchString", searchString);

        await using var connection = new SqlConnection(_options.ConnectionString);

        var models = await connection.QueryAsync<Book, Author, Book>(
            storedProcedure,
            map: (book, author) =>
            {
                book.Author = author;
                return book;
            },
            splitOn: nameof(Book.AuthorId),
            param: parameters,
            commandType: CommandType.StoredProcedure);

        var result = new PagedListResponseDto<Book>()
        {
            Items = models,
            Page = page,
            PerPage = perPage,
            TotalCount = parameters.Get<int>("@TotalCount"),
        };
        return result;
    }

    /// <inheritdoc/>>
    public async Task<Book?> GetByIdAsync(long id)
    {
        const string storedProcedure = "Books_GetById";
        await using var connection = new SqlConnection(_options.ConnectionString);

        var records = await connection.QueryAsync<Book, Author, Book>(
            storedProcedure,
            map: (book, author) =>
            {
                book.Author = author;
                return book;
            },
            splitOn: nameof(Book.AuthorId),
            param: new { Id = id },
            commandType: CommandType.StoredProcedure);
        var book = records.FirstOrDefault();
        return book;
    }

    /// <inheritdoc/>>
    public async Task UpdateAsync(Book book)
    {
        const string storedProcedure = "Books_Update";
        await using var connection = new SqlConnection(_options.ConnectionString);

        var result = await connection.QuerySingleAsync(
            storedProcedure,
            param: new
            {
                book.Id,
                book.Title,
                book.PublishYear,
                book.TableOfContents,
                book.AuthorId,
            },
            commandType: CommandType.StoredProcedure);
    }
}
