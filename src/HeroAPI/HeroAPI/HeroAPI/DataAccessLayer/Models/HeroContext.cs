using Microsoft.EntityFrameworkCore;


namespace HeroAPI.DataAccessLayer.Models
{
    public class HeroContext : DbContext
    {
        public HeroContext(DbContextOptions<HeroContext> options)
            : base(options)
        {
        }

        public DbSet<Hero> Heroes { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
    }
}
