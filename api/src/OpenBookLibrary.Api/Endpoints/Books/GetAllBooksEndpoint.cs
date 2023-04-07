using OpenBookLibrary.Api.Mapping;
using OpenBookLibrary.Application.Services;

namespace OpenBookLibrary.Api.Endpoints.Books;

public static class GetAllBooksEndpoint
{
    public const string Name = "GetAllBooks";

    public static IEndpointRouteBuilder MapGetAllBooks(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.Books.GetAll, async (IBookService bookService, CancellationToken token
        ) =>
        {
            var books = await bookService.GetAllAsync(token);

            var booksResponse = books.MapToResponse();
            return TypedResults.Ok(booksResponse);
        }).WithName(Name);
        return app;
    }
}