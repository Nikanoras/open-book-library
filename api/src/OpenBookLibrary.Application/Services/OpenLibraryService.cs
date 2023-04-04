using System.Net.Http.Json;
using OpenBookLibrary.Application.Models;

namespace OpenBookLibrary.Application.Services;

public class OpenLibraryService : IOpenLibraryService
{
    private readonly HttpClient _httpClient;

    public OpenLibraryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<OpenLibraryBook?> GetBookByIsbn13(string isbn13)
    {
        using var response = await _httpClient.GetAsync($"https://openlibrary.org/isbn/{isbn13}.json");
        return await response.Content.ReadFromJsonAsync<OpenLibraryBook>();
    }
}