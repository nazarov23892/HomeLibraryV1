using Dapper;
using HomeLibrary.AL.Repositories;
using HomeLibrary.DAL.Models;
using HomeLibrary.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

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
    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        const string sql =
    $"""
    select
    b.{nameof(Book.Id)}, b.{nameof(Book.Title)}, b.{nameof(Book.PublishYear)}, b.{nameof(Book.TableOfContents)}
    from Books b
    """;

        await using var connection = new SqlConnection(_options.ConnectionString);

        var models = await connection.QueryAsync<Book>(sql);
        return models;
    }
}
