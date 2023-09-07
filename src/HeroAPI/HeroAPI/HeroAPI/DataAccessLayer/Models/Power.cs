namespace HeroAPI.DataAccessLayer.Models
{
    public class Power
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public ICollection<Hero> Heroes { get; set; }
    }

}
