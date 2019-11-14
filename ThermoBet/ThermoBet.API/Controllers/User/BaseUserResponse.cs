namespace ThermoBet.API.Controllers.User
{
    public class BaseUser
    {
        /// <summary>
        /// identifier of the user.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Pseudo of the uer visible on boards.
        /// </summary>
        public string Pseudo { get; set; }

        /// <summary>
        /// Identifier of the avatar.
        /// </summary>
        public string Avatar { get; set; }

    }
}
