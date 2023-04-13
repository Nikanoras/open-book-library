using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace OpenBookLibrary.Application.Database;

public class DbInitializer
{
    public async Task InitializeAsync(ConfigurationManager config)
    {
        var connectionStringBuilder = new ConnectionStringBuilder(config);
        await using var connection = new SqlConnection(connectionStringBuilder.Build(false));

        await connection.OpenAsync();

        var database = config["Database:InitialCatalog"];
        await connection.ExecuteAsync(new CommandDefinition($"""
            IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = '{database}')
            BEGIN
                CREATE DATABASE [{database}]
            END
            USE {database}
        """));

        await connection.ExecuteAsync(new CommandDefinition("""
            IF (NOT EXISTS (SELECT * 
                            FROM INFORMATION_SCHEMA.TABLES 
                            WHERE TABLE_NAME = 'Books'))
            BEGIN
                CREATE TABLE Books (
                    Id UNIQUEIDENTIFIER primary key,
                    Isbn13 NVARCHAR(13) not null,
                    Title NVARCHAR(200) not null,
                    Authors NVARCHAR(200) not null
                )
            END
        """));
    }
}