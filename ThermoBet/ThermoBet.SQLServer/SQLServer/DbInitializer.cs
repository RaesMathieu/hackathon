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

            context.Tournaments.Add(new TournamentModel
            {
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
                    }
                }

            });

            context.Tournaments.Add(new TournamentModel
            {
                Description = "",
                EndTimeUtc = DateTime.Today.ToUniversalTime(),
                Name = "Salve de hier",
                StartTimeUtc = DateTime.Today.AddDays(-1).ToUniversalTime(),
                Markets = new List<MarketModel>() {
                    new MarketModel {
                        Name = "Marseille vas t'il gagner le match se soir ?",
                        Selections = new List<SelectionModel>() {
                            new SelectionModel {
                                Name = "Marseille",
                                IsYes = true
                            },
                            new SelectionModel {
                                Name = "Valence",
                                IsYes = false
                            },
                        }
                    },
                    new MarketModel {
                        Name = "Lyon vas t'il gagner le match se soir ?",
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
                    }
                }

            });

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