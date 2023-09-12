namespace HeroAPI.DataAccessLayer.Models
{

    public class Power
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public ICollection<HeroPower> HeroPowers { get; set; } = new List<HeroPower> ();

        //public ICollection<Hero> Heroes { get; set; } = new ();

    }
}
