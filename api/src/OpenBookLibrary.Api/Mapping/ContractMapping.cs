using OpenBookLibrary.Application.Models;
using OpenBookLibrary.Contracts.Responses;

namespace OpenBookLibrary.Api.Mapping;

public static class ContractMapping
{
    public static BookResponse MapToResponse(this Book book)
    {
        return new BookResponse
        {
            Id = book.Id,
            Isbn13 = book.Isbn13,
            Title = book.Title,
            Authors = book.Authors
        };
    }

    public static IEnumerable<BookResponse> MapToResponse(this IEnumerable<Book> books)
    {
        return books.Select(MapToResponse);
    }
}