using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ThermoBet.Core.Models;
using ThermoBet.Data;
using System.Linq;
using System;

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

        public async Task<IEnumerable<TournamentModel>> GetCurrentsAsync()
        {
            return await _thermoBetContext
                    .Tournaments
                    .Include(x => x.Markets)
                        .ThenInclude(market => market.Selections)
                    .Where(x => x.StartTimeUtc <= DateTime.UtcNow && x.EndTimeUtc >= DateTime.UtcNow)
                    .ToListAsync();
        }

        public async Task<IEnumerable<TournamentModel>> GetAlreadyStartedAsync(int lastNumber)
        {
            return await _thermoBetContext
                    .Tournaments
                    .Include(x => x.Markets)
                        .ThenInclude(market => market.Selections)
                    .Where(x => x.StartTimeUtc <= DateTime.UtcNow)
                    .OrderByDescending(x => x.StartTimeUtc)
                    .Take(lastNumber)
                    .ToListAsync();
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
            return _thermoBetContext
                    .Tournaments;

        }

        public async Task BetAsync(int userId, int tournamentId, int marketId, int selectionId)
        {
            var user = await _thermoBetContext.
                    Users.SingleAsync(x => x.Id == userId);

            var bets = await _thermoBetContext.Entry(user)
                .Collection(x => x.Bets)
                .Query()
                .Where(x => x.Tournament.Id == tournamentId && x.Market.Id == marketId)
                .SingleOrDefaultAsync();

            var selection = await _thermoBetContext.Selections.SingleAsync(x => x.Id == selectionId);
            if (bets != null)
            {
                bets.Selection = selection;
                bets.DateUtc = DateTime.UtcNow;
            }
            else
            {
                await _thermoBetContext.Bets.AddAsync(new BetModel
                {
                    DateUtc = DateTime.UtcNow,
                    MarketId = marketId,
                    TournamentId = tournamentId,
                    User = user,
                    Selection = selection
                });
            }

            await _thermoBetContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<BetModel>> GetBetAsync(int userId, int tournamentId)
        {
            var user = await _thermoBetContext.
                    Users.SingleAsync(x => x.Id == userId);

            var bets = await _thermoBetContext.Entry(user)
                .Collection(x => x.Bets)
                .Query()
                .Include(x => x.Market)
                .Include(x => x.Selection)
                .Include(x => x.Tournament)
                .Where(x => x.Tournament.Id == tournamentId)
                .ToListAsync();

            return bets;
        }
    }
}