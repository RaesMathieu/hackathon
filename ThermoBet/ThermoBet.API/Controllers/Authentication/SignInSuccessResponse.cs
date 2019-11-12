namespace ThermoBet.API.Controllers
{
    public class SignInSuccessResponse : SignInResponse
    {
        /// <summary>
        /// Token
        /// </summary>
        public string TokenId { get; set; }
    }
}