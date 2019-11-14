namespace ThermoBet.API.Controllers
{
    /// <summary>
    /// Sucess response
    /// </summary>
    public class SignInSuccessResponse : SignInResponse
    {
        /// <summary>
        /// Token
        /// </summary>
        public string TokenId { get; set; }
    }
}