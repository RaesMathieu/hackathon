using Arena42.Models.DTO;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Arena42.Services;
using Arena42.Services.Repository;
using System.Linq;

namespace Arena42.Controllers
{
    public class TournamentController : ApiController
    {
        [Route("api/tournaments")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<Tournament>))]
        public IHttpActionResult GetTournanents()
        {
            using (var db = new Adriana42Context())
            {
                var repository = new Repository<Models.Tournament>(db);

                var tournamentList = repository.All().Select(x => new Tournament
                {
                    Description = x.Description,
                    StartTimeUtc = x.StartTimeUtc,
                    EndTimeUtc = x.EndTimeUtc,
                    Name = x.Name,
                    ImgUrl = x.ImgUrl,
                    Id = x.Id,
                    Markets = x.Markets.Select(y => new Market
                    {
                        Id = y.Id,
                        Name = y.Name,
                        ImgUrl = y.ImgUrl,
                        Selections = y.Selections.Select(s => new Selection
                        {
                            Id = s.Id,
                            ImgUrl = s.ImgUrl,
                            Name = s.Name,
                            Odds = s.Odds.ToString()
                        })
                    })
                });
                return Ok(new List<Tournament>(tournamentList));
            }
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

        // GET api/values/5
        [Route("api/tournament/{tournamentid}/leaderboard")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(LeaderBoard))]
        public IHttpActionResult GetLeaderBoardByTournamentId(int tournamentid)
        {
            using (var db = new Adriana42Context())
            {
                var tournamentRepository = new Repository<Models.Tournament>(db);
                var marketRepository = new Repository<Models.Market>(db);
                var betRepository = new Repository<Models.Bet>(db);

                var bets = betRepository.Find(b => b.TournamentId == tournamentid);
                return Ok(new LeaderBoard
                {
                    TournamentId = tournamentid,
                    Ranking = bets.GroupBy(b => b.UserId).Select(b => new LeaderBoardRanking
                    {
                        UserName = b.Key.ToString(),
                        Score = b.Count()
                    }).ToList()
                });
            }
        }

        [Route("api/tournament/{tournamentid}/Result")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(TournamentResult))]
        public IHttpActionResult GetResultByTournamentId(int tournamentId)
        {
            using (var db = new Adriana42Context())
            {
                var tournamentRepository = new Repository<Models.Tournament>(db);
                var marketRepository = new Repository<Models.Market>(db);
                var betRepository = new Repository<Models.Bet>(db);
                var bets = betRepository.Find(b => b.TournamentId == tournamentId);
                var tournament = tournamentRepository.GetById(t => t.Id == tournamentId);
                if (tournament == null)
                    return NotFound();

                var tournamentResult = new TournamentResult
                {
                    Id = tournament.Id,
                    Description = tournament.Description,
                    StartTimeUtc = tournament.StartTimeUtc,
                    EndTimeUtc = tournament.EndTimeUtc,
                    ImgUrl = tournament.ImgUrl,
                    Name = tournament.Name
                };

                tournamentResult.MarketResults = bets.Select(b => new MarketResult
                {
                    MarketId = b.MarketId,
                    ChosenSelectionId = b.SelectionId
                }).ToList();
                tournamentResult.MarketResults.ForEach(x =>
                {
                    var market = marketRepository.GetById(x.MarketId);
                    if (market != null)
                    {
                        x.Selections = market.Selections?.Select(s => new Selection
                        {
                            Id = s.Id,
                            ImgUrl = s.ImgUrl,
                            Name = s.Name,
                            Odds = s.Odds.ToString(),
                            Result = s.Result
                        });
                        x.WinningSelectionId = x.Selections.FirstOrDefault(s => s.Result == true)?.Id;
                        x.Name = market.Name;
                        x.ImgUrl = market.ImgUrl;
                    };
                });

                return Ok(tournamentResult);
            }
        }

        public int UserId
        {
            get { return int.Parse(this.Request.Headers.GetValues("X-CLIENT").FirstOrDefault()); }
        }

        [Route("api/tournament/{tournamentid}/market/{marketid}/selection/{selectionid}")]
        [HttpPost]
        public IHttpActionResult Bet(int tournamentId, int marketId, int selectionId)
        {
            using (var db = new Adriana42Context())
            {
                var repository = new Repository<Models.Bet>(db);
                var bet = new Models.Bet
                {
                    Date = DateTime.UtcNow,
                    TournamentId = tournamentId,
                    MarketId = marketId,
                    SelectionId = selectionId,
                    UserId = UserId
                };
                repository.Add(bet);
                db.SaveChanges();
                return Ok();
            }
        }

        [Route("api/result")]
        [HttpPut]
        public IHttpActionResult Put([FromBody]ResultRequest request)
        {
            using (var db = new Adriana42Context())
            {
                var selectionsRepository = new Repository<Models.Selection>(db);
                var selection = selectionsRepository.GetById(request.SelectionId);
                if (selection == null)
                    return NotFound();
                selection.Result = request.Result;

                var repository = new Repository<Models.Bet>(db);
                var bets = repository.Find(b => b.SelectionId == request.SelectionId).ToList();
                bets.ForEach(b => b.Result = request.Result);

                db.SaveChanges();

                return Ok();
            }
        }
    }
}
