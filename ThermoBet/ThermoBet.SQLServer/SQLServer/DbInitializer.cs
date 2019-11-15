using ThermoBet.Core.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ThermoBet.Data.Services;

namespace ThermoBet.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ThermoBetContext context)
        {
            // Database creation if needed
            context.Database.EnsureCreated();

            // Sure we have one admin user.
            if (!context.Users.Any(x => x.IsAdmin == true))
            {
                context.Users.Add(new UserModel
                {
                    Login = "Admin",
                    HashPassword = Encryptor.MD5Hash("P@ssword12345"),
                    IsAdmin = true
                });

                context.SaveChanges();
            }
        }

        public static void ClearData(ThermoBetContext context)
        {


            DropTable(context,
                "Bet",
                "Bets",
                "Tournaments",
                "Markets",
                "Selections", 
                "Users",
                "LoginHistories",
                "Configurations");

            context.Database.EnsureCreated();

            context.Users.Add(new UserModel
            {
                Login = "Admin",
                HashPassword = Encryptor.MD5Hash("P@ssword12345"),
                IsAdmin = true
            });

            context.SaveChanges();
        }

        private static void DropTable(ThermoBetContext context, params string[] tableName)
        {
            var allTables = string.Join(", ", tableName.Select(x => $@"`{x}`"));

            var query = $@"DROP TABLE IF EXISTS {allTables}";
            context.Database.ExecuteSqlCommand(query);
        }
    }
}