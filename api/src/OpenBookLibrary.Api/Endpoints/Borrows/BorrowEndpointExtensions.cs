namespace OpenBookLibrary.Api.Endpoints.Borrows;

public static class BorrowEndpointExtensions
{
    public static IEndpointRouteBuilder MapBorrowEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapBorrowBook();
        app.MapReturnBook();
        return app;
    }
}