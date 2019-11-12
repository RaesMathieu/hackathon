using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ThermoBet.Core.Models;
using ThermoBet.Data;

namespace ThermoBet.Data.Services
{
    public class UserService : IUserService
    {
        private readonly ThermoBetContext _thermoBetContext;

        public UserService(ThermoBetContext thermoBetContext)
        {
            _thermoBetContext = thermoBetContext;
        }

        public async Task<UserModel> CreateAsync(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                return null;

            var userModel = new UserModel
            {
                Login = login,
                HashPassword = Encryptor.MD5Hash(password)
            };
            _thermoBetContext
                .Users
                .Add(userModel);

            await _thermoBetContext.SaveChangesAsync();

            return userModel;
        }

        public async Task<UserModel> GetByAsync(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                return null;

            var hashPassword = Encryptor.MD5Hash(password);
            return await _thermoBetContext
                    .Users
                    .FirstOrDefaultAsync(x => x.Login == login && x.HashPassword == hashPassword);
        }
    }
}