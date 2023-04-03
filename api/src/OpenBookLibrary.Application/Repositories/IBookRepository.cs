using OpenBookLibrary.Application.Models;

namespace OpenBookLibrary.Application.Repositories;

public interface IBookRepository
{
    Task<bool> CreateAsync(Book book);
    Task<Book?> GetByIdAsync(Guid id);
}