﻿using DotNetAngularStoreSample.Repository.Ef;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DotNetAngularStoreSample.Server.IoC
{
    public class DbContextOptionsFactory
    {
        public static DbContextOptions<AppDbContext> Get(IConfiguration configuration)
        {
            var connectionString = configuration["SqlServer:ConnectionString"];
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseSqlServer(connectionString);
            return builder.Options;
        }
    }
}