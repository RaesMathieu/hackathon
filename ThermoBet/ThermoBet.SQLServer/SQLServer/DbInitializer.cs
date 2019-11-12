using ThermoBet.Data;
using ThermoBet.Core.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ThermoBet.Data.Services;

namespace ThermoBet.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ThermoBetContext context)
        {
            context.Database.EnsureCreated();
            //context.Database.Migrate();


            if (context.Tournaments.Any())
            {
                return;
            }

            for(int i =0; i< 100 ; i++)
            {
            context.Tournaments.Add(new TournamentModel
            {
                Description = "Tournament inserting for testing n" + i,
                EndTimeUtc = DateTime.UtcNow.AddDays(i*-1),
                Name = "TestTournament n" + i,
                StartTimeUtc = DateTime.UtcNow.AddDays((i+1)*-1),
                Markets = new List<MarketModel>() {
                    new MarketModel {
                        Name = "Lille - Valence",
                        Selections = new List<SelectionModel>() {
                            new SelectionModel {
                                Name = "Lille",
                                Odds = 1.02m
                            },
                            new SelectionModel {
                                Name = "NULL",
                                Odds = 1.10m
                            },
                            new SelectionModel {
                                Name = "Valence",
                                Odds = 2.42m
                            },
                        }
                    },
                    new MarketModel {
                        Name = "Benefica - Lyon",
                        Selections = new List<SelectionModel>() {
                            new SelectionModel {
                                Name = "Benefica",
                                Odds = 1.02m
                            },
                            new SelectionModel {
                                Name = "NULL",
                                Odds = 1.10m
                            },
                            new SelectionModel {
                                Name = "Lyon",
                                Odds = 2.42m
                            },
                        }
                    }
                }

            });

            }


            context.Users.Add(new UserModel
            {
                Login = "Admin",
                HashPassword = Encryptor.MD5Hash("P@ssword12345"),
                IsAdmin = true
            });
            
            context.SaveChanges();
        }
    }
}