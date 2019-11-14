using System.Linq;
using AutoMapper;
using ThermoBet.Core.Models;

namespace ThermoBet.API.Controllers
{
    public class StatsMapping : Profile
    {
        public StatsMapping()
        {
            CreateMap<StatsPositionModel, StatsPosition>()
            .ReverseMap();

            CreateMap<UserStatsModel, UserStats>()
            .ReverseMap();

            CreateMap<StatsModel, Stats>()
            .ReverseMap();

        }
    }
}