using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThermoBet.Core.Models;

public interface ITournamentService
{
    Task<IEnumerable<TournamentModel>> GetCurrentsAsync();

    Task<IEnumerable<TournamentModel>> GetAllAsync();

    IQueryable<TournamentModel> GetAll();

    Task<TournamentModel> GetAsync(int id);

    Task Update(TournamentModel tournament);

    Task BetAsync(int userId, int tournamentId, int marketId, int selectionId);

    Task<IEnumerable<BetModel>> GetBetAsync(int userId, int tournamentId);
}