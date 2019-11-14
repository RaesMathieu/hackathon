using AutoMapper;
using System.Collections.Generic;
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

            CreateMap<IEnumerable<StatsPositionModel>, IEnumerable<StatsPositionModel>>()
                .ReverseMap();
        }
    }
}