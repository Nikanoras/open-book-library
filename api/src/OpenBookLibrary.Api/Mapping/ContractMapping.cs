using OpenBookLibrary.Application.Models;
using OpenBookLibrary.Contracts.Requests;
using OpenBookLibrary.Contracts.Responses;

namespace OpenBookLibrary.Api.Mapping;

public static class ContractMapping
{
    public static Book MapToBook(this CreateBookRequest request, string openBookTitle)
    {
        return new Book
        {
            Id = Guid.NewGuid(),
            Isbn13 = request.Isbn13,
            Title = openBookTitle
        };
    }

    public static BookResponse MapToResponse(this Book book)
    {
        return new BookResponse
        {
            Id = book.Id,
            Isbn13 = book.Isbn13,
            Title = book.Title
        };
    }

    public static IEnumerable<BookResponse> MapToResponse(this IEnumerable<Book> books)
    {
        return books.Select(MapToResponse);
    }
}