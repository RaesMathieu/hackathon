using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ThermoBet.Core.Models;

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

            if (await _thermoBetContext
                .Users.AnyAsync(x => x.Login == login))
                return null;

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

        public async Task<UserModel> GetByAsync(int id)
        {
            return await _thermoBetContext
                    .Users
                    .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SigInAsync(UserModel user)
        {
            _thermoBetContext
                    .LoginHistories
                    .Add(new LoginHistoryModel
                    {
                        User = user,
                        LoginDateTimeUtc = DateTime.UtcNow
                    });

            await _thermoBetContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserModel user)
        {
            _thermoBetContext
                    .Users
                    .Update(user);

            await _thermoBetContext.SaveChangesAsync();
        }

    }
}