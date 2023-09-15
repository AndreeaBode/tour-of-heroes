using HeroAPI.DataAccessLayer.Models;

namespace HeroAPI.BusinessLogicLayer.DTOs
{
    public class HeroDTO
    {
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the name of the hero.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the power or abilities of the hero.
        /// </summary>
        //public string? Power { get; set; }


        /// <summary>
        /// Gets or sets the URL of an image representing the hero.
        /// </summary>
        public string? ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets a description or background information about the hero.
        /// </summary>
        public string? Description { get; set; }

        public string Power { get; set; }
    }
}
