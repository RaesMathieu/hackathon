namespace ThermoBet.API.Controllers
{
    /// <summary>
    /// Case of error authentification
    /// </summary>
    public class SignInErrorResponse : SignInResponse
    {
        /// <summary>
        /// Explain why the Sign In faild
        /// </summary>
        public string ErrorMessage { get; set;  }
    }
}