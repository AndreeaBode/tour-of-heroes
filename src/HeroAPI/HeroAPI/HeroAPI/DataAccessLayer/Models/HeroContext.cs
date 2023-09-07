using Microsoft.EntityFrameworkCore;


namespace HeroAPI.DataAccessLayer.Models
{
    /// <summary>
    /// Represents the database context for managing Hero and User entities using Entity Framework Core.
    /// </summary>
    public class HeroContext : DbContext
    {
        public HeroContext(DbContextOptions<HeroContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the DbSet for Hero entities.
        /// </summary>
        public DbSet<Hero> Heroes { get; set; } = null!;

        public DbSet<Power> Powers { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for User entities.
        /// </summary>
        public DbSet<User> Users { get; set; } = null!;

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurare relație one-to-many între Hero și Power
            modelBuilder.Entity<Hero>()
                .HasMany(hero => hero.Powers)
                .WithMany(power => power.Heroes)
                .UsingEntity(j => j.ToTable("HeroPowers"));
        }*/
    }

}
