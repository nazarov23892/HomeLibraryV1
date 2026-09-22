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
    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        const string storedProcedure = "Books_Search";
        await using var connection = new SqlConnection(_options.ConnectionString);

        var models = await connection.QueryAsync<Book>(
            storedProcedure, 
            new { Query = string.Empty, },
            commandType: CommandType.StoredProcedure);
        return models;
    }
}
