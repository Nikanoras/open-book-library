using OpenBookLibrary.Application.Models;

namespace OpenBookLibrary.Application.Services;

public interface IBookService
{
    Task<Book> CreateAsync(CreateBookModel request, CancellationToken token = default);
    Task<Book?> GetByIdAsync(Guid id, CancellationToken token = default);
    Task<IEnumerable<Book>> GetAllAsync(GetAllBooksOptions options, CancellationToken token = default);
    Task DeleteById(Guid id, CancellationToken token = default);
    Task DeleteByIsbn13(string isbn13, CancellationToken token = default);
    Task<int> GetCountAsync(string? isbn13, string? title, string? author, CancellationToken token = default);
}