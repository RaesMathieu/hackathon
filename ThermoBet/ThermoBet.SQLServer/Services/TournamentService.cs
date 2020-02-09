using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ThermoBet.Core.Models;
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

        public async Task<IEnumerable<TournamentModel>> GetCurrentsAsync(int userId)
        {
            var dateTime = await _configurationService.GetDateTimeUtcNow();
            var tounaments = await _thermoBetContext
                    .Tournaments
                    .Include(x => x.Winnables)
                    .Include(x => x.Markets)
                        .ThenInclude(market => market.Selections)
                    .Where(x => x.StartTimeUtc <= dateTime && x.EndTimeUtc >= dateTime)
                    .Where(x => x.OptinUsers.Any(y => y.UserId == userId))
                    .ToListAsync();

            foreach (var tournament in tounaments)
                ReorderMarketSelection(tournament);

            return tounaments;
        }

        public async Task<IEnumerable<TournamentModel>> GetAlreadyStartedAsync(int userId, int lastNumber)
        {
            var dateTime = await _configurationService.GetDateTimeUtcNow();

            var tournaments = await _thermoBetContext
                    .Tournaments
                    .Include(x => x.Winnables)
                    .Include(x => x.Markets)
                        .ThenInclude(market => market.Selections)
                    .Where(x => x.StartTimeUtc <= dateTime)
                    .Where(x => x.OptinUsers.Any(y => y.UserId == userId))
                    .OrderByDescending(x => x.StartTimeUtc)
                    .Take(lastNumber == 2 ? 10 : lastNumber)
                    .ToListAsync();

            foreach (var tournament in tournaments)
                ReorderMarketSelection(tournament);

            //In case we need only 2 tournaments, we want to fake data and add all markets after 2nd
            //into 2nd one
            if(lastNumber == 2 && tournaments.Count > 2)
            {
                foreach (var tournament in tournaments.Skip(2))
                {
                    tournament.Markets.ToList().ForEach(m => tournaments[1].Markets.Add(m));
                }
            }

            return tournaments.Take(lastNumber);
        }

        public async Task<IEnumerable<TournamentModel>> GetAllAsync()
        {
            var tounaments = await _thermoBetContext
                    .Tournaments
                    .Include(x => x.Winnables)
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
                    .Include(x => x.Winnables)
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

            var tournament = await _thermoBetContext.Tournaments.SingleAsync(x => x.Id == tournamentId && x.OptinUsers.Any(y => y.UserId == userId));
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

        public async Task OptinAsync(int userId, string tournamentCode)
        {
            var dateTime = await _configurationService.GetDateTimeUtcNow();
            var user = await _thermoBetContext.
                    Users.SingleAsync(x => x.Id == userId);

            var optinTournament = await _thermoBetContext.Entry(user)
                .Collection(x => x.Bets)
                .Query()
                .Where(x => x.Tournament.Code == tournamentCode)
                .SingleOrDefaultAsync();

            var tournament = await _thermoBetContext.Tournaments.SingleAsync(x => x.Code.ToLower() == tournamentCode.ToLower());
            if (!(tournament.StartTimeUtc < dateTime && tournament.EndTimeUtc > dateTime))
                throw new FinishedTournamentCoreException();

            if (optinTournament == null)
            {
                await _thermoBetContext.TournamentUserOptins.AddAsync(new TournamentUserOptinModel
                {
                    DateUtc = dateTime,
                    TournamentId = tournament.Id,
                    User = user,
                    Tournament = tournament
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

        public async Task<IEnumerable<BetModel>> GetBetFakeTournamentsAsync(int userId, DateTime maxDate)
        {
            var user = await _thermoBetContext.
                    Users.SingleAsync(x => x.Id == userId);

            var bets = await _thermoBetContext.Entry(user)
                .Collection(x => x.Bets)
                .Query()
                .Include(x => x.Market)
                .Include(x => x.Selection)
                .Include(x => x.Tournament)
                .Where(x => x.Tournament.StartTimeUtc <= maxDate)
                .ToListAsync();

            return bets;
        }

        public async Task<IEnumerable<BetModel>> GetAllBets(int tournamentId)
        {
            var bets = await _thermoBetContext.Bets
                .Include(b => b.Selection)
                .Include(b => b.User)
                .Include(b => b.Market)
                .Where(b => b.Selection.Market.Tournament.Id == tournamentId)
                .OrderBy(b => b.UserId)
                .ThenBy(b => b.DateUtc)
                .ToListAsync();

            return bets;
        }
    }
}