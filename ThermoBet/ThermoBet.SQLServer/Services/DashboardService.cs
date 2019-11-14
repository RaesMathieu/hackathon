using System;
using System.Linq;
using System.Collections.Generic;
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

            var toReturn = await _thermoBetContext
                .LoginHistories
                .Where(x => x.LoginDateTimeUtc >= startDate)
                .Select(x => new { Date = x.LoginDateTimeUtc.Date, UserId= x.User.Id })
                .Distinct()
                .GroupBy(x => x.Date)
                .Select(x => new
                {
                    Date = x.Key,
                    Count = x.Count()
                })
                .ToDictionaryAsync(x => x.Date, x => x.Count);

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

            var toReturn = await _thermoBetContext
                .LoginHistories
                .Where(x => x.LoginDateTimeUtc >= startDate)
                .GroupBy(x => x.LoginDateTimeUtc.Date)
                .Select(x => new {
                    Date = x.Key,
                    Count = x.Count()
                })
                .ToDictionaryAsync(x => x.Date, x => x.Count);

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
