using FluentValidation;
using OpenBookLibrary.Application.Clients;
using OpenBookLibrary.Application.Models;

namespace OpenBookLibrary.Application.Validators;

public class BookValidator : AbstractValidator<CreateBookModel>
{
    private readonly IOpenLibraryClient _openLibraryClient;

    public BookValidator(IOpenLibraryClient openLibraryClient)
    {
        _openLibraryClient = openLibraryClient;
        RuleFor(x => x.Isbn13)
            .Cascade(CascadeMode.Stop)
            .Must(x => x.StartsWith("978")).WithMessage("ISBN must start with 978")
            .Length(13)
            .MustAsync(MustExist)
            .WithMessage(t => $"No book exists with provided ISBN13: {t.Isbn13}");
    }

    private async Task<bool> MustExist(CreateBookModel request, string isbn13, CancellationToken token = default)
    {
        var book = await _openLibraryClient.GetBookByIsbn13Async(isbn13, token);

        return book is not null;
    }
}