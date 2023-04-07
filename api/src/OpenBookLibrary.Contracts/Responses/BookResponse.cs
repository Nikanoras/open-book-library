namespace OpenBookLibrary.Contracts.Responses;

public class BookResponse
{
    public required Guid Id { get; init; }
    public required string Isbn13 { get; set; }
    public required string Title { get; set; }
    public required string Authors { get; set; }
}