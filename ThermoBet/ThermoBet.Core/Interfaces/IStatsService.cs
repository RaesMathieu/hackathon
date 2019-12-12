using System.Collections.Generic;
using System.Threading.Tasks;
using ThermoBet.Core.Models;

public interface IStatsService
{
    Task<StatsModel> GetByUserIdAsync(int userId, bool fixBugIssue = true);
    Task Compute(int tournamentId, bool fixBugIssue = true);
    void ResetPoints();
}