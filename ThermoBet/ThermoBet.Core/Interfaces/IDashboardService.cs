using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ThermoBet.Core.Interfaces
{
    public interface IDashboardService
    {
        Task<Dictionary<DateTime, int>> GetUniqueUserLoginByDay(int nbDay);
        Task<Dictionary<DateTime, int>> GetUserLoginByDay(int nbDay);

        
    }
}