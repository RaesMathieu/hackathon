//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using ThermoBet.Core.Models;
//using ThermoBet.Data;
//using System.Linq;
//using System;

//namespace ThermoBet.Data.Services
//{
//    public class StatsService : ITournamentService
//    {
//        private readonly ThermoBetContext _thermoBetContext;

//        public TournamentService(ThermoBetContext thermoBetContext)
//        {
//            _thermoBetContext = thermoBetContext;
//        }

//        public async Task Get(int userId)
//        {
//            var userBets = _thermoBetContext
//                    .Bets
//                    .Where(b => b.UserId == userId);
//            await _thermoBetContext.SaveChangesAsync();
//        }
//    }
//}