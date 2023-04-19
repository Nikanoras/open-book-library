namespace OpenBookLibrary.Contracts.Requests;

public class CreateBookRequest
{
    public required string Isbn13 { get; init; }
}