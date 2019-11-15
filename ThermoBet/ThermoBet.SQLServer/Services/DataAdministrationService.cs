using System;
using System.Collections.Generic;
using ThermoBet.Core.Interfaces;
using ThermoBet.Core.Models;
using ThermoBet.Data;

namespace ThermoBet.SQLServer.Services
{
    public class DataAdministrationService : IDataAdministrationService
    {
        private readonly ThermoBetContext context;

        public DataAdministrationService(ThermoBetContext thermoBetContext)
        {
            context = thermoBetContext;
        }

        public void ClearData()
        {
            DbInitializer.ClearData(context);
        }

        public void InsertTestData()
        {
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

            context.SaveChanges();
        }
    }
}
