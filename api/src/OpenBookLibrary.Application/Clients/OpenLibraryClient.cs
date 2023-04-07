using System.Net.Http.Json;
using Microsoft.Extensions.Caching.Memory;
using OpenBookLibrary.Application.Models;

namespace OpenBookLibrary.Application.Services;

public class OpenLibraryClient : IOpenLibraryClient
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _memoryCache;

    public OpenLibraryClient(HttpClient httpClient, IMemoryCache memoryCache)
    {
        _httpClient = httpClient;
        _memoryCache = memoryCache;
    }

    public async Task<OpenLibraryBook?> GetBookByIsbn13Async(string isbn13, CancellationToken token = default)
    {
        var exists = _memoryCache.TryGetValue(isbn13, out OpenLibraryBook? book);
        if (exists) return book;

        var response =
            await _httpClient.GetFromJsonAsync<Dictionary<string, OpenLibraryBook>>(
                $"books?bibkeys=ISBN:{isbn13}&format=json&jscmd=data", token);


        var result = response!.Any() ? response!.Values.First() : null;

        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(5));

        _memoryCache.Set(isbn13, result, cacheEntryOptions);

        return result;
    }
}