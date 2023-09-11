namespace HeroAPI.DataAccessLayer.Models
{

    public class Power
    {
        public long Id { get; set; }
        public string? Name { get; set; }

        public ICollection<HeroPower> HeroPowers { get; set; } = new List<HeroPower>();

    }
}
