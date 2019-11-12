using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ThermoBet.Core.Models;
using ThermoBet.Data;
using System.Linq;

namespace ThermoBet.Data.Services
{
    public class TournamentService : ITournamentService
    {
        private readonly ThermoBetContext _thermoBetContext;

        public TournamentService(ThermoBetContext thermoBetContext)
        {
            _thermoBetContext = thermoBetContext;
        }

        public async Task Update(TournamentModel tournament)
        {
            _thermoBetContext
                    .Tournaments
                    .Update(tournament);
            await _thermoBetContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TournamentModel>> GetAllAsync()
        {
            return await _thermoBetContext
                    .Tournaments
                    .Include(x => x.Markets)
                        .ThenInclude(market => market.Selections)
                    .ToListAsync();
        }

        public async Task<TournamentModel> GetAsync(int id)
        {
            return await _thermoBetContext
                    .Tournaments
                    .Include(x => x.Markets)
                        .ThenInclude(market => market.Selections)
                    .FirstAsync(x => x.Id == id);
        }

        public IQueryable<TournamentModel> GetAll()
        {
            return  _thermoBetContext
                    .Tournaments;
                    
        }
    }
}