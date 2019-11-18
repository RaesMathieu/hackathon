using System.Linq;
using System.Threading.Tasks;
using ThermoBet.Core.Models;

public interface IUserService
{
    Task<UserModel> GetByAsync(string login, string password);

    Task<UserModel> CreateAsync(string login, string password);

    Task<UserModel> GetByAsync(int id);

    Task SigInAsync(UserModel user);

    Task UpdateAsync(UserModel user);

    IQueryable<UserModel> GetAllUsersAsync();

    Task Update(UserModel user);
}