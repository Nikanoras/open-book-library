using FluentValidation;
using FluentValidation.Results;
using OpenBookLibrary.Application.Models;
using OpenBookLibrary.Application.Repositories;

namespace OpenBookLibrary.Application.Services;

public class BorrowService : IBorrowService
{
    private readonly IBookRepository _bookRepository;
    private readonly IBorrowRepository _borrowRepository;

    public BorrowService(IBookRepository bookRepository, IBorrowRepository borrowRepository)
    {
        _bookRepository = bookRepository;
        _borrowRepository = borrowRepository;
    }

    public async Task<bool> BorrowBookAsync(Guid bookId, Guid userId, CancellationToken token = default)
    {
        var bookExists = await _bookRepository.ExistsByIdAsync(bookId, token);

        if (!bookExists) return false;

        var isBorrowed = await _borrowRepository.IsBookBorrowedAsync(bookId, token);

        if (isBorrowed)
            throw new ValidationException(new[]
            {
                new ValidationFailure
                {
                    PropertyName = "BookId",
                    ErrorMessage = "Book is already borrowed"
                }
            });

        return await _borrowRepository.BorrowBookAsync(bookId, DateTime.UtcNow, userId, token);
    }

    public async Task<bool> ReturnBookAsync(Guid bookId, Guid userId, CancellationToken token = default)
    {
        var bookExists = await _bookRepository.ExistsByIdAsync(bookId, token);

        if (!bookExists) return false;

        var borrow = await _borrowRepository.GetActiveBorrowAsync(bookId, userId, token);

        if (borrow is null)
            throw new ValidationException(new[]
            {
                new ValidationFailure
                {
                    PropertyName = "Borrow",
                    ErrorMessage = "This book is not borrowed or not borrowed by this user"
                }
            });

        return await _borrowRepository.ReturnBookAsync(bookId, DateTime.UtcNow, userId, token);
    }

    public Task<IEnumerable<Borrow>> GetBorrowsForUserAsync(Guid userId, CancellationToken token = default)
    {
        return _borrowRepository.GetBorrowsForUserAsync(userId, token);
    }
}