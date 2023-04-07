using OpenBookLibrary.Api.Endpoints.Books;

namespace OpenBookLibrary.Api.Endpoints;

public static class EndpointsExtensions
{
    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapBookEndpoints();
        return app;
    }
}