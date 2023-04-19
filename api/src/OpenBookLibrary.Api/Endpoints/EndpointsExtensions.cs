using OpenBookLibrary.Api.Endpoints.Books;
using OpenBookLibrary.Api.Endpoints.Borrows;

namespace OpenBookLibrary.Api.Endpoints;

public static class EndpointsExtensions
{
    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapBookEndpoints();
        app.MapBorrowEndpoints();
        return app;
    }
}