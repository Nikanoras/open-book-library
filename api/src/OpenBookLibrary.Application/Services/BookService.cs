using FluentValidation;
using OpenBookLibrary.Application.Clients;
using OpenBookLibrary.Application.Models;
using OpenBookLibrary.Application.Repositories;

namespace OpenBookLibrary.Application.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IValidator<CreateBookModel> _bookValidator;
    private readonly IOpenLibraryClient _openLibraryClient;
    private readonly IValidator<GetAllBooksOptions> _optionsValidator;

    public BookService(IOpenLibraryClient openLibraryClient, IBookRepository bookRepository,
        IValidator<CreateBookModel> bookValidator, IValidator<GetAllBooksOptions> optionsValidator)
    {
        _openLibraryClient = openLibraryClient;
        _bookRepository = bookRepository;
        _bookValidator = bookValidator;
        _optionsValidator = optionsValidator;
    }

    public async Task<Book> CreateAsync(CreateBookModel request, CancellationToken token = default)
    {
        await _bookValidator.ValidateAndThrowAsync(request, token);

        var openBook = await _openLibraryClient.GetBookByIsbn13Async(request.Isbn13, token);

        if (openBook is null)
            throw new ArgumentException($"Book by provided ISBN13: {request.Isbn13} not found", nameof(request.Isbn13));

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

    public async Task<IEnumerable<Book>> GetAllAsync(GetAllBooksOptions options,
        CancellationToken token = default)
    {
        await _optionsValidator.ValidateAndThrowAsync(options, token);

        return await _bookRepository.GetAllAsync(options, token);
    }

    public async Task DeleteById(Guid id, CancellationToken token = default)
    {
        await _bookRepository.DeleteById(id, token);
    }

    public async Task DeleteByIsbn13(string isbn13, CancellationToken token = default)
    {
        await _bookRepository.DeleteByIsbn13(isbn13, token);
    }

    public Task<int> GetCountAsync(string? isbn13, string? title, string? author, CancellationToken token = default)
    {
        return _bookRepository.GetCountAsync(isbn13, title, author, token);
    }
}