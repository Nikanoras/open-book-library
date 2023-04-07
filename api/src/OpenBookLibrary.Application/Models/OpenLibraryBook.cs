namespace OpenBookLibrary.Application.Models;

public class OpenLibraryBook
{
    public required string Title { get; init; }
    public IEnumerable<OpenLibraryAuthor> Authors { get; init; } = Enumerable.Empty<OpenLibraryAuthor>();
}

public class OpenLibraryAuthor
{
    public required string Name { get; init; }
}