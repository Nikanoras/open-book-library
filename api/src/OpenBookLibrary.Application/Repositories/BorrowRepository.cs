using Dapper;
using OpenBookLibrary.Application.Database;
using OpenBookLibrary.Application.Models;

namespace OpenBookLibrary.Application.Repositories;

public class BorrowRepository : IBorrowRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public BorrowRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }
    
    public async Task<bool> BorrowBookAsync(Guid bookId, DateTime borrowed, Guid userId, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
        var result = await connection.ExecuteAsync(new CommandDefinition("""
            INSERT INTO Borrows(UserId, Borrowed, BookId)
            VALUES (@UserId, @Borrowed, @BookId)
        """, new { UserId = userId, Borrowed = borrowed, BookId = bookId }, cancellationToken: token));

        return result > 0;
    }

    public async Task<bool> IsBookBorrowedAsync(Guid bookId, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
        return await connection.ExecuteScalarAsync<bool>(new CommandDefinition("""
            SELECT COUNT(1) FROM Borrows WHERE BookId = @BookId AND Returned IS NULL
        """, new { BookId = bookId }, cancellationToken: token));
    }

    public async Task<Borrow?> GetBorrowAsync(Guid bookId, Guid userId, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
        return await connection.QuerySingleOrDefaultAsync<Borrow>(new CommandDefinition("""
            SELECT * FROM Borrows WHERE BookId = @BookId AND UserId = @UserId
        """, new { BookId = bookId, UserId = userId }, cancellationToken: token));
    }

    public async Task<bool> ReturnBookAsync(Guid bookId, DateTime returned, Guid userId, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
        var result = await connection.ExecuteAsync(new CommandDefinition("""
            UPDATE Borrows SET Returned = @Returned WHERE BookId = @BookId AND UserId = @UserId
        """, new { Returned = returned, BookId = bookId, UserId = userId }, cancellationToken: token));

        return result > 0;
    }
}