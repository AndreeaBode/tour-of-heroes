namespace HeroAPI.BusinessLogicLayer.DTOs
{
    /// <summary>
    /// Data Transfer Object (DTO) representing a response containing an authentication token.
    /// </summary>
    public class LoginResponseDTO
    {
        /// <summary>
        /// Gets or sets the authentication token generated upon successful login.
        /// </summary>
        public string? Token { get; set; }
    }
}
