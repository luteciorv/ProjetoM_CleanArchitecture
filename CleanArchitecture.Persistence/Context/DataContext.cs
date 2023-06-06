using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistence.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }
      
        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
             modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);

        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }

    }
}
