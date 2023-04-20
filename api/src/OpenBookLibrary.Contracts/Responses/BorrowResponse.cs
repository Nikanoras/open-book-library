namespace OpenBookLibrary.Contracts.Responses;

public class BorrowResponse
{
    public required Guid BookId { get; set; }
    public required DateTime Borrowed { get; set; }
    public DateTime? Returned { get; set; }
}