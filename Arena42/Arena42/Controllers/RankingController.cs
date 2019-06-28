using System;
using Arena42.Models.DTO;
using Arena42.Services;
using Arena42.Services.Repository;
using System.Linq;
using System.Web.Mvc;

namespace Arena42.Controllers
{
    public class RankingController : Controller
    {
        public int UserId
        {
            get { return int.Parse(this.Request.Headers.GetValues("X-CLIENT").FirstOrDefault()); }
        }

        public ActionResult Results(int tournamentId)
        {
            throw  new NotImplementedException();
            //using (var db = new Adriana42Context())
            //{
            //    var betRepository = new Repository<Models.Bet>(db);

            //    betRepository.Find(x => x.Tournament.Id == tournamentId && x.User.Id == UserId)


            //}
            //using (var db = new Adriana42Context())
            //{
            //    var tournamentRepository = new Repository<Models.Tournament>(db);
            //    var marketRepository = new Repository<Models.Market>(db);
            //    var betRepository = new Repository<Models.Bet>(db);
            //    var bets = betRepository.Find(b => b.Tournament.Id == tournamentId);
            //    var tournament = tournamentRepository.GetById(t => t.Id == tournamentId);
            //    if (tournament == null)
            //        return HttpNotFound();

                //    var tournamentResult = new TournamentResult
                //    {
                //        Id = tournament.Id,
                //        Description = tournament.Description,
                //        StartTimeUtc = tournament.StartTimeUtc,
                //        EndTimeUtc = tournament.EndTimeUtc,
                //        ImgUrl = tournament.ImgUrl,
                //        Name = tournament.Name
                //    };

                //    tournamentResult.MarketResults = bets.Select(b => new MarketResult
                //    {
                //        MarketId = b.Market.Id,
                //        ChosenSelectionId = b.Selection.Id
                //    }).ToList();
                //    tournamentResult.MarketResults.ForEach(x =>
                //    {
                //        var market = marketRepository.GetById(x.Market.Id);
                //        if (market != null)
                //        {
                //            x.Selections = market.Selections?.Select(s => new Selection
                //            {
                //                Id = s.Id,
                //                ImgUrl = s.ImgUrl,
                //                Name = s.Name,
                //                Odds = s.Odds.ToString(),
                //                Result = s.Result
                //            });
                //            x.WinningSelectionId = x.Selections.FirstOrDefault(s => s.Result == true)?.Id;
                //            x.Name = market.Name;
                //            x.ImgUrl = market.ImgUrl;
                //        };
                //    });

                //    return View(tournamentResult); ;
                //}
        }

        public ActionResult LeaderBoard(int tournamentId)
        {
            throw new NotImplementedException();
            //using (var db = new Adriana42Context())
            //{
            //    var tournamentRepository = new Repository<Models.Tournament>(db);
            //    var marketRepository = new Repository<Models.Market>(db);
            //    var betRepository = new Repository<Models.Bet>(db);

            //    var bets = betRepository.Find(b => b.Tournament.Id == tournamentId);
            //    return View(new LeaderBoard
            //    {
            //        TournamentId = tournamentId,
            //        Ranking = bets.GroupBy(b => b.User.Id).Select(b => new LeaderBoardRanking
            //        {
            //            UserName = b.Key.ToString(),
            //            Score = b.Count()
            //        }).ToList()
            //    });
            //}
        }
    }
}
