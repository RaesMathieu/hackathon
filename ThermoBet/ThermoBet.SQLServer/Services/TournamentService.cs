using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ThermoBet.Core.Models;
using ThermoBet.Data;
using System.Linq;
using System;
using ThermoBet.Core.Interfaces;
using ThermoBet.Core.Exception;

namespace ThermoBet.Data.Services
{
    public class TournamentService : ITournamentService
    {
        private readonly ThermoBetContext _thermoBetContext;
        private readonly IConfigurationService _configurationService;

        public TournamentService(
            ThermoBetContext thermoBetContext,
            IConfigurationService configurationService)
        {
            _thermoBetContext = thermoBetContext;
            this._configurationService = configurationService;
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
            var dateTime = await _configurationService.GetDateTimeUtcNow();
            var tounaments = await _thermoBetContext
                    .Tournaments
                    .Include(x => x.Markets)
                        .ThenInclude(market => market.Selections)
                    .Where(x => x.StartTimeUtc <= dateTime && x.EndTimeUtc >= dateTime)
                    .ToListAsync();

            foreach (var tournament in tounaments)
                ReorderMarketSelection(tournament);

            return tounaments;
        }

        public async Task<IEnumerable<TournamentModel>> GetAlreadyStartedAsync(int lastNumber)
        {
            var dateTime = await _configurationService.GetDateTimeUtcNow();

            var tounaments = await _thermoBetContext
                    .Tournaments
                    .Include(x => x.Markets)
                        .ThenInclude(market => market.Selections)
                    .Where(x => x.StartTimeUtc <= dateTime)
                    .OrderByDescending(x => x.StartTimeUtc)
                    .Take(lastNumber)
                    .ToListAsync();

            foreach (var tournament in tounaments)
                ReorderMarketSelection(tournament);

            return tounaments;
        }

        public async Task<IEnumerable<TournamentModel>> GetAllAsync()
        {
            var tounaments = await _thermoBetContext
                    .Tournaments
                    .Include(x => x.Markets)
                        .ThenInclude(market => market.Selections)
                    .ToListAsync();

            foreach(var tournament in tounaments)
                ReorderMarketSelection(tournament);

            return tounaments;
        }

        public async Task<TournamentModel> GetAsync(int id)
        {
            var tournament = await _thermoBetContext
                    .Tournaments
                    .Include(x => x.Markets)
                        .ThenInclude(market => market.Selections)
                    .FirstAsync(x => x.Id == id);

            ReorderMarketSelection(tournament);
            return tournament;
        }


        private void ReorderMarketSelection(TournamentModel tournament)
        {
            tournament.Markets = tournament.Markets.OrderBy(x => x.Position).ToList();
            foreach (var market in tournament.Markets)
                market.Selections = market.Selections.OrderByDescending(x => x.IsYes).ToList();

        }

        public IQueryable<TournamentModel> GetAll()
        {
            return _thermoBetContext
                    .Tournaments;

        }

        public async Task BetAsync(int userId, int tournamentId, int marketId, int selectionId)
        {
            var dateTime = await _configurationService.GetDateTimeUtcNow();
            var user = await _thermoBetContext.
                    Users.SingleAsync(x => x.Id == userId);

            var bets = await _thermoBetContext.Entry(user)
                .Collection(x => x.Bets)
                .Query()
                .Where(x => x.Tournament.Id == tournamentId && x.Market.Id == marketId)
                .SingleOrDefaultAsync();

            var tournament = await _thermoBetContext.Tournaments.SingleAsync(x => x.Id == tournamentId);
            if (!(tournament.StartTimeUtc < dateTime && tournament.EndTimeUtc > dateTime))
                throw new FinishedTournamentCoreException();

            var selection = await _thermoBetContext.Selections.SingleAsync(x => x.Id == selectionId);
            if (bets != null)
            {
                bets.Selection = selection;
                bets.DateUtc = dateTime;
            }
            else
            {
                await _thermoBetContext.Bets.AddAsync(new BetModel
                {
                    DateUtc = dateTime,
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