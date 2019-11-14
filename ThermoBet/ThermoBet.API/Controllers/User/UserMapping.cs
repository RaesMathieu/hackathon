using System.Linq;
using AutoMapper;
using ThermoBet.API.Controllers.User;
using ThermoBet.Core.Models;

public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<UserModel, UserResponse>()
            .ReverseMap();

            CreateMap<UserRequest, UserModel>()
            .ReverseMap();
        }
    }