using OpenBookLibrary.Api.Auth;
using OpenBookLibrary.Api.Mapping;
using OpenBookLibrary.Application.Services;
using OpenBookLibrary.Contracts.Responses;

namespace OpenBookLibrary.Api.Endpoints.Borrows;

public static class GetUserBorrowsEndpoint
{
    private const string Name = "GetUserBorrows";

    public static IEndpointRouteBuilder MapGetUserBorrows(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.Borrows.GetUserBorrows,
                async (HttpContext context, IBorrowService borrowService, CancellationToken token) =>
                {
                    var userId = context.GetUserId();
                    var borrows = await borrowService.GetBorrowsForUserAsync(userId!.Value, token);
                    var response = borrows.MapToResponse();
                    return TypedResults.Ok(response);
                })
            .WithName(Name)
            .Produces<IEnumerable<BorrowResponse>>()
            .RequireAuthorization();

        return app;
    }
}