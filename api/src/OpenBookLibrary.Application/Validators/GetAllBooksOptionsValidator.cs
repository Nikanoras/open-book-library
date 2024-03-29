using FluentValidation;
using OpenBookLibrary.Application.Models;

namespace OpenBookLibrary.Application.Validators;

public class GetAllBooksOptionsValidator : AbstractValidator<GetAllBooksOptions>
{
    private static readonly string[] AcceptableSortFields =
    {
        "title", "isbn13", "authors"
    };

    public GetAllBooksOptionsValidator()
    {
        RuleFor(x => x.SortField)
            .Must(x => x is null || AcceptableSortFields.Contains(x, StringComparer.OrdinalIgnoreCase))
            .WithMessage("You can only sort by 'Title' or 'Isbn13' or 'Authors'");

        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 25)
            .WithMessage("You can get between 1 and 25 movies per page");
    }
}