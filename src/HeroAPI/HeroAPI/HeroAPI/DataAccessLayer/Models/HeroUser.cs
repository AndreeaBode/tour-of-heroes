namespace HeroAPI.DataAccessLayer.Models
{
    public class HeroUser
    {
        public int Id { get; set; }

        public int HeroId { get; set; }

        public int UserId { get; set; }

        public Hero Hero { get; set; } = null!;

        public User User { get; set; } = null!;
    }
}
