using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenBookLibrary.Application.Clients;
using OpenBookLibrary.Application.Database;
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

    public static IServiceCollection AddDatabase(this IServiceCollection services,
        ConfigurationManager config)
    {
        var connectionStringBuilder = new ConnectionStringBuilder(config);
        services.AddSingleton<IDbConnectionFactory>(_ =>
            new SqlConnectionFactory(connectionStringBuilder.Build()));
        services.AddSingleton<DbInitializer>();
        return services;
    }
}