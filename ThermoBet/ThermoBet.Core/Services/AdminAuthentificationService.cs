using System.Threading.Tasks;
using ThermoBet.Core.Models;

namespace ThermoBet.Core
{
    public class AdminAuthentificationService : IAdminAuthentificationService
    {
        private readonly IUserService userService;

        public AdminAuthentificationService(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<UserModel> LoginAsync(string login, string password)
        {
            var user = await userService.GetByAsync(login, password);
            if (user != null && user.IsAdmin)
                return user;
            return null;
        }
    }
}