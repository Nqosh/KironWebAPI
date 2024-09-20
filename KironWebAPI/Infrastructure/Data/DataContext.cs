using KironWebAPI.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace KironWebAPI.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<MappingCountryEvent> MappingCountryEvents { get; set; }

        public DbSet<Navigation> Navigation { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
