using System.Threading.Tasks;
using ThermoBet.Core.Models;

public interface IStatsService
{
    Task<StatsModel> GetByUserIdAsync(int userId);
}