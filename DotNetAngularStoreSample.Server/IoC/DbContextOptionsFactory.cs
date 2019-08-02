using DotNetAngularStoreSample.Repository.Ef;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DotNetAngularStoreSample.Server.IoC
{
    /// <summary>
    /// Creates options for Db context with connection string from appsettings.json
    /// </summary>
    public class DbContextOptionsFactory
    {
        public static DbContextOptions<AppDbContext> Get(IConfiguration configuration)
        {
            var connectionString = configuration["SqlServer:ConnectionString"];
            var type = configuration["SqlServer:Type"];

            var builder = new DbContextOptionsBuilder<AppDbContext>();
            if (type?.ToLower() == "sqlite")
            {
                var connection = new SqliteConnection(connectionString);
                connection.Open();
                
                builder.UseSqlite(connection);
            }
            else
            {
                builder.UseSqlServer(connectionString);
            }
            return builder.Options;
        }
    }
}