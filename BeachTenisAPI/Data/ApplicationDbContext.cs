using BeachTenisAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeachTenisAPI.Data
{
    // It's needed `Microsoft.EntityFrameworkCore.Tools` NuGet package to follow those commands.
    // `add-migration comment` -> Creates an abstraction of SQL default to create and manage database, in a new folder named `Migrations`
    // `update database` -> Updated DataBase and created table if not exists.
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Spreadsheet>().HasKey(table => new {
                table.User, table.Day
            });
        }

        public DbSet<User> Users { get; set; } // STRONG
        public DbSet<Spreadsheet> Spreadsheets { get; set; } // ASSOCIATIVE
    }
}
