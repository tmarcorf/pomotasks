using Microsoft.EntityFrameworkCore;
using Pomotasks.Domain.Entities;
using Pomotasks.Domain.Interfaces;

namespace Pomotasks.Persistence.Context
{
    public class PomotasksContext : DbContext
    {
        public PomotasksContext(DbContextOptions<PomotasksContext> options)
            : base(options)
        {
        }

        public DbSet<Todo> Todos { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
