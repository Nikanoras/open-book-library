using OpenBookLibrary.Api.Auth;
using OpenBookLibrary.Application.Services;

namespace OpenBookLibrary.Api.Endpoints.Books;

public static class DeleteBookEndpoint
{
    private const string Name = "DeleteBook";

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
            })
            .WithName(Name)
            .Produces(StatusCodes.Status200OK)
            .RequireAuthorization(AuthConstants.AdminUserPolicyName);
        return app;
    }
}