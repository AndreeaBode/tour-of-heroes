using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace HeroAPI.DataAccessLayer.Models
{
    public class HeroPower
    {
        public int Id { get; set; }

        public int HeroId { get; set; }

        public int PowerId { get; set; }

        public Hero Hero { get; set; } = null!;
        public Power Power { get; set; } = null!;
    }
}
