using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using ThermoBet.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ThermoBet.Core.Interfaces;

namespace ThermoBet.SQLServer.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ThermoBetContext _thermoBetContext;

        public DashboardService(ThermoBetContext thermoBetContext)
        {
            _thermoBetContext = thermoBetContext;
        }

        public async Task<Dictionary<DateTime, int>> GetUniqueUserLoginByDay(int nbDay)
        {
            var startDate = DateTime.Today.AddDays(nbDay * -1);

            var result = await _thermoBetContext
                .LoginHistories
                .Where(x => x.LoginDateUtc >= startDate)
                .Include(x => x.User)
                .GroupBy(x => x.LoginDateUtc.Date)
                .ToListAsync();

            var toReturn = result
                .Select(x => new
                {
                    Date = x.Key,
                    Count = x.Select(x => x.User.Id).Distinct().Count()
                })
                .ToDictionary(x => x.Date, x => x.Count);

            for (var i = 0; i < nbDay; i++)
            {
                var key = DateTime.Today.AddDays(i*-1);
                if (!toReturn.ContainsKey(key))
                    toReturn.Add(key, 0);
            }
            return toReturn;
        }

        public async Task<Dictionary<DateTime, int>> GetUserLoginByDay(int nbDay)
        {
            var startDate = DateTime.Today.AddDays(nbDay * -1);

            var result = await _thermoBetContext
                .LoginHistories
                .Where(x => x.LoginDateUtc >= startDate)
                .Include(x => x.User)
                .GroupBy(x => x.LoginDateUtc.Date)
                .ToListAsync();

            var toReturn = result
                
                .Select(x => new
                {
                    Date = x.Key,
                    Count = x.Select(x => x.User.Id).Count()
                })
                .ToDictionary(x => x.Date, x => x.Count);

            for (var i = 0; i < nbDay; i++)
            {
                var key = DateTime.Today.AddDays(i * -1);
                if (!toReturn.ContainsKey(key))
                    toReturn.Add(key, 0);
            }
            return toReturn;
        }
    }
}
