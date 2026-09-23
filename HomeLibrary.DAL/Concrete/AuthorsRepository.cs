using Dapper;
using HomeLibrary.AL.Repositories;
using HomeLibrary.DAL.Models;
using HomeLibrary.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace HomeLibrary.DAL.Concrete;

/// <summary>
/// Репозиторий авторов.
/// </summary>
public class AuthorsRepository : IAuthorsRepository
{
    readonly DatabaseOptions _options;

    public AuthorsRepository(
        IOptions<DatabaseOptions> options)
    {
        _options = options.Value;
    }

    /// <inheritdoc/>>
    public async Task<Author> CreateAsync(Author author)
    {
        const string storedProcedure = "Authors_Create";
        await using var connection = new SqlConnection(_options.ConnectionString);
        var model = await connection.QuerySingleAsync<Author>(
            storedProcedure,
            param: new { author.Name },
            commandType: CommandType.StoredProcedure);
        return model;
    }

    /// <inheritdoc/>>
    public async Task<Author?> FindByNameAsync(
        string name)
    {
        const string storedProcedure = "Authors_FindByName";
        await using var connection = new SqlConnection(_options.ConnectionString);

        var model = await connection.QuerySingleOrDefaultAsync<Author>(
            storedProcedure,
            new { Name = name },
            commandType: CommandType.StoredProcedure);
        return model;
    }
}
