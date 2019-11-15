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
            var startDateTime = new DateTime(2019, 12, 9).ToUniversalTime();
            //Tournament - 1
            context.Tournaments.Add(new TournamentModel
            {
                Name = "Parie le 9 pour match du 10 décembre LIGUE DES CHAMPIONS (Inter – Barcelone, Lyon – RB Leipzig, Chelsea - Lille)",
                StartTimeUtc = startDateTime,
                EndTimeUtc = startDateTime.AddDays(1),
                Markets = new List<MarketModel>() {
                    new MarketModel { Name = "Barcelone gagne le match ?",
                        Selections = new List<SelectionModel>() {
                            new SelectionModel { Name = "Inter", IsYes = true,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/barcelone_fr.png",
                            },
                            new SelectionModel { Name = "Barcelone", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/barcelone_fr.png",
                            },
                        }
                    },
                    new MarketModel { Name = "Y aura t-il moins de 3 buts dans le match ?",
                        Selections = new List<SelectionModel>() {
                            new SelectionModel { Name = "Inter", IsYes = true,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/barcelone_fr.png",
                            },
                            new SelectionModel { Name = "Barcelone", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/barcelone_fr.png",
                            }
                        }
                    },
                    new MarketModel { Name = "Y aura t-il plus que 7 corners dans le match ?",
                        Selections = new List<SelectionModel>() {
                            new SelectionModel { Name = "Inter", IsYes = true,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/barcelone_fr.png",
                            },
                            new SelectionModel { Name = "Barcelone", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/barcelone_fr.png",
                            },
                        }
                    },
                    new MarketModel { Name = "Lyon gagne le match ?",
                        Selections = new List<SelectionModel>() {
                             new SelectionModel { Name = "Lyon", IsYes = true,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/Lyon.png",

                            },
                            new SelectionModel { Name = "RB Leipzig", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/leipzig_fr.png",
                            },
                        }
                    },
                    new MarketModel { Name = "Y aura t-il un doublé marqué dans le match ?",
                        Selections = new List<SelectionModel>() {
                             new SelectionModel { Name = "Lyon", IsYes = true,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/Lyon.png",

                            },
                            new SelectionModel { Name = "RB Leipzig", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/leipzig_fr.png",
                            },
                        }
                    },
                    new MarketModel { Name = "But entre la 1ere et la 15eme minute ?",
                        Selections = new List<SelectionModel>() {
                             new SelectionModel { Name = "Lyon", IsYes = true,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/Lyon.png",

                            },
                            new SelectionModel { Name = "RB Leipzig", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/leipzig_fr.png",
                            },
                        }
                    },
                    new MarketModel { Name = "Lille gagne le match ?",
                        Selections = new List<SelectionModel>() {
                            new SelectionModel { Name = "Chelsea", IsYes = true,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/leipzig_fr.png",
                            },
                            new SelectionModel { Name = "Lille", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/Lyon.png",

                            },
                        }
                    },
                    new MarketModel { Name = "Y aura-t-il plus de buts en 1ere mi-temps qu’en deuxieme ?",
                        Selections = new List<SelectionModel>() {
                            new SelectionModel { Name = "Chelsea", IsYes = true,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/leipzig_fr.png",
                            },
                            new SelectionModel { Name = "Lille", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/Lyon.png",

                            },
                        }
                    },
                    new MarketModel { Name = "Y aura t-il moins de 3 buts dans le match ?",
                        Selections = new List<SelectionModel>() {
                            new SelectionModel { Name = "Chelsea", IsYes = true,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/leipzig_fr.png",
                            },
                            new SelectionModel { Name = "Lille", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/Lyon.png",

                            },
                        }
                    },
                    new MarketModel { Name = "L’arbitre arbitrera-t-il en jaune ?",
                        Selections = new List<SelectionModel>() {
                            new SelectionModel { Name = "Chelsea", IsYes = true,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/leipzig_fr.png",
                            },
                            new SelectionModel { Name = "Lille", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/Lyon.png",

                            },
                        }
                    }
                }

            });

            context.SaveChanges();

            startDateTime = startDateTime.AddDays(1);
            //Tournament - 2
            context.Tournaments.Add(new TournamentModel
            {
                Name = "Parie le 10 pour match 11 décembre LIGUE DES CHAMPIONS (PSG - Galatasaray, Bayern Munich – Tottenham, Bruges - Real Madrid)",
                StartTimeUtc = startDateTime,
                EndTimeUtc = startDateTime.AddDays(1),
                Markets = new List<MarketModel>() {
                    new MarketModel { Name = "Paris gagne le match ?",
                        Selections = new List<SelectionModel>() {
                            new SelectionModel { Name = "PSG ", IsYes = true,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/barcelone_fr.png",
                            },
                            new SelectionModel { Name = "Galatasaray", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/barcelone_fr.png",
                            },
                        }
                    },
                    new MarketModel { Name = "Y aura t-il moins de 3 buts dans le match ?",
                        Selections = new List<SelectionModel>() {
                            new SelectionModel { Name = "PSG ", IsYes = true,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/barcelone_fr.png",
                            },
                            new SelectionModel { Name = "Galatasaray", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/barcelone_fr.png",
                            },
                        }
                    },
                    new MarketModel { Name = "Y aura t-il plus que 9 corners dans le match ?",
                        Selections = new List<SelectionModel>() {
                            new SelectionModel { Name = "PSG ", IsYes = true,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/barcelone_fr.png",
                            },
                            new SelectionModel { Name = "Galatasaray", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/barcelone_fr.png",
                            },
                        }
                    },
                    new MarketModel { Name = "Tottenham gagne le match ?",
                        Selections = new List<SelectionModel>() {
                             new SelectionModel { Name = "Bayern Munich", IsYes = true,
                                ImgUrl = " http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/tottenham_fr.png",
                            },
                            new SelectionModel { Name = "Tottenham", IsYes = false,
                                ImgUrl = " http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/tottenham_fr.png",
                            },
                        }
                    },
                    new MarketModel { Name = "Y aura t-il un doublé marqué dans le match ?",
                        Selections = new List<SelectionModel>() {
                             new SelectionModel { Name = "Bayern Munich", IsYes = true,
                                ImgUrl = " http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/tottenham_fr.png",
                            },
                            new SelectionModel { Name = "Tottenham", IsYes = false,
                                ImgUrl = " http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/tottenham_fr.png",
                            },
                        }
                    },
                    new MarketModel { Name = "But entre la 1ere et la 15eme minute ?",
                        Selections = new List<SelectionModel>() {
                             new SelectionModel { Name = "Bayern Munich", IsYes = true,
                                ImgUrl = " http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/tottenham_fr.png",
                            },
                            new SelectionModel { Name = "Tottenham", IsYes = false,
                                ImgUrl = " http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/tottenham_fr.png",
                            },
                        }
                    },
                    new MarketModel { Name = "Madrid gagne le match ?",
                        Selections = new List<SelectionModel>() {
                            new SelectionModel { Name = "Bruges", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/bruges_fr.png",
                            },
                            new SelectionModel { Name = "Real Madrid", IsYes = true,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/bruges_fr.png",
                            },
                        }
                    },
                    new MarketModel { Name = "1ere mi-temps plus prolifique que la 2eme ?",
                        Selections = new List<SelectionModel>() {
                            new SelectionModel { Name = "Bruges", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/bruges_fr.png",
                            },
                            new SelectionModel { Name = "Real Madrid", IsYes = true,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/bruges_fr.png",
                            },
                        }
                    },
                    new MarketModel { Name = "Y aura t-il moins de 3 buts dans le match ?",
                        Selections = new List<SelectionModel>() {
                            new SelectionModel { Name = "Bruges", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/bruges_fr.png",
                            },
                            new SelectionModel { Name = "Real Madrid", IsYes = true,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/bruges_fr.png",
                            },
                        }
                    },
                    new MarketModel { Name = "Y’aura t-il + de 4 minutes de temps additionnel à la fin du match ?",
                        Selections = new List<SelectionModel>() {
                            new SelectionModel { Name = "Bruges", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/bruges_fr.png",
                            },
                            new SelectionModel { Name = "Real Madrid", IsYes = true,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/bruges_fr.png",
                            },
                        }
                    }
                }

            });

            context.SaveChanges();
        }
    }
}
