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
            context.Database.EnsureCreated();
            // Database creation/Migration if needed
            //try
            //{
            //    context.Database.Migrate();
            //}
            //catch (InvalidOperationException ex) when(ex.Message == "Relational-specific methods can only be used when the context is using a relational database provider.")
            //{
            //    // do nothing - this error occurs when using memory database.
            //}

            if (context.Tournaments.Any())
            {
                return;
            }

            var tournament = new TournamentModel
            {
                Code = "Test",
                Description = "",
                EndTimeUtc = DateTime.Today.AddDays(1).ToUniversalTime(),
                Name = "Salve de aujoutd'hui",
                StartTimeUtc = DateTime.Today.ToUniversalTime(),
                Markets = new List<MarketModel>() {
                    new MarketModel {
                        Name = "Lille vas t'il gagner le match se soir ?",
                        Selections = new List<SelectionModel>() {
                            new SelectionModel {
                                Name = "Lille",
                                IsYes = true
                            },
                            new SelectionModel {
                                Name = "Valence",
                                IsYes = false
                            },
                        }
                    },
                    new MarketModel {
                        Name = "Benefica vas t'il gagner le match se soir ?",
                        Selections = new List<SelectionModel>() {
                            new SelectionModel {
                                Name = "Benefica",
                                IsYes = true
                            },
                            new SelectionModel {
                                Name = "Lyon",
                                IsYes = false
                            },
                        }
                    },
                },
                Winnables = new List<TournamentWinnableModel>()
                {
                    new TournamentWinnableModel
                    {
                        NbGoodAnswer = 8,
                        AmountOfWinnings = 5
                    },
                    new TournamentWinnableModel
                    {
                        NbGoodAnswer = 10,
                        AmountOfWinnings = 100
                    }
                }

            };
            context.Tournaments.Add(tournament);

            // Sure we have one admin user.
            if (!context.Users.Any(x => x.IsAdmin == true))
            {
                context.Users.Add(new UserModel
                {
                    Login = "Admin",
                    HashPassword = Encryptor.MD5Hash("P@ssword12345"),
                    IsAdmin = true
                });
                var userTest = new UserModel
                {
                    Login = "TestUser",
                    HashPassword = Encryptor.MD5Hash("P@ssword12345"),
                    IsAdmin = false,
                    BetclicUserName = "BetclicUserName",
                    Email = "email@email.com",
                    FirstName = "FirstName",
                    SecondName = "SecondName",
                    Pseudo = "Pseudo"
                };

                userTest.OptinTournament = new List<TournamentUserOptinModel>()
                {
                    new TournamentUserOptinModel
                    {
                        Tournament = tournament,
                        User = userTest,
                        DateUtc = DateTime.UtcNow
                    }
                };

                context.Users.Add(userTest);

                foreach(var market in tournament.Markets)
                {
                    context.Bets.Add(new BetModel
                    {
                        DateUtc = DateTime.UtcNow,
                        Market = market,
                        Tournament = tournament,
                        Selection = market.Selections.First(),
                        User = userTest
                    });
                }
            }



            context.SaveChanges();
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