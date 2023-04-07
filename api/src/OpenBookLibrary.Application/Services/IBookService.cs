using OpenBookLibrary.Application.Models;
using OpenBookLibrary.Contracts.Requests;

namespace OpenBookLibrary.Application.Services;

public interface IBookService
{
    Task<Book?> CreateAsync(CreateBookRequest request, CancellationToken token = default);
    Task<Book?> GetByIdAsync(Guid id, CancellationToken token = default);
    Task<IEnumerable<Book>> GetAllAsync(CancellationToken token = default);
    Task DeleteById(Guid id, CancellationToken token = default);
    Task DeleteByIsbn13(string isbn13, CancellationToken token = default);
}