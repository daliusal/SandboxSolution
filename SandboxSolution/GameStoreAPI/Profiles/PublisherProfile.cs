using AutoMapper;
using GameStoreAPI.Dtos;
using GameStoreAPI.Models;

namespace GameStoreAPI.Profiles
{
    public class PublisherProfile : Profile
    {
        public PublisherProfile()
        {
            CreateMap<Publisher, PublisherReadDto>();
            CreateMap<PublisherReadDto, Publisher>();
            CreateMap<PublisherCreateDto, Publisher>();
        }
        
    }
}
