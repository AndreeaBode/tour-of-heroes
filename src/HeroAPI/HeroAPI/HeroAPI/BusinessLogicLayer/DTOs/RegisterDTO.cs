using Microsoft.VisualBasic;
using System.Security.Cryptography.Xml;
using System.Xml.Linq;

namespace HeroAPI.BusinessLogicLayer.DTOs
{
    /// <summary>
    /// Data Transfer Object (DTO) for representing user registration information.
    /// </summary>
    public class RegisterDTO
    {
        /// <summary>
        /// Gets or sets the email address provided during registration.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the password chosen by the user for registration.
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// Gets or sets the confirmed password for user registration to ensure accuracy.
        /// </summary>
        public string? ConfirmedPassword { get; set; }
    }
}
