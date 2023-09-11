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

        public void SeedData()
        {
            heroContext.Database.Migrate(); 

            if (!heroContext.Heroes.Any())
            {
               /* heroContext.Heroes.Add(new Hero
                {

                });*/

                heroContext.Powers.Add(new Power
                {
                    Id = 0,
                    Name = "Invisibility"
                }) ; 
                
                heroContext.Powers.Add(new Power
                {
                    Id = 1,
                    Name = "Super Speed"
                }) ;
                
                heroContext.Powers.Add(new Power
                {
                    Id = 2,
                    Name = "Time Travel"
                }) ;
                
                heroContext.Powers.Add(new Power
                {
                    Id = 3,
                    Name = "Mind Control"
                }) ;
                
                heroContext.Powers.Add(new Power
                {
                    Id = 4,
                    Name = "Immortality"
                }) ;
                
                heroContext.Powers.Add(new Power
                {
                    Id = 5,
                    Name = "Animal Communication"
                }) ; 
            }
        }
    }
}
