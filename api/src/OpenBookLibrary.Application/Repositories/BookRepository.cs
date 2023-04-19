using Dapper;
using OpenBookLibrary.Application.Database;
using OpenBookLibrary.Application.Models;

namespace OpenBookLibrary.Application.Repositories;

public class BookRepository : IBookRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public BookRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<bool> CreateAsync(Book book, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);

        var result = await connection.ExecuteAsync(new CommandDefinition("""
            INSERT INTO Books Values (@Id, @Isbn13, @Title, @Authors) 
        """, book, cancellationToken: token));

        return result > 0;
    }

    public async Task<Book?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);

        var result = await connection.QuerySingleOrDefaultAsync<Book>(new CommandDefinition("""
            SELECT * FROM Books
            WHERE Id = @Id
        """, new { Id = id }, cancellationToken: token));
        return result;
    }

    public async Task<IEnumerable<Book>> GetAllAsync(GetAllBooksOptions options, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);

        var orderClause = options.SortField is not null
            ? $"""ORDER BY {options.SortField} {(options.SortOrder == SortOrder.Ascending ? "ASC" : "DESC")}"""
            : """ORDER BY Id ASC""";

        var result = await connection.QueryAsync<Book>(new CommandDefinition($"""
            SELECT * FROM Books
            WHERE (@Isbn13 IS NULL OR Isbn13 LIKE ('%' + @Isbn13 + '%'))
            AND (@Title IS NULL OR Title LIKE ('%' + @Title + '%'))
            AND (@Author IS NULL OR Authors LIKE ('%' + @Author + '%'))
            {orderClause}
            OFFSET @PageOffset ROWS
            FETCH NEXT @PageSize ROWS ONLY
            """,
            new
            {
                options.Isbn13,
                options.Title,
                options.Author,
                options.SortField,
                options.PageSize,
                PageOffset = (options.Page - 1) * options.PageSize
            }, cancellationToken: token));
        return result;
    }

    public async Task<bool> DeleteById(Guid id, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);

        var removedCount = await connection.ExecuteAsync(new CommandDefinition("""
            DELETE FROM Books WHERE Id = @Id
        """, new { Id = id }, cancellationToken: token));

        return removedCount > 0;
    }

    public async Task<bool> DeleteByIsbn13(string isbn13, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);

        var removedCount = await connection.ExecuteAsync(new CommandDefinition("""
            DELETE FROM Books WHERE Isbn13 = @Isbn13
        """, new { Isbn13 = isbn13 }, cancellationToken: token));

        return removedCount > 0;
    }

    public async Task<int> GetCountAsync(string? isbn13, string? title, string? author,
        CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);

        return await connection.QuerySingleAsync<int>(new CommandDefinition("""
            SELECT COUNT(Id) FROM Books
            WHERE (@Isbn13 IS NULL OR Isbn13 LIKE ('%' + @Isbn13 + '%'))
            AND (@Title IS NULL OR Title LIKE ('%' + @Title + '%'))
            AND (@Author IS NULL OR Authors LIKE ('%' + @Author + '%'))
        """, new { Isbn13 = isbn13, Title = title, Author = author }));
    }

    public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);

        return await connection.ExecuteScalarAsync<bool>(new CommandDefinition("""
            SELECT COUNT(Id) FROM Books
            WHERE Id = @Id
        """, new { Id = id }, cancellationToken: token));
    }
}