using System.Threading.Tasks;
using ThermoBet.Core.Models;

public interface IUserAuthentificationService
{
    Task<UserModel> LoginAsync(string login, string password);

    Task<UserModel> RegisterAsync(string login, string password);
}