using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThermoBet.Core.Models;

public interface IUserService
{
    Task<UserModel> GetByAsync(string login, string password);

    Task<UserModel> CreateAsync(string login, string password);
}