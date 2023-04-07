namespace OpenBookLibrary.Application.Models;

public class OpenLibraryResponse
{
    public required string Title { get; init; }

    public IEnumerable<OpenLibraryAuthorResponse> Authors { get; init; } =
        Enumerable.Empty<OpenLibraryAuthorResponse>();
}

public class OpenLibraryAuthorResponse
{
    public required string Name { get; init; }
}