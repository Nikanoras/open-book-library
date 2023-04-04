using OpenBookLibrary.Application.Models;

namespace OpenBookLibrary.Application.Services;

public interface IBookService
{
    Task<bool> CreateAsync(Book book, CancellationToken token = default);
    Task<Book?> GetByIdAsync(Guid id, CancellationToken token = default);
    Task<IEnumerable<Book>> GetAllAsync(CancellationToken token = default);
    Task DeleteById(Guid id, CancellationToken token = default);
    Task DeleteByIsbn13(string isbn13, CancellationToken token = default);
}