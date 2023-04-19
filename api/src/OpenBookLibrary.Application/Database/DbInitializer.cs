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
        """));

        await connection.ExecuteAsync(new CommandDefinition($"""
            USE {database}
            IF (NOT EXISTS (SELECT * 
                            FROM INFORMATION_SCHEMA.TABLES 
                            WHERE TABLE_NAME = 'Books'))
            BEGIN
                CREATE TABLE Books (
                    Id uniqueidentifier primary key,
                    Isbn13 nvarchar(13) NOT NULL,
                    Title nvarchar(200) NOT NULL,
                    Authors nvarchar(200) NOT NULL
                )
            END
        """));

        await connection.ExecuteAsync(new CommandDefinition("""
            IF (NOT EXISTS (SELECT * 
                            FROM INFORMATION_SCHEMA.TABLES 
                            WHERE TABLE_NAME = 'Borrows'))
            BEGIN
                CREATE TABLE Borrows (
                    UserId uniqueidentifier NOT NULL,
                    Borrowed datetime NOT NULL,
                    Returned datetime NULL,
                    BookId uniqueidentifier FOREIGN KEY REFERENCES Books(Id),
                )
            END
        """));
    }
}