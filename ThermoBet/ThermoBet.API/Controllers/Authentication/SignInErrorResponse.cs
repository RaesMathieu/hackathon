namespace ThermoBet.API.Controllers
{
    public class SignInErrorResponse : SignInResponse
    {
        /// <summary>
        /// Explain why the Sign In faild
        /// </summary>
        public string ErrorMessage { get; }
    }
}