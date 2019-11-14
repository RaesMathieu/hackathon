namespace ThermoBet.API.Controllers
{
    /// <summary>
    /// Credential of the user.
    /// </summary>
    public class SigInRequest
    {
        /// <summary>
        /// User name for identifier the user
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// PAssward
        /// </summary>
        public string Password { get; set; }
    }
}