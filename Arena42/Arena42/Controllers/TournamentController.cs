using Arena42.Models.DTO;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Arena42.Controllers
{
    public class TournamentController : ApiController
    {
        // GET api/values/5
        [Route("api/tournaments")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<Tournament>))]
        public IHttpActionResult GetTournanents()
        {
            List<Tournament> tournamentList = GetTournamentsStub();
            return Ok(new List<Tournament>(tournamentList));
        }

        private static List<Tournament> GetTournamentsStub()
        {
            return new List<Tournament>
            {
                new Tournament
                {
                    Id = 1,
                    Description = "Premier tournoi du betclic hackathon !",
                    StartTimeUtc = DateTime.UtcNow,
                    EndTimeUtc = DateTime.UtcNow,
                    ImgUrl = "http://www.footmercato.net/images/a/benjamin-lecomte-plait-beaucoup-a-monaco_257228.jpg",
                    Name = "Ligue 1 journée 1",
                    Market = new List<Market>
                    {
                        GetMarketStub(1),
                        GetMarketStub(2),
                        GetMarketStub(3),
                        GetMarketStub(4)
                    }
                }
            };
        }

        //// GET api/values/5
        //[Route("api/tournament/{id}")]
        //[SwaggerResponse(HttpStatusCode.OK, Type = typeof(Tournament))]
        //public IHttpActionResult GetTournamentById(int id)
        //{
        //    return Ok(new Tournament
        //    {
        //        Id = id,
        //        StartTimeUtc = DateTime.Now,
        //        EndTimeUtc = DateTime.Now.AddDays(1)
        //    });
        //}

        //// GET api/values/5
        //[Route("api/tournament/{tournamentid}/leaderboard")]
        //[SwaggerResponse(HttpStatusCode.OK, Type = typeof(LeaderBoard))]
        //public IHttpActionResult GetLeaderBoardByTournamentId(int tournamentid)
        //{
        //    return Ok(new LeaderBoard());²
        //}

        // GET api/values/5
        [Route("api/tournament/{tournamentid}/Result")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(TournamentResult))]
        public IHttpActionResult GetResultByTournamentId(int tournamentid)
        {
            return Ok(GetTournamentResultStub());
        }

        private TournamentResult GetTournamentResultStub()
        {
            return new TournamentResult
            {
                Id = 1,
                Description = "Premier tournoi du betclic hackathon !",
                StartTimeUtc = DateTime.UtcNow,
                EndTimeUtc = DateTime.UtcNow,
                ImgUrl = "http://www.footmercato.net/images/a/benjamin-lecomte-plait-beaucoup-a-monaco_257228.jpg",
                Name = "Ligue 1 journée 1",
                Market = new List<Market>
                    {
                        GetMarketStub(1),
                        GetMarketStub(2),
                        GetMarketStub(3),
                        GetMarketStub(4)
                    },
                MarketResults = new List<MarketResult>
                {
                    new MarketResult
                    {
                        BetclicMarketId = "1",
                        Id = 1,
                        ImgUrl = "https://image.freepik.com/psd-gratuit/conception-fond-resume_1297-82.jpg",
                        Name = "Match winner",
                        Selections = new List<Selection>
                        {
                            new Selection
                            {
                                Id = 2,
                                ImgUrl = "https://upload.wikimedia.org/wikipedia/fr/thumb/8/86/Paris_Saint-Germain_Logo.svg/1024px-Paris_Saint-Germain_Logo.svg.png",
                                Name = "PSG",
                                Odds = "2"
                            },
                            new Selection
                            {
                                Id = 4,
                                ImgUrl = "https://upload.wikimedia.org/wikipedia/fr/4/43/Logo_Olympique_de_Marseille.svg",
                                Name = "OM",
                                Odds = "3"
                            },
                        },
                        ChosenSelectionId = 2,
                        WinningSelectionId = 4
                    },
                    new MarketResult
                    {
                        BetclicMarketId = "2",
                        Id = 2,
                        ImgUrl = "https://image.freepik.com/psd-gratuit/conception-fond-resume_1297-82.jpg",
                        Name = "Match winner",
                        Selections = new List<Selection>
                        {
                            new Selection
                            {
                                Id = 6,
                                ImgUrl = "https://upload.wikimedia.org/wikipedia/fr/thumb/8/86/Paris_Saint-Germain_Logo.svg/1024px-Paris_Saint-Germain_Logo.svg.png",
                                Name = "PSG",
                                Odds = "2"
                            },
                            new Selection
                            {
                                Id = 8,
                                ImgUrl = "https://upload.wikimedia.org/wikipedia/fr/4/43/Logo_Olympique_de_Marseille.svg",
                                Name = "OM",
                                Odds = "3"
                            },
                        },
                        ChosenSelectionId = 6,
                        WinningSelectionId = 6
                    }
                }
            };
        }

        // POST api/values
        [Route("api/tournament/{tournamentid}/market/{marketid}/selection/{selectionid}")]
        public IHttpActionResult Post(int tournamentId, int marketId, int selectionId)
        {
            return Ok();
        }

        private static Market GetMarketStub(int id)
        {
            return new Market
            {
                BetclicMarketId = id.ToString(),
                Id = id,
                Name = "Match winner",
                ImgUrl = "https://image.freepik.com/psd-gratuit/conception-fond-resume_1297-82.jpg",
                Selections = new List<Selection>
                {
                    new Selection
                    {
                        Id = id * 2,
                        ImgUrl = "https://upload.wikimedia.org/wikipedia/fr/thumb/8/86/Paris_Saint-Germain_Logo.svg/1024px-Paris_Saint-Germain_Logo.svg.png",
                        Name = "PSG",
                        Odds = "2"
                    },
                    new Selection
                    {
                        Id = id * 2,
                        ImgUrl = "https://upload.wikimedia.org/wikipedia/fr/4/43/Logo_Olympique_de_Marseille.svg",
                        Name = "OM",
                        Odds = "3"
                    },
                }
            };
        }
    }
}
