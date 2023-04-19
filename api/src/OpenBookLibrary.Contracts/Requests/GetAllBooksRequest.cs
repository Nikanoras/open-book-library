namespace OpenBookLibrary.Contracts.Requests;

public class GetAllBooksRequest : PagedRequest
{
    public string? Title { get; set; }
    public string? Isbn13 { get; init; }
    public string? Author { get; set; }
    public string? SortBy { get; init; }
}