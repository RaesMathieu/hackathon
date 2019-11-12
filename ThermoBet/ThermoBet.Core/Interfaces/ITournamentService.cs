using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThermoBet.Core.Models;

public interface ITournamentService
{
    Task<IEnumerable<TournamentModel>> GetAllAsync();

    IQueryable<TournamentModel> GetAll();

    Task<TournamentModel> GetAsync(int id);

    Task Update(TournamentModel tournament);
}