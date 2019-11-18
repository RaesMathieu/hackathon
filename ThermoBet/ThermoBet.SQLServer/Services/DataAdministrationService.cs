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
            var startDateTime = new DateTime(2019, 12, 9, 20, 0, 0).ToUniversalTime();
            #region Parie le 9 pour match du 10 décembre LIGUE DES CHAMPIONS (Inter – Barcelone, Lyon – RB Leipzig, Chelsea - Lille)
            context.Tournaments.Add(new TournamentModel
            {
                Name = "Parie le 9 pour match du 10 décembre LIGUE DES CHAMPIONS (Inter – Barcelone, Lyon – RB Leipzig, Chelsea - Lille)",
                StartTimeUtc = startDateTime,
                EndTimeUtc = startDateTime.AddDays(1),
                Markets = new List<MarketModel>() {
                    new MarketModel { Name = "Barcelone gagne le match ?", Position = 1, StartTimeUtc = new DateTime(2019, 12, 10, 21, 0, 0),
                        Selections = new List<SelectionModel>() {
                            new SelectionModel { Name = "Inter", IsYes = true,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/milan-inter_fr.png",
                            },
                            new SelectionModel { Name = "Barcelone", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/barcelone_fr.png",
                            },
                        }
                    },
                    new MarketModel { Name = "Y aura t-il moins de 3 buts dans le match ?", Position = 2, StartTimeUtc = new DateTime(2019, 12, 10, 21, 0, 0),
                        Selections = new List<SelectionModel>() {
                            new SelectionModel { Name = "Inter", IsYes = true,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/milan-inter_fr.png",
                            },
                            new SelectionModel { Name = "Barcelone", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/barcelone_fr.png",
                            }
                        }
                    },
                    new MarketModel { Name = "Y aura t-il plus que 7 corners dans le match ?", Position = 3, StartTimeUtc = new DateTime(2019, 12, 10, 21, 0, 0),
                        Selections = new List<SelectionModel>() {
                            new SelectionModel { Name = "Inter", IsYes = true,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/milan-inter_fr.png",
                            },
                            new SelectionModel { Name = "Barcelone", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/barcelone_fr.png",
                            },
                        }
                    },
                    new MarketModel { Name = "Lyon gagne le match ?", Position = 4, StartTimeUtc = new DateTime(2019, 12, 10, 21, 0, 0),
                        Selections = new List<SelectionModel>() {
                             new SelectionModel { Name = "Lyon", IsYes = true,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/Lyon.png",

                            },
                            new SelectionModel { Name = "RB Leipzig", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/leipzig_fr.png",
                            },
                        }
                    },
                    new MarketModel { Name = "Y aura t-il un doublé marqué dans le match ?", Position = 5, StartTimeUtc = new DateTime(2019, 12, 10, 21, 0, 0),
                        Selections = new List<SelectionModel>() {
                             new SelectionModel { Name = "Lyon", IsYes = true,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/Lyon.png",

                            },
                            new SelectionModel { Name = "RB Leipzig", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/leipzig_fr.png",
                            },
                        }
                    },
                    new MarketModel { Name = "But entre la 1ere et la 15eme minute ?", Position = 6, StartTimeUtc = new DateTime(2019, 12, 10, 21, 0, 0),
                        Selections = new List<SelectionModel>() {
                             new SelectionModel { Name = "Lyon", IsYes = true,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/Lyon.png",

                            },
                            new SelectionModel { Name = "RB Leipzig", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/leipzig_fr.png",
                            },
                        }
                    },
                    new MarketModel { Name = "Lille gagne le match ?", Position = 7, StartTimeUtc = new DateTime(2019, 12, 10, 21, 0, 0),
                        Selections = new List<SelectionModel>() {
                            new SelectionModel { Name = "Chelsea", IsYes = true,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/chelsea_fr.png",
                            },
                            new SelectionModel { Name = "Lille", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/lille.png",

                            },
                        }
                    },
                    new MarketModel { Name = "Y aura-t-il plus de buts en 1ere mi-temps qu’en deuxieme ?", Position = 8, StartTimeUtc = new DateTime(2019, 12, 10, 21, 0, 0),
                        Selections = new List<SelectionModel>() {
                            new SelectionModel { Name = "Chelsea", IsYes = true,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/chelsea_fr.png",
                            },
                            new SelectionModel { Name = "Lille", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/lille.png",

                            },
                        }
                    },
                    new MarketModel { Name = "Y aura t-il moins de 3 buts dans le match ?", Position = 9, StartTimeUtc = new DateTime(2019, 12, 10, 21, 0, 0),
                        Selections = new List<SelectionModel>() {
                            new SelectionModel { Name = "Chelsea", IsYes = true,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/chelsea_fr.png",
                            },
                            new SelectionModel { Name = "Lille", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/lille.png",

                            },
                        }
                    },
                    new MarketModel { Name = "L’arbitre arbitrera-t-il en jaune ?", Position = 10, StartTimeUtc = new DateTime(2019, 12, 10, 21, 0, 0),
                        Selections = new List<SelectionModel>() {
                            new SelectionModel { Name = "Chelsea", IsYes = true,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/chelsea_fr.png",
                            },
                            new SelectionModel { Name = "Lille", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/lille.png",

                            },
                        }
                    }
                }

            });

            #endregion

            startDateTime = startDateTime.AddDays(1);
            #region Parie le 10 pour match 11 décembre LIGUE DES CHAMPIONS (PSG - Galatasaray, Bayern Munich – Tottenham, Bruges - Real Madrid)
            context.Tournaments.Add(new TournamentModel
            {
                Name = "Parie le 10 pour match 11 décembre LIGUE DES CHAMPIONS (PSG - Galatasaray, Bayern Munich – Tottenham, Bruges - Real Madrid)",
                StartTimeUtc = startDateTime,
                EndTimeUtc = startDateTime.AddDays(1),
                Markets = new List<MarketModel>() {
                    new MarketModel { Name = "Paris gagne le match ?", Position = 1, StartTimeUtc = new DateTime(2019, 12, 11, 21, 0, 0),
                        Selections = new List<SelectionModel>() {
                            new SelectionModel { Name = "PSG ", IsYes = true,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/paris.png",
                            },
                            new SelectionModel { Name = "Galatasaray", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/Galatasaray_fr.png",
                            },
                        }
                    },
                    new MarketModel { Name = "Y aura t-il moins de 3 buts dans le match ?", Position = 2, StartTimeUtc = new DateTime(2019, 12, 11, 21, 0, 0),
                        Selections = new List<SelectionModel>() {
                            new SelectionModel { Name = "PSG ", IsYes = true,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/paris.png",
                            },
                            new SelectionModel { Name = "Galatasaray", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/Galatasaray_fr.png",
                            },
                        }
                    },
                    new MarketModel { Name = "Y aura t-il plus que 9 corners dans le match ?", Position = 3, StartTimeUtc = new DateTime(2019, 12, 11, 21, 0, 0),
                        Selections = new List<SelectionModel>() {
                            new SelectionModel { Name = "PSG ", IsYes = true,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/paris.png",
                            },
                            new SelectionModel { Name = "Galatasaray", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/Galatasaray_fr.png",
                            },
                        }
                    },
                    new MarketModel { Name = "Tottenham gagne le match ?", Position = 4, StartTimeUtc = new DateTime(2019, 12, 11, 21, 0, 0),
                        Selections = new List<SelectionModel>() {
                             new SelectionModel { Name = "Bayern Munich", IsYes = true,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/bayern_munich_fr.png",
                            },
                            new SelectionModel { Name = "Tottenham", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/tottenham_fr.png",
                            },
                        }
                    },
                    new MarketModel { Name = "Y aura t-il un doublé marqué dans le match ?", Position = 5, StartTimeUtc = new DateTime(2019, 12, 11, 21, 0, 0),
                        Selections = new List<SelectionModel>() {
                             new SelectionModel { Name = "Bayern Munich", IsYes = true,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/bayern_munich_fr.png",
                            },
                            new SelectionModel { Name = "Tottenham", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/tottenham_fr.png",
                            },
                        }
                    },
                    new MarketModel { Name = "But entre la 1ere et la 15eme minute ?", Position = 6, StartTimeUtc = new DateTime(2019, 12, 11, 21, 0, 0),
                        Selections = new List<SelectionModel>() {
                             new SelectionModel { Name = "Bayern Munich", IsYes = true,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/bayern_munich_fr.png",
                            },
                            new SelectionModel { Name = "Tottenham", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/tottenham_fr.png",
                            },
                        }
                    },
                    new MarketModel { Name = "Madrid gagne le match ?", Position = 7, StartTimeUtc = new DateTime(2019, 12, 11, 21, 0, 0),
                        Selections = new List<SelectionModel>() {
                            new SelectionModel { Name = "Bruges", IsYes = true,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/bruges_fr.png",
                            },
                            new SelectionModel { Name = "Real Madrid", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/real-madrid_fr.png",
                            },
                        }
                    },
                    new MarketModel { Name = "1ere mi-temps plus prolifique que la 2eme ?", Position = 8, StartTimeUtc = new DateTime(2019, 12, 11, 21, 0, 0),
                        Selections = new List<SelectionModel>() {
                            new SelectionModel { Name = "Bruges", IsYes = true,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/bruges_fr.png",
                            },
                            new SelectionModel { Name = "Real Madrid", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/real-madrid_fr.png",
                            },
                        }
                    },
                    new MarketModel { Name = "Y aura t-il moins de 3 buts dans le match ?", Position = 9, StartTimeUtc = new DateTime(2019, 12, 11, 21, 0, 0),
                        Selections = new List<SelectionModel>() {
                            new SelectionModel { Name = "Bruges", IsYes = true,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/bruges_fr.png",
                            },
                            new SelectionModel { Name = "Real Madrid", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/real-madrid_fr.png",
                            },
                        }
                    },
                    new MarketModel { Name = "Y’aura t-il + de 4 minutes de temps additionnel à la fin du match ?", Position = 10, StartTimeUtc = new DateTime(2019, 12, 11, 21, 0, 0),
                        Selections = new List<SelectionModel>() {
                            new SelectionModel { Name = "Bruges", IsYes = true,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/bruges_fr.png",
                            },
                            new SelectionModel { Name = "Real Madrid", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/real-madrid_fr.png",
                            },
                        }
                    }
                }

            });
            #endregion

            startDateTime = new DateTime(2019, 12, 11, 18, 0, 0).ToUniversalTime();
            #region Parie le 11 pour match 12 décembre LIGUE EUROPA (Stade Rennais – Lazio Rome, Wolfsburg – Saint-Etienne)
            context.Tournaments.Add(new TournamentModel
            {
                Name = "Parie le 11 pour match 12 décembre LIGUE EUROPA (Stade Rennais – Lazio Rome, Wolfsburg – Saint-Etienne)",
                StartTimeUtc = startDateTime,
                EndTimeUtc = startDateTime.AddDays(1),
                Markets = new List<MarketModel>() {
                    new MarketModel { Name = "Rennes gagne le match ?", Position = 1, StartTimeUtc = new DateTime(2019, 12, 12, 18, 55, 0),
                        Selections = new List<SelectionModel>() {
                            new SelectionModel { Name = "Stade Rennais ", IsYes = true,
                                ImgUrl = " http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/rennes.png",
                            },
                            new SelectionModel { Name = "Lazio Rome", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/lazio_fr.png",
                            },
                        }
                    },
                    new MarketModel { Name = "Y aura t-il moins de 3 buts dans le match ?", Position = 2, StartTimeUtc = new DateTime(2019, 12, 12, 18, 55, 0),
                        Selections = new List<SelectionModel>() {
                            new SelectionModel { Name = "Stade Rennais ", IsYes = true,
                                ImgUrl = " http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/rennes.png",
                            },
                            new SelectionModel { Name = "Lazio Rome", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/lazio_fr.png",
                            },
                        }
                    },
                    new MarketModel { Name = "Y aura t-il plus que 7 corners dans le match ?", Position = 3, StartTimeUtc = new DateTime(2019, 12, 12, 18, 55, 0),
                        Selections = new List<SelectionModel>() {
                            new SelectionModel { Name = "Stade Rennais ", IsYes = true,
                                ImgUrl = " http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/rennes.png",
                            },
                            new SelectionModel { Name = "Lazio Rome", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/lazio_fr.png",
                            },
                        }
                    },
                    new MarketModel { Name = "But dans le temps additionnel (d’une des deux mi-temps) ?", Position = 4, StartTimeUtc = new DateTime(2019, 12, 12, 18, 55, 0),
                        Selections = new List<SelectionModel>() {
                            new SelectionModel { Name = "Stade Rennais ", IsYes = true,
                                ImgUrl = " http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/rennes.png",
                            },
                            new SelectionModel { Name = "Lazio Rome", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/lazio_fr.png",
                            },
                        }
                    },
                    new MarketModel { Name = "Y aura t-il un doublé marqué dans le match ?", Position = 5, StartTimeUtc = new DateTime(2019, 12, 12, 18, 55, 0),
                        Selections = new List<SelectionModel>() {
                            new SelectionModel { Name = "Stade Rennais ", IsYes = true,
                                ImgUrl = " http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/rennes.png",
                            },
                            new SelectionModel { Name = "Lazio Rome", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/lazio_fr.png",
                            },
                        }
                    },
                    new MarketModel { Name = "Saint-Etienne  gagne le match ?", Position = 6, StartTimeUtc = new DateTime(2019, 12, 12, 21, 0, 0),
                        Selections = new List<SelectionModel>() {
                            new SelectionModel { Name = "Wolfsburg", IsYes = true,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/wolfsburg_fr.png",
                            },
                            new SelectionModel { Name = "Saint-Etienne", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/saintetienne.png",
                            },
                        }
                    },
                    new MarketModel { Name = "Y aura-t-il plus de buts en 1ere mi-temps qu’en deuxieme ?", Position = 7, StartTimeUtc = new DateTime(2019, 12, 12, 21, 0, 0),
                        Selections = new List<SelectionModel>() {
                            new SelectionModel { Name = "Wolfsburg", IsYes = true,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/wolfsburg_fr.png",
                            },
                            new SelectionModel { Name = "Saint-Etienne", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/saintetienne.png",
                            },
                        }
                    },
                    new MarketModel { Name = "Y aura t-il moins de 3 buts dans le match ?", Position = 8, StartTimeUtc = new DateTime(2019, 12, 12, 21, 0, 0),
                        Selections = new List<SelectionModel>() {
                            new SelectionModel { Name = "Wolfsburg", IsYes = true,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/wolfsburg_fr.png",
                            },
                            new SelectionModel { Name = "Saint-Etienne", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/saintetienne.png",
                            },
                        }
                    },
                    new MarketModel { Name = "Y’aura-t-il 6 changements effectués dans le matchs (sur 6 possibles)", Position = 9, StartTimeUtc = new DateTime(2019, 12, 12, 21, 0, 0),
                        Selections = new List<SelectionModel>() {
                            new SelectionModel { Name = "Wolfsburg", IsYes = true,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/wolfsburg_fr.png",
                            },
                            new SelectionModel { Name = "Saint-Etienne", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/saintetienne.png",
                            },
                        }
                    },
                    new MarketModel { Name = "Y aura t-il un but marqué autrement que du pied ou de la tête ?", Position = 10, StartTimeUtc = new DateTime(2019, 12, 12, 21, 0, 0),
                        Selections = new List<SelectionModel>() {
                            new SelectionModel { Name = "Wolfsburg", IsYes = true,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/wolfsburg_fr.png",
                            },
                            new SelectionModel { Name = "Saint-Etienne", IsYes = false,
                                ImgUrl = "http://img.cdn.betclic.com/img2/common/creas/footballFlags/png/saintetienne.png",
                            },
                        }
                    }
                }

            });
            #endregion
            context.SaveChanges();
        }
    }
}
