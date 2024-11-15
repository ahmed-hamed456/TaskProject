using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProject.DataAccess.Data;
using TaskProject.Entities.Models;

namespace TaskProject.DataAccess.Handlers
{
    public class DbContextFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;

        public DbContextFactory(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }

        public DbContext CreateDbContext(DatabaseType databaseType)
        {
            var optionsBuilder1 = new DbContextOptionsBuilder<ApplicationDbContext>();
            var optionsBuilder2 = new DbContextOptionsBuilder<PostgresDbContext>();

            switch (databaseType)
            {
                case DatabaseType.SqlServer:
                    optionsBuilder1.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
                    return new ApplicationDbContext(optionsBuilder1.Options);
                    
                case DatabaseType.PostgreSQL:
                    optionsBuilder2.UseNpgsql(_configuration.GetConnectionString("PostgresConnection"));
                    return new PostgresDbContext(optionsBuilder2.Options);

                default:
                    throw new NotSupportedException($"Database type {databaseType} is not supported.");
                }
            }
      }
}
