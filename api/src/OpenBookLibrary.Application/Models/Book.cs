namespace OpenBookLibrary.Application.Models;

public class Book
{
    public required Guid Id { get; init; }
    public required string Isbn13 { get; set; }
    public required string Title { get; set; }
}