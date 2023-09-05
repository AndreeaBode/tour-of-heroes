
namespace HeroAPI.BusinessLogicLayer.DTOs { 
/// <summary>
/// Data Transfer Object (DTO) for representing user login information.
/// </summary>
public class LoginDTO
{
    /// <summary>
    /// Gets or sets the email address associated with the user.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the password used for user authentication.
    /// </summary>
    public string Password { get; set; }
}
}