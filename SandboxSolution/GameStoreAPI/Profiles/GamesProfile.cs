using AutoMapper;
using GameStoreAPI.Dtos;
using GameStoreAPI.Models;

namespace GameStoreAPI.Profiles
{
    public class GamesProfile : Profile
    {
        public GamesProfile()
        {
            //source -> target
            CreateMap<Game, GameReadDto>();
            CreateMap<GameReadDto, Game>();
            CreateMap<GameCreateDto, Game>();
            CreateMap<Game, GameEditDto>();
            CreateMap<GameEditDto, Game>();
        }
    }
}
