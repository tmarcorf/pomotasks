using Microsoft.EntityFrameworkCore;
using Pomotasks.Domain.Entities;

namespace Pomotasks.Persistence.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<Todo> Todos { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Address> Addresses { get; set; }
    }
}
