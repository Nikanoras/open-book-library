using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace OpenBookLibrary.Application.Database;

public class ConnectionStringBuilder
{
    private readonly ConfigurationManager _config;

    public ConnectionStringBuilder(ConfigurationManager config)
    {
        _config = config;
    }

    public string Build(bool useDatabase = true)
    {
        var builder = new SqlConnectionStringBuilder
        {
            DataSource = _config["Database:DataSource"],
            UserID = _config["Database:UserID"],
            Password = _config["Database:Password"],
            TrustServerCertificate = true,
            IntegratedSecurity = false,
            Encrypt = true
        };

        if (useDatabase)
        {
            builder.InitialCatalog = _config["Database:InitialCatalog"];
        }

        return builder.ToString();
    }
}