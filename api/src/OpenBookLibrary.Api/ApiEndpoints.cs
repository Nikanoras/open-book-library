namespace OpenBookLibrary.Api;

public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class Books
    {
        private const string Base = $"{ApiBase}/books";

        public const string Create = Base;
        public const string Get = $"{Base}/{{id:guid}}";
        public const string GetAll = Base;
        public const string Delete = $"{Base}/{{idOrIsbn13}}";

        public const string BorrowBook = $"{Base}/{{id:guid}}/borrows";
        public const string ReturnBook = $"{Base}/{{id:guid}}/borrows";
    }
}