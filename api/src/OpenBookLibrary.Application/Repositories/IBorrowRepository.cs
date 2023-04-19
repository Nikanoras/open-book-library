using OpenBookLibrary.Application.Models;

namespace OpenBookLibrary.Application.Repositories;

public interface IBorrowRepository
{
    Task<bool> BorrowBookAsync(Guid bookId, DateTime borrowed, Guid userId, CancellationToken token = default);
    Task<bool> IsBookBorrowedAsync(Guid bookId, CancellationToken token = default);
    Task<Borrow?> GetBorrowAsync(Guid bookId, Guid userId, CancellationToken token = default);
    Task<bool> ReturnBookAsync(Guid bookId, DateTime returned,Guid userId, CancellationToken token = default);
}