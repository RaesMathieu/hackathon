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

        /// <summary>
        /// First name of the user
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Second name of the user
        /// </summary>
        public string SecondName { get; set; }

        /// <summary>
        /// Email of the user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Username in betclic website.
        /// </summary>
        public string BetclicUserName { get; set; }

    }
}
