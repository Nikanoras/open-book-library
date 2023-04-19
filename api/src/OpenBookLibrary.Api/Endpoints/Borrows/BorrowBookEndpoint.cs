using OpenBookLibrary.Api.Auth;
using OpenBookLibrary.Application.Services;

namespace OpenBookLibrary.Api.Endpoints.Borrows;

public static class BorrowBookEndpoint
{
    public const string Name = "BorrowBook";

    public static IEndpointRouteBuilder MapBorrowBook(this IEndpointRouteBuilder app)
    {
        app.MapPost(ApiEndpoints.Books.BorrowBook,
            async (Guid id, HttpContext context, IBorrowService borrowService, CancellationToken token) =>
            {
                var userId = context.GetUserId();
                var result = await borrowService.BorrowBookAsync(id, userId!.Value, token);
                return result ? Results.Ok() : Results.NotFound();
            })
            .WithName(Name)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization();

        return app;
    }
}