using OpenBookLibrary.Api.Auth;
using OpenBookLibrary.Api.Mapping;
using OpenBookLibrary.Application.Services;
using OpenBookLibrary.Contracts.Requests;
using OpenBookLibrary.Contracts.Responses;

namespace OpenBookLibrary.Api.Endpoints.Books;

public static class GetAllBooksEndpoint
{
    private const string Name = "GetAllBooks";

    public static IEndpointRouteBuilder MapGetAllBooks(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.Books.GetAll, async ([AsParameters] GetAllBooksRequest request,
                IBookService bookService, CancellationToken token
            ) =>
            {
                var options = request.MapToOptions();

                var books = await bookService.GetAllAsync(options, token);
                var bookCount = await bookService.GetCountAsync(options.Isbn13, options.Title, options.Author, token);

                var booksResponse = books.MapToResponse(
                    request.Page.GetValueOrDefault(PagedRequest.DefaultPage),
                    request.PageSize.GetValueOrDefault(PagedRequest.DefaultPageSize),
                    bookCount);
                return TypedResults.Ok(booksResponse);
            })
            .WithName(Name)
            .Produces<BookResponse>()
            .RequireAuthorization(AuthConstants.TrustedMemberPolicyName);
        return app;
    }
}