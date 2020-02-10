using ThermoBet.Core.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ThermoBet.Data.Services;
using System;
using System.Collections.Generic;

namespace ThermoBet.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ThermoBetContext context)
        {
            // Database creation/Migration if needed
            context.Database.Migrate();

            //if (context.Tournaments.Any())
            //{
            //    return;
            //}

            //context.Tournaments.Add(new TournamentModel
            //{
            //    Code = "Test",
            //    Description = "",
            //    EndTimeUtc = DateTime.Today.AddDays(1).ToUniversalTime(),
            //    Name = "Salve de aujoutd'hui",
            //    StartTimeUtc = DateTime.Today.ToUniversalTime(),
            //    Markets = new List<MarketModel>() {
            //        new MarketModel {
            //            Name = "Lille vas t'il gagner le match se soir ?",
            //            Selections = new List<SelectionModel>() {
            //                new SelectionModel {
            //                    Name = "Lille",
            //                    IsYes = true
            //                },
            //                new SelectionModel {
            //                    Name = "Valence",
            //                    IsYes = false
            //                },
            //            }
            //        },
            //        new MarketModel {
            //            Name = "Benefica vas t'il gagner le match se soir ?",
            //            Selections = new List<SelectionModel>() {
            //                new SelectionModel {
            //                    Name = "Benefica",
            //                    IsYes = true
            //                },
            //                new SelectionModel {
            //                    Name = "Lyon",
            //                    IsYes = false
            //                },
            //            }
            //        },
            //    },
            //    //Winnables = new List<TournamentWinnableModel>()
            //    //{
            //    //    new TournamentWinnableModel
            //    //    {
            //    //        NbGoodAnswer = 8,
            //    //        AmountOfWinnings = 5
            //    //    },
            //    //    new TournamentWinnableModel
            //    //    {
            //    //        NbGoodAnswer = 10,
            //    //        AmountOfWinnings = 100
            //    //    }
            //    //}

            //});

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
                "Selections");

            DropTable2(context,
               "Configurations",
                "Users",
                "LoginHistories");

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
            var allTables = string.Join(", ", tableName.Select(x => $@"{x}"));
            var query = $@"SET FOREIGN_KEY_CHECKS=0; DROP TABLE IF EXISTS {allTables}; SET FOREIGN_KEY_CHECKS=1;";
            context.Database.ExecuteSqlCommand(query);
        }

        private static void DropTable2(ThermoBetContext context, params string[] tableName)
        {
            var allTables = string.Join(", ", tableName.Select(x => $@"{x}"));
            var query = $@"SET FOREIGN_KEY_CHECKS=0; DROP TABLE IF EXISTS {allTables}; SET FOREIGN_KEY_CHECKS=1;";
            context.Database.ExecuteSqlCommand(query);
        }
    }
}