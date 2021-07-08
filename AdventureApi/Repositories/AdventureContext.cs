using Microsoft.EntityFrameworkCore;
using AdventureApi.Entities;
using AdventureApi.Entities.LanguageSources;

namespace AdventureApi.Repositories {
    public class AdventureContext : DbContext {
        public AdventureContext(DbContextOptions<AdventureContext> options) : base(options){}

        public DbSet<Adventure> Adventures { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<En> SourcesEn { get; set; }
        public DbSet<Esp> SourcesEsp { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<Adventure>().ToTable("adventures");
            modelBuilder.Entity<Location>().ToTable("locations");
            modelBuilder.Entity<Route>().ToTable("routes");
            modelBuilder.Entity<En>().ToTable("sources_en");
            modelBuilder.Entity<Esp>().ToTable("sources_esp");

            modelBuilder.Entity<Adventure>().HasKey(a => a.Id);
            modelBuilder.Entity<Adventure>().Property(a => a.Id)
                .HasColumnType("uuid")
                .HasDefaultValueSql("uuid_generate_v4()")
                .IsRequired();

            modelBuilder.Entity<Location>().HasKey(a => a.Id);
            modelBuilder.Entity<Location>().Property(a => a.Id)
                .HasColumnType("uuid")
                .HasDefaultValueSql("uuid_generate_v4()")
                .IsRequired();
            
            modelBuilder.Entity<Route>().HasKey(a => a.Id);
            modelBuilder.Entity<Route>().Property(a => a.Id)
                .HasColumnType("uuid")
                .HasDefaultValueSql("uuid_generate_v4()")
                .IsRequired();

            modelBuilder.Entity<Adventure>()
                 .HasMany(a => a.Locations)
                 .WithOne(l => l.Adventure)
                 .HasForeignKey(l => l.AdventureId);

            modelBuilder.Entity<Location>()
                .HasMany(l => l.Routes)
                .WithOne(r => r.Location)
                .HasForeignKey(r => r.LocationId);
            
            //language sources
            modelBuilder.Entity<En>().HasKey(e => e.Id);
            modelBuilder.Entity<En>().Property(e => e.Id)
                .HasColumnType("uuid")
                .HasDefaultValueSql("uuid_generate_v4()")
                .IsRequired();
            
            modelBuilder.Entity<Esp>().HasKey(e => e.Id);
            modelBuilder.Entity<Esp>().Property(e => e.Id)
                .HasColumnType("uuid")
                .HasDefaultValueSql("uuid_generate_v4()")
                .IsRequired();
        }
    }
}