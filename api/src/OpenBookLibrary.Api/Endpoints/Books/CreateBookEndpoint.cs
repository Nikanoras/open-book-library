using OpenBookLibrary.Api.Auth;
using OpenBookLibrary.Api.Mapping;
using OpenBookLibrary.Application.Services;
using OpenBookLibrary.Contracts.Requests;
using OpenBookLibrary.Contracts.Responses;

namespace OpenBookLibrary.Api.Endpoints.Books;

public static class CreateBookEndpoint
{
    private const string Name = "CreateBook";

    public static IEndpointRouteBuilder MapCreateBook(this IEndpointRouteBuilder app)
    {
        app.MapPost(ApiEndpoints.Books.Create, async (
                CreateBookRequest request, IBookService bookService, CancellationToken token
            ) =>
            {
                var model = request.MapToCreateBookModel();

                var book = await bookService.CreateAsync(model, token);

                var bookResponse = book.MapToResponse();

                return TypedResults.CreatedAtRoute(bookResponse, GetBookEndpoint.Name, new { id = book.Id });
            })
            .WithName(Name)
            .Produces<BookResponse>(StatusCodes.Status201Created)
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
            .RequireAuthorization(AuthConstants.AdminUserPolicyName);
        return app;
    }
}