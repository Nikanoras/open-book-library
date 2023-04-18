using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OpenBookLibrary.Api.Swagger;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IHostEnvironment _environment;
    private readonly IConfiguration _config;
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider, IHostEnvironment environment, IConfiguration config)
    {
        _provider = provider;
        _environment = environment;
        _config = config;
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
            options.SwaggerDoc(
                description.GroupName,
                new OpenApiInfo
                {
                    Title = _environment.ApplicationName,
                    Version = description.ApiVersion.ToString()
                });

        const string securityDefinitionName = "oauth2";
        var tenantId = _config["Authentication:AzureAd:TenantId"]!;
        var readScope = _config["Authentication:AzureAd:ReadScope"]!;
        options.AddSecurityDefinition(securityDefinitionName, new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "OAuth2.0 Auth Code with PKCE",
            Name = securityDefinitionName,
            Type = SecuritySchemeType.OAuth2,
            Flows = new OpenApiOAuthFlows
            {
                AuthorizationCode = new OpenApiOAuthFlow
                {
                    AuthorizationUrl =
                        new Uri(
                            $"https://login.microsoftonline.com/{tenantId}/oauth2/v2.0/authorize"),
                    TokenUrl = new Uri(
                        $"https://login.microsoftonline.com/{tenantId}/oauth2/v2.0/token"),
                    Scopes = new Dictionary<string, string>
                        { { readScope, "read the api" } }
                }
            }
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = securityDefinitionName
                    }
                },
                Array.Empty<string>()
            }
        });
    }
}