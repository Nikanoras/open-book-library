namespace OpenBookLibrary.Contracts.Requests;

public class GetAllBooksRequest : PagedRequest
{
    public string? Title { get; set; }
    public string? Isbn13 { get; init; }
    public string? SortBy { get; init; }
}