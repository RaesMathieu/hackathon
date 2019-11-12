using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThermoBet.Core.Models;

public interface IAdminAuthentificationService
{
    Task<UserModel> LoginAsync(string login, string password);
}
