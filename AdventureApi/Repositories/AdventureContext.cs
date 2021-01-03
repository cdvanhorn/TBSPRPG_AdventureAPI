using Microsoft.EntityFrameworkCore;
using AdventureApi.Entities;

namespace AdventureApi.Repositories {
    public class AdventureContext : DbContext {
        public AdventureContext(DbContextOptions<AdventureContext> options) : base(options){}

        public DbSet<Adventure> Adventures { get; set; }

        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adventure>().ToTable("AdventureService.Adventure");
            modelBuilder.Entity<Location>().ToTable("AdventureService.Location");

            modelBuilder.Entity<Adventure>()
                .HasMany(a => a.Locations)
                .WithOne(l => l.Adventure);
        }
    }
}