namespace OpenBookLibrary.Api.Endpoints.Books;

public static class BookEndpointExtensions
{
    public static IEndpointRouteBuilder MapBookEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGetBook();
        app.MapGetAllBooks();
        app.MapCreateBook();
        app.MapDeleteBook();

        return app;
    }
}