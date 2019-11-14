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

        public async Task<StatsModel> GetByUserIdAsync(int userId)
        {
            var userBets = _thermoBetContext
                    .Bets
                    .Where(b => b.UserId == userId);
            var succeedSwipesCount = _thermoBetContext
                .Users
                .SingleOrDefault(u => u.Id == userId)
                .GlobalPoints;

            var winningsBets = userBets.Count(ub => ub.Market.WinningSelectionId == ub.Selection.Id);
            var resultedBetCount = userBets.Count(ub => ub.Market.WinningSelectionId.HasValue);

            var userStats = new UserStatsModel
            {
                UserId = userId,
                AllSwipesCount = userBets.Count(),
                MonthlySwipesCount = userBets.Where(b => b.DateUtc.AddMonths(1) > DateTime.UtcNow).Count(),
                SucceedSwipesCount = succeedSwipesCount,
                SucceedPercentage = resultedBetCount == 0 ? 0 : (int)((decimal)winningsBets / resultedBetCount * 100)
            };

            var users = (await _thermoBetContext
                .Users
                .Where(u => u.GlobalPoints > 0)
                .OrderByDescending(u => u.GlobalPoints)
                .ToListAsync());

            //Calculate first 3 places
            var positions = users
                .Select((u, i) => new { User = u, Index = i })
                .Take(3)
                .Select(u => new StatsPositionModel
                {
                    UserId = u.User.Id,
                    Position = u.Index + 1,
                    Pseudo = u.User.Pseudo ?? $"Joueur_{u.User.Id}",
                    Score = u.User.GlobalPoints
                })
                .ToList();

            //Add 3 next positions
            if(positions.Any(p => p.UserId == userId))
            {
                var nextPositions = users
                .Select((u, i) => new { User = u, Index = i })
                .Skip(3)
                .Take(3)
                .Select(u => new StatsPositionModel
                {
                    UserId = u.User.Id,
                    Position = u.Index + 1,
                    Pseudo = u.User.Pseudo ?? $"Joueur_{u.User.Id}",
                    Score = u.User.GlobalPoints
                })
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
                    .Select(u => new StatsPositionModel
                    {
                        UserId = u.User.Id,
                        Position = u.Index + 1,
                        Pseudo = u.User.Pseudo ?? $"Joueur_{u.User.Id}",
                        Score = u.User.GlobalPoints
                    })
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
    }
}