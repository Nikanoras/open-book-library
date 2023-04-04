using FluentValidation;
using OpenBookLibrary.Application.Models;
using OpenBookLibrary.Application.Repositories;

namespace OpenBookLibrary.Application.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IValidator<Book> _bookValidator;

    public BookService(IBookRepository bookRepository, IValidator<Book> bookValidator)
    {
        _bookRepository = bookRepository;
        _bookValidator = bookValidator;
    }

    public async Task<bool> CreateAsync(Book book, CancellationToken token = default)
    {
        await _bookValidator.ValidateAndThrowAsync(book, token);

        return await _bookRepository.CreateAsync(book, token);
    }

    public async Task<Book?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        return await _bookRepository.GetByIdAsync(id, token);
    }

    public async Task<IEnumerable<Book>> GetAllAsync(CancellationToken token = default)
    {
        return await _bookRepository.GetAllAsync(token);
    }

    public async Task DeleteById(Guid id, CancellationToken token = default)
    {
        await _bookRepository.DeleteById(id, token);
    }

    public async Task DeleteByIsbn13(string isbn13, CancellationToken token = default)
    {
        await _bookRepository.DeleteByIsbn13(isbn13, token);
    }
}