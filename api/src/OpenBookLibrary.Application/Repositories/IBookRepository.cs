using OpenBookLibrary.Application.Models;

namespace OpenBookLibrary.Application.Repositories;

public interface IBookRepository
{
    Task<bool> CreateAsync(Book book, CancellationToken token = default);
    Task<Book?> GetByIdAsync(Guid id, CancellationToken token = default);
    Task<IEnumerable<Book>> GetAllAsync(GetAllBooksOptions getAllBooksOptions, CancellationToken token = default);
    Task<bool> DeleteById(Guid id, CancellationToken token = default);
    Task<bool> DeleteByIsbn13(string isbn13, CancellationToken token = default);
    Task<int> GetCountAsync(string? isbn13, CancellationToken token = default);
}