using FluentValidation;
using OpenBookLibrary.Application.Models;
using OpenBookLibrary.Application.Repositories;
using OpenBookLibrary.Contracts.Requests;

namespace OpenBookLibrary.Application.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IValidator<CreateBookRequest> _bookValidator;
    private readonly IOpenLibraryClient _openLibraryClient;

    public BookService(IOpenLibraryClient openLibraryClient, IBookRepository bookRepository,
        IValidator<CreateBookRequest> bookValidator)
    {
        _openLibraryClient = openLibraryClient;
        _bookRepository = bookRepository;
        _bookValidator = bookValidator;
    }

    public async Task<Book?> CreateAsync(CreateBookRequest request, CancellationToken token = default)
    {
        await _bookValidator.ValidateAndThrowAsync(request, token);

        var openBook = await _openLibraryClient.GetBookByIsbn13Async(request.Isbn13, token);

        if (openBook is null) return null;

        var book = new Book
        {
            Id = Guid.NewGuid(),
            Isbn13 = request.Isbn13,
            Title = openBook.Title,
            Authors = string.Join(", ", openBook.Authors.Select(x => x.Name))
        };

        await _bookRepository.CreateAsync(book, token);

        return book;
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