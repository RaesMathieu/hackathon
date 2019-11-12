namespace ThermoBet.API.Controllers
{
    public abstract class SignInResponse
    {
        /// <summary>
        /// Is login was done correctly
        /// </summary>
        public bool IsSucsess { get; set; }
    }
}