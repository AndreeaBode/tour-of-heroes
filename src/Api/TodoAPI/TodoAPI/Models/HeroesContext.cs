using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models;

public class HeroesContext : DbContext
{
    public HeroesContext(DbContextOptions<HeroesContext> options)
        : base(options)
    {
    }

    public DbSet<Heroes> HeroesItem { get; set; } = null!;
}