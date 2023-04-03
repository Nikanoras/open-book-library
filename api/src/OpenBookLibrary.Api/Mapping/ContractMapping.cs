using OpenBookLibrary.Application.Models;
using OpenBookLibrary.Contracts.Requests;
using OpenBookLibrary.Contracts.Responses;

namespace OpenBookLibrary.Api.Mapping;

public static class ContractMapping
{
    public static Book MapToBook(this CreateBookRequest request)
    {
        return new Book
        {
            Id = Guid.NewGuid()
        };
    }

    public static BookResponse MapToResponse(this Book book)
    {
        return new BookResponse
        {
            Id = book.Id
        };
    }
}