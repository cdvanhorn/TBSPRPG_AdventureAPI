using Microsoft.EntityFrameworkCore;
using AdventureApi.Entities;

namespace AdventureApi.Repositories {
    public class AdventureContext : DbContext {
        public AdventureContext(DbContextOptions<AdventureContext> options) : base(options){}

        public DbSet<Adventure> Adventures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adventure>().ToTable("AdventureService.Adventure");
        }
    }
}