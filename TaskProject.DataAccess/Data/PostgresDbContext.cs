using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProject.Entities.Models;

namespace TaskProject.DataAccess.Data
{
    public class PostgresDbContext : DbContext
    {
        public PostgresDbContext(DbContextOptions<PostgresDbContext> options)
        : base(options)
        {

        }

        public DbSet<Contact> Contacts { get; set; }


    }
}
