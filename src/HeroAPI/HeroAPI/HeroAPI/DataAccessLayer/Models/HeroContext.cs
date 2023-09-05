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
    }

}
