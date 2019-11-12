using System.Linq;
using AutoMapper;
using ThermoBet.Core.Models;

public class TournamentMapping : Profile
    {
        public TournamentMapping()
        {
            CreateMap<SelectionModel, Selection>()
            .ReverseMap();

            CreateMap<MarketModel, Market>()
            .ReverseMap();

            CreateMap<TournamentModel, TournamentReponse>()
            .ReverseMap();

        }
    }