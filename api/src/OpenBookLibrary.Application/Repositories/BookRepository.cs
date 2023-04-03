using OpenBookLibrary.Application.Models;

namespace OpenBookLibrary.Application.Repositories;

public class BookRepository : IBookRepository
{
    private readonly List<Book> _books = new();
    public Task<bool> CreateAsync(Book book)
    {
        _books.Add(book);
        return Task.FromResult(true);
    }

    public Task<Book?> GetByIdAsync(Guid id)
    {
        var book = _books.SingleOrDefault(x => x.Id == id);
        return Task.FromResult(book);
    }
}