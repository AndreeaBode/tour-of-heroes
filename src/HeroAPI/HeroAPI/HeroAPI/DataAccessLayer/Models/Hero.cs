using Microsoft.EntityFrameworkCore;


namespace HeroAPI.DataAccessLayer.Models
{
    /// <summary>
    /// Represents a Hero entity with attributes such as name, power, image URL, and description.
    /// </summary>
    public class Hero
    {
        /// <summary>
        /// Gets or sets the unique identifier for the hero.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the hero.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the power or abilities of the hero.
        /// </summary>
        public List<Power> Powers { get; set; } = new List<Power>();

        /// <summary>
        /// Gets or sets the URL of an image representing the hero.
        /// </summary>
        public string? ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets a description or background information about the hero.
        /// </summary>
        public string? Description { get; set; }


    }

}

