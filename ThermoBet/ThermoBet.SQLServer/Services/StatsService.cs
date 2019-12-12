using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ThermoBet.Core.Models;
using ThermoBet.Data;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace ThermoBet.Data.Services
{
    public class StatsService : IStatsService
    {
        private readonly ThermoBetContext _thermoBetContext;

        public StatsService(ThermoBetContext thermoBetContext)
        {
            _thermoBetContext = thermoBetContext;
        }

        public async Task<StatsModel> GetByUserIdAsync(int userId, bool fixBugIssue = true)
        {
            var userBets = _thermoBetContext
                .Bets
                .Where(b => b.UserId == userId);
            var succeedSwipesCount = _thermoBetContext
                .Users
                .SingleOrDefault(u => u.Id == userId)?
                .GlobalPoints;

            var winningsBets = userBets.Count(ub => fixBugIssue ? (ub.Market.WinningSelectionId != ub.Selection.Id && ub.Market.WinningSelectionId.HasValue) : ub.Market.WinningSelectionId == ub.Selection.Id);
            var resultedBetCount = userBets.Count(ub => ub.Market.WinningSelectionId.HasValue);

            var userStats = new UserStatsModel
            {
                UserId = userId,
                AllSwipesCount = userBets.Count(),
                MonthlySwipesCount = userBets.Where(b => b.DateUtc.AddMonths(1) > DateTime.UtcNow).Count(),
                SucceedSwipesCount = succeedSwipesCount ?? 0,
                SucceedPercentage = resultedBetCount == 0 ? 0 : (int)((decimal)winningsBets / resultedBetCount * 100)
            };

            var users = (await _thermoBetContext
                .Users
                .OrderByDescending(u => u.GlobalPoints)
                .ToListAsync());

            //Calculate first 3 places
            var positions = users
                .Select((u, i) => new { User = u, Index = i })
                .Take(3)
                .Select(u => GetStatsPositionModel(u.User, u.Index + 1))
                .ToList();

            //Add 3 next positions
            if (positions.Any(p => p.UserId == userId))
            {
                var nextPositions = users
                .Select((u, i) => new { User = u, Index = i })
                .Skip(3)
                .Take(3)
                .Select(u => GetStatsPositionModel(u.User, u.Index + 1))
                .ToList();
                positions.AddRange(nextPositions);
            }
            else
            {
                var user = users
                .Select((u, i) => new { UserId = u.Id, Index = i })
                .SingleOrDefault(u => u.UserId == userId);

                if (user != null)
                {
                    var userPosition = user.Index + 1;

                    var nextPositions = users
                    .Select((u, i) => new { User = u, Index = i })
                    .Skip(userPosition == 4 ? 3 : (userPosition - 2))
                    .Take(3)
                    .Select(u => GetStatsPositionModel(u.User, u.Index + 1))
                    .ToList();
                    positions.AddRange(nextPositions);
                }
            }

            var stats = new StatsModel
            {
                UserStats = userStats,
                Positions = positions
            };

            return stats;
        }

        public void ResetPoints()
        {
            var users = _thermoBetContext.Users;
            foreach(var user in users)
            {
                user.CurrentPoints = 0;
                user.GlobalPoints = 0;
            }
            _thermoBetContext.SaveChanges();
        }

        public async Task Compute(int tournamentId, bool fixBugIssue = true)
        {
            var bets = _thermoBetContext.Bets
                .Include(b => b.Selection)
                .Include(b => b.Market)
                .Include(b => b.User)
                .Where(b => b.TournamentId == tournamentId)
                .OrderBy(b => b.UserId)
                .OrderBy(b => b.DateUtc)
                .ToList();

            foreach (var bet in bets)
            {
                var user = _thermoBetContext.Users.SingleOrDefault(u => u.Id == bet.User.Id);
                if (user != null)
                {
                    if (fixBugIssue ? (bet.Selection.Id != bet.Market.WinningSelectionId && bet.Market.WinningSelectionId.HasValue) : bet.Selection.Id == bet.Market.WinningSelectionId)
                    {
                        //Winning bet
                        user.CurrentPoints++;
                    }
                    else
                    {
                        user.CurrentPoints = 0;
                    }

                    if (user.GlobalPoints < user.CurrentPoints)
                        user.GlobalPoints = user.CurrentPoints;
                }
            }

            await _thermoBetContext.SaveChangesAsync();
        }

        private StatsPositionModel GetStatsPositionModel(UserModel u, int position)
        {
            return new StatsPositionModel
            {
                UserId = u.Id,
                Position = position,
                Pseudo = u.Pseudo ?? $"Joueur_{u.Id}",
                Score = u.GlobalPoints,
                Avatar = u.Avatar
            };
        }
    }
}