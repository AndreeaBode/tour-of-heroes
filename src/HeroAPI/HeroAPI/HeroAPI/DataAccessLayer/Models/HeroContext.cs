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


        /// <summary>
        /// Gets or sets the DbSet for User entities.
        /// </summary>
        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Power> Powers { get; set; } = null!;

        // Define the many-to-many relationship between Hero and Power
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HeroPower>()
                .HasKey(hp => new { hp.HeroId, hp.PowerId });

            modelBuilder.Entity<HeroPower>()
                .HasOne(hp => hp.Hero)
                .WithMany(h => h.HeroPowers)
                .HasForeignKey(hp => hp.HeroId);

            modelBuilder.Entity<HeroPower>()
                .HasOne(hp => hp.Power)
                .WithMany(p => p.HeroPowers)
                .HasForeignKey(hp => hp.PowerId);
        }
    }

}
