using OpenBookLibrary.Application.Models;

namespace OpenBookLibrary.Application.Repositories;

public class BookRepository : IBookRepository
{
    private readonly List<Book> _books = new();

    public Task<bool> CreateAsync(Book book, CancellationToken token = default)
    {
        _books.Add(book);
        return Task.FromResult(true);
    }

    public Task<Book?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        var book = _books.SingleOrDefault(x => x.Id == id);
        return Task.FromResult(book);
    }

    public Task<IEnumerable<Book>> GetAllAsync(CancellationToken token = default)
    {
        return Task.FromResult(_books.AsEnumerable());
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
}