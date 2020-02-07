using System.Linq;
using AutoMapper;
using ThermoBet.Core.Models;
using ThermoBet.MVC.Models;

namespace ThermoBet.MVC.Mapping
{
    public class AutoMapperMapping : Profile
    {
        public AutoMapperMapping()
        {
            CreateMap<SelectionModel, SelectionViewModel>()
                .ReverseMap();

            CreateMap<MarketModel, MarketViewModel>()
                .ReverseMap();

            CreateMap<TournamentModel, TournamentViewModel>()
                .ReverseMap();

            CreateMap<SelectionModel, ResultingSelectionViewModel>()
                .ReverseMap();

            CreateMap<MarketModel, ResultingMarketViewModel>()
                .ReverseMap();

            CreateMap<TournamentModel, ResultingTournamentViewModel>()
                .ReverseMap();

            CreateMap<TournamentWinnableModel, TournamentWinnableViewModel>()
                .ReverseMap();

            CreateMap<UserModel, UserViewModel>()
                .ReverseMap();

        }
    }
}