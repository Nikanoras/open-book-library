using OpenBookLibrary.Application.Models;

namespace OpenBookLibrary.Application.Services;

public interface IOpenLibraryService
{
    public Task<OpenLibraryBook?> GetBookByIsbn13(string isbn13);
}