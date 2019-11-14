namespace ThermoBet.API.Controllers.User
{
    /// <summary>
    /// Detail of a user.
    /// </summary>
    public class UserResponse : BaseUser
    {
        /// <summary>
        /// Identifier of the user.
        /// </summary>
        public int Id { get; set; }
    }
}
