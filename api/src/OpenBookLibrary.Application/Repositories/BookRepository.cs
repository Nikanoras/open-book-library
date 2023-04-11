using Dapper;
using OpenBookLibrary.Application.Database;
using OpenBookLibrary.Application.Models;

namespace OpenBookLibrary.Application.Repositories;

public class BookRepository : IBookRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;
    private readonly List<Book> _books = new();

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

        var orderClause = string.Empty;
        if (options.SortField is not null)
        {
            orderClause = $"""
            ORDER BY {options.SortField} {(options.SortOrder == SortOrder.Ascending ? "ASC" : "DESC")}
            """;
        }
        
        var result = await connection.QueryAsync<Book>(new CommandDefinition($"""
            SELECT * FROM Books
            WHERE (@Isbn13 IS NULL OR Isbn13 LIKE ('%' + @Isbn13 + '%'))
            AND (@Title IS NULL OR Title LIKE ('%' + @Title + '%'))
            AND (@Author IS NULL OR Authors LIKE ('%' + @Author + '%'))
            {orderClause}
        """, new { options.Isbn13, options.Title, options.Author, options.SortField }, cancellationToken: token));
        return result;
        

        // if (!string.IsNullOrEmpty(options.Isbn13)) result = result.Where(x => x.Isbn13 == options.Isbn13);
        //
        // if (options.SortField is not null)
        //     result = options.SortOrder == SortOrder.Ascending
        //         ? result.OrderBy(x => x.Title)
        //         : result.OrderByDescending(x => x.Title);
        //
        // return Task.FromResult(result.Skip((options.Page - 1) * options.PageSize).Take(options.PageSize));
    }

    public Task<bool> DeleteById(Guid id, CancellationToken token = default)
    {
        var removedCount = _books.RemoveAll(x => x.Id == id);

        return Task.FromResult(removedCount > 0);
    }

    public Task<bool> DeleteByIsbn13(string isbn13, CancellationToken token = default)
    {
        var removedCount = _books.RemoveAll(x => x.Isbn13 == isbn13);

        return Task.FromResult(removedCount > 0);
    }

    public Task<int> GetCountAsync(string? isbn13, CancellationToken token = default)
    {
        return Task.FromResult(_books.Count(x => x.Isbn13 == isbn13));
    }
}