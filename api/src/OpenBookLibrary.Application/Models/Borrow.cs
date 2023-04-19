namespace OpenBookLibrary.Application.Models;

public class Borrow
{
    public required Guid BookId { get; set; }
    public required DateTime Borrowed { get; set; }
    public DateTime? Returned { get; set; }
}