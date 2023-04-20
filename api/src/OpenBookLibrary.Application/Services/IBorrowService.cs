using OpenBookLibrary.Application.Models;

namespace OpenBookLibrary.Application.Services;

public interface IBorrowService
{
    Task<bool> BorrowBookAsync(Guid bookId, Guid userId, CancellationToken token = default);
    Task<bool> ReturnBookAsync(Guid bookId, Guid userId, CancellationToken token = default);
    Task<IEnumerable<Borrow>> GetBorrowsForUserAsync(Guid userId, CancellationToken token = default);
}