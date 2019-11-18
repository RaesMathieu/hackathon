using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ThermoBet.Core.Exception;
using ThermoBet.Core.Interfaces;
using ThermoBet.Core.Models;

namespace ThermoBet.Data.Services
{
    public class UserService : IUserService
    {
        private readonly ThermoBetContext _thermoBetContext;
        private readonly IConfigurationService _configurationService;

        public UserService(
            ThermoBetContext thermoBetContext,
            IConfigurationService configurationService)
        {
            _thermoBetContext = thermoBetContext;
            this._configurationService = configurationService;
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
            var dateTime = await _configurationService.GetDateTimeUtcNow();
            _thermoBetContext
                    .LoginHistories
                    .Add(new LoginHistoryModel
                    {
                        User = user,
                        LoginDateTimeUtc = dateTime
                    });

            await _thermoBetContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserModel user)
        {
            if (!string.IsNullOrEmpty(user.Pseudo))
            {
                var pseudoAlreadyInUSer = await _thermoBetContext
                        .Users
                        .AnyAsync(x => x.Pseudo == user.Pseudo && x.Id != user.Id);
                if (pseudoAlreadyInUSer)
                    throw new UserPseudoAlreadyUsedCoreException();
            }

            _thermoBetContext
                    .Users
                    .Update(user);

            await _thermoBetContext.SaveChangesAsync();
        }

        public IQueryable<UserModel> GetAllUsersAsync()
        {
            var users = _thermoBetContext
                    .Users
                    .Where(x => x.IsAdmin == false);

            return users;
        }
    }
}