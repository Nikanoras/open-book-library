using OpenBookLibrary.Api.Auth;
using OpenBookLibrary.Application.Services;

namespace OpenBookLibrary.Api.Endpoints.Borrows;

public static class ReturnBookEndpoint
{
    private const string Name = "ReturnBook";
    
    public static IEndpointRouteBuilder MapReturnBook(this IEndpointRouteBuilder app)
    {
        app.MapPut(Api.ApiEndpoints.Books.ReturnBook,
                async (Guid id, HttpContext context, IBorrowService borrowService, CancellationToken token) =>
                {
                    var userId = context.GetUserId();
                    var result = await borrowService.ReturnBookAsync(id, userId!.Value, token);
                    return result ? Results.Ok() : Results.NotFound();
                })
            .WithName(Name)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization();
        return app;
    }
}