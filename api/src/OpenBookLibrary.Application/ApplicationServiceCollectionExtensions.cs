using Microsoft.Extensions.DependencyInjection;
using OpenBookLibrary.Application.Repositories;

namespace OpenBookLibrary.Application;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IBookRepository, BookRepository>();
        return services;
    }
}