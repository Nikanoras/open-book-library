using FluentValidation;
using OpenBookLibrary.Application.Models;

namespace OpenBookLibrary.Application.Validators;

public class BookValidator : AbstractValidator<Book>
{
    public BookValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Isbn13)
            .Must(x => x.StartsWith("978")).WithMessage("ISBN must start with 978")
            .Length(13);
    }
}