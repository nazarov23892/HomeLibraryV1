using Dapper;
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

        var book = await connection.QuerySingleAsync<Book>(
            storedProcedure,
            new 
            {
                newBook.Title,
                newBook.PublishYear,
                newBook.TableOfContents,
                newBook.AuthorId,
            },
            commandType: CommandType.StoredProcedure);
        return book;
    }

    /// <inheritdoc/>>
    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        const string storedProcedure = "Books_Search";
        await using var connection = new SqlConnection(_options.ConnectionString);

        var models = await connection.QueryAsync<Book, Author, Book>(
            storedProcedure,
            map: (book, author) =>
            {
                book.Author = author;
                return book;
            },
            splitOn: nameof(Book.AuthorId),
            param: new { Query = string.Empty, },
            commandType: CommandType.StoredProcedure);
        return models;
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
}
