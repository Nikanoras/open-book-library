using OpenBookLibrary.Api.Auth;
using OpenBookLibrary.Api.Mapping;
using OpenBookLibrary.Application.Services;

namespace OpenBookLibrary.Api.Endpoints.Books;

public static class GetBookEndpoint
{
    public const string Name = "GetBook";

    public static IEndpointRouteBuilder MapGetBook(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.Books.Get, async (
                Guid id, IBookService bookService, CancellationToken token
            ) =>
            {
                var book = await bookService.GetByIdAsync(id, token);
                if (book is null) return Results.NotFound();

                var response = book.MapToResponse();
                return TypedResults.Ok(response);
            }).WithName(Name)
            .RequireAuthorization(AuthConstants.TrustedMemberPolicyName);
        return app;
    }
}