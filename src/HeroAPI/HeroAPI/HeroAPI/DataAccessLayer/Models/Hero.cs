using Microsoft.EntityFrameworkCore;


namespace HeroAPI.DataAccessLayer.Models
{
    public class Hero
    {

        public long Id { get; set; }
        public string? Name { get; set; }

        public string? Power { get; set; }
        public string? ImageUrl { get; set; }

        public string? Description { get; set; }
    }
}

