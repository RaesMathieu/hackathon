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
using System.Linq.Expressions;

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
            throw new NotImplementedException();
            //using (var db = new Adriana42Context())
            //{
            //    var tournamentRepository = new Repository<Models.Tournament>(db);
            //    var marketRepository = new Repository<Models.Market>(db);
            //    var betRepository = new Repository<Models.Bet>(db);

            //    var bets = betRepository.Find(b => b.Tournament.Id == tournamentid);
            //    return Ok(new LeaderBoard
            //    {
            //        TournamentId = tournamentid,
            //        Ranking = bets.GroupBy(b => b.User.Id).Select(b => new LeaderBoardRanking
            //        {
            //            UserName = b.Key.ToString(),
            //            Score = b.Count()
            //        }).ToList()
            //    });
            //}
        }

        [Route("api/tournament/{tournamentid}/Result")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(TournamentResult))]
        public IHttpActionResult GetResultByTournamentId(int tournamentId)
        {
            //throw new NotImplementedException();

            using (var db = new Adriana42Context())
            {
                var tournamentRepository = new Repository<Models.Tournament>(db);
                var betRepository = new Repository<Models.Bet>(db);

                var allTournaments = tournamentRepository.All().Where(x => x.Id == tournamentId).ToList();
                var userBets =  betRepository.All().Where(x => x.User.Id == UserId && x.Tournament.Id == tournamentId).ToList();


                var tournam = allTournaments.Select(x => new Models.DTO.TournamentResult
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImgUrl = x.ImgUrl,
                    Description = x.Description,
                    StartTimeUtc = x.StartTimeUtc,
                    EndTimeUtc = x.EndTimeUtc,
                    MarketResults = new List<MarketResult>(userBets.Select(z => new Models.DTO.MarketResult
                    {
                        Market = new Market
                        {
                            Id = z.Market.Id,
                            Name = z.Market.Name,
                            ImgUrl = z.Market.ImgUrl,
                            Selections = new List<Selection>(z.Market.Selections.Select(y => new Models.DTO.Selection
                            {
                                Id = y.Id,
                                Name = y.Name,
                                ImgUrl = y.ImgUrl,
                                Odds = y.Odds.ToString(),
                                Result = y.Result
                            }))                            
                        },
                        ChosenSelection = new Models.DTO.Selection
                        {
                            Id = z.Selection.Id,
                            Name = z.Selection.Name,
                            ImgUrl = z.Selection.ImgUrl,
                            Result = z.Selection.Result,
                            Odds = z.Selection.Odds.ToString()
                        },
                        WinningSelection = z.Market.Selections.Where(xx => xx.Result == true).Select(xxx => new Models.DTO.Selection
                        {
                            Name = xxx.Name,
                            ImgUrl = xxx.ImgUrl,
                            Id = xxx.Id,
                            Result = xxx.Result,
                            Odds = xxx.Odds.ToString()
                        }).FirstOrDefault()
                    }))
                    
                }).FirstOrDefault();

                return Ok(tournam);

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
                var repositoryTournament = new Repository<Models.Tournament>(db);
                var repositoryMarket = new Repository<Models.Market>(db);
                var repositorySelection = new Repository<Models.Selection>(db);
                var repositoryUser = new Repository<Models.User>(db);


                var repository = new Repository<Models.Bet>(db);
                var bet = new Models.Bet
                {
                    Date = DateTime.UtcNow,
                    Tournament = repositoryTournament.GetById(tournamentId),
                    Market = repositoryMarket.GetById(marketId),
                    Selection = repositorySelection.GetById(selectionId),
                    User = repositoryUser.GetById(UserId)
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
                var bets = repository.Find(b => b.Selection.Id == request.SelectionId).ToList();
                bets.ForEach(b => b.Result = request.Result);

                db.SaveChanges();

                return Ok();
            }
        }
    }
}
