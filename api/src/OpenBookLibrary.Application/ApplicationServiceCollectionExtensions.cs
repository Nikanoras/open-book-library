using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OpenBookLibrary.Application.Repositories;
using OpenBookLibrary.Application.Services;

namespace OpenBookLibrary.Application;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IBookRepository, BookRepository>();
        services.AddSingleton<IBookService, BookService>();
        services.AddSingleton<IOpenLibraryClient, OpenLibraryClient>();
        services.AddSingleton<HttpClient>();
        services.AddValidatorsFromAssemblyContaining<IApplicationMarker>(ServiceLifetime.Singleton);
        services.AddMemoryCache();

        services.AddHttpClient<IOpenLibraryClient, OpenLibraryClient>(x =>
        {
            x.BaseAddress = new Uri("https://openlibrary.org/api/");
        });

        return services;
    }
}