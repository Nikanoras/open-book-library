using OpenBookLibrary.Application.Models;

namespace OpenBookLibrary.Application.Services;

public interface IOpenLibraryClient
{
    public Task<OpenLibraryResponse?> GetBookByIsbn13Async(string isbn13, CancellationToken token = default);
}