using Microsoft.EntityFrameworkCore;

namespace HeroAPI.DataAccessLayer.Models
{
    public class HeroContextConfigurator
    {
        private readonly HeroContext heroContext;

        public HeroContextConfigurator(HeroContext heroContext)
        {
            this.heroContext = heroContext;
        }

        public async Task SeedData()
        {
            heroContext.Database.Migrate(); 

            if (!heroContext.Powers.Any())
            { 

                await heroContext.Powers.AddAsync(new Power
                {
                    Name = "Invisibility"
                }) ;

                await heroContext.Powers.AddAsync(new Power
                {
                    Name = "Super Speed"
                }) ;

                await heroContext.Powers.AddAsync(new Power
                {
                    Name = "Time Travel"
                }) ;

                await heroContext.Powers.AddAsync(new Power
                {
                    Name = "Mind Control"
                }) ;

                await heroContext.Powers.AddAsync(new Power
                {
                    Name = "Immortality"
                }) ;

                await heroContext.Powers.AddAsync(new Power
                {
                    Name = "Animal Communication"
                }) ;

                await heroContext.SaveChangesAsync();
            }
        }
    }
}
