namespace ThermoBet.API.Controllers
{
    /// <summary>
    /// SigIn response
    /// </summary>
    public abstract class SignInResponse
    {
        /// <summary>
        /// Is login was done correctly
        /// </summary>
        public bool IsSucsess { get; set; }
    }
}