namespace HeroAPI.DataAccessLayer.Models
{
    /// <summary>
    /// Represents a User entity with attributes such as name, email, password, and an optional associated Hero.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password associated with the user.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the optional identifier of an associated Hero, if applicable.
        /// </summary>
        public int? HeroId { get; set; }

        public string Role { get; set; }
    }

}
