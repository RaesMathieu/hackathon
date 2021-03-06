﻿using Arena42.Models.DTO;
using Arena42.Services;
using Arena42.Services.Repository;
using System.Linq;
using System.Web.Mvc;

namespace Arena42.Controllers
{
    public class RankingController : Controller
    {
        public ActionResult Results(int tournamentId, int? token)
        {
            using (var db = new Adriana42Context())
            {
                var tournamentRepository = new Repository<Models.Tournament>(db);
                var marketRepository = new Repository<Models.Market>(db);
                var betRepository = new Repository<Models.Bet>(db);
                var bets = betRepository.Find(b => b.TournamentId == tournamentId && (!token.HasValue || token.Value == b.UserId));
                var tournament = tournamentRepository.GetById(t => t.Id == tournamentId);
                if (tournament == null)
                    return HttpNotFound();

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

                return View(tournamentResult); ;
            }
        }

        public ActionResult LeaderBoard(int tournamentId)
        {
            using (var db = new Adriana42Context())
            {
                var tournamentRepository = new Repository<Models.Tournament>(db);
                var marketRepository = new Repository<Models.Market>(db);
                var betRepository = new Repository<Models.Bet>(db);

                var bets = betRepository.Find(b => b.TournamentId == tournamentId);
                return View(new LeaderBoard
                {
                    TournamentId = tournamentId,
                    Ranking = bets.GroupBy(b => b.UserId).Select(b => new LeaderBoardRanking
                    {
                        UserName = b.Key.ToString(),
                        Score = b.Count()
                    }).ToList()
                });
            }
        }
    }
}
