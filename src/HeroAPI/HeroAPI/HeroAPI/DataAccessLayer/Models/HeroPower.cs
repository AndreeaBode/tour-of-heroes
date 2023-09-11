using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HeroAPI.DataAccessLayer.Models
{
    public class HeroPower
    {
        [Key]
        [Column(Order = 0)]
        public int HeroId { get; set; }
        [Key]
        [Column(Order = 1)]
        public int PowerId { get; set; }

         public Hero Hero { get; set; }
        public Power Power { get; set; }
    }
}
