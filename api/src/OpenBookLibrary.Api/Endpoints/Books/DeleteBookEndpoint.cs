using OpenBookLibrary.Application.Services;

namespace OpenBookLibrary.Api.Endpoints.Books;

public static class DeleteBookEndpoint
{
    public const string Name = "DeleteBook";

    public static IEndpointRouteBuilder MapDeleteBook(this IEndpointRouteBuilder app)
    {
        app.MapDelete(ApiEndpoints.Books.Delete, async (string idOrIsbn13, IBookService bookService,
            CancellationToken token
        ) =>
        {
            if (Guid.TryParse(idOrIsbn13, out var id))
                await bookService.DeleteById(id, token);
            else
                await bookService.DeleteByIsbn13(idOrIsbn13, token);

            return Results.Ok();
        }).WithName(Name);
        return app;
    }
}