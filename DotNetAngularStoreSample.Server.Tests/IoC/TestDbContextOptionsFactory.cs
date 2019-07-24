using DotNetAngularStoreSample.Repository.Ef;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace DotNetAngularStoreSample.Server.Tests.IoC
{
    /// <summary>
    /// Factory for In-Memory SqlLite DB options
    /// </summary>
    public class TestDbContextOptionsFactory
    {
        public static DbContextOptions<AppDbContext> Get()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseSqlite(connection);
            return builder.Options;
        }
    }
}