using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;
using PetHelper.Core.DataBase;

namespace PetHelper.Volunteer.Infastructure;

public class SqlConnectionFactory (IConfiguration configuration) : ISqlConnectionFactory
{
    public IDbConnection CreateConnection() =>
        new NpgsqlConnection(configuration.GetConnectionString("Database"));
}