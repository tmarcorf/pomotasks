using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pomotasks.Domain.Entities;
using Pomotasks.Domain.Interfaces;
using Pomotasks.Persistence.DatabaseConfiguration;

namespace Pomotasks.Persistence.Context
{
    public class PomotasksContext : DbContext
    {
        public PomotasksContext()
        {
        }

        public PomotasksContext(DbContextOptions<PomotasksContext> options)
            : base(options)
        {
        }

        public DbSet<Todo> Todos { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Address> Addresses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    DatabaseConnection.ConnectionConfiguration
                    .GetConnectionString("DefaultConnection"));
            }

        }
    }
}
