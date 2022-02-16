using AutoMapper;
using GameStoreAPI.Dtos;
using GameStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GameStoreAPI.Controllers
{
    public class PublisherController : Controller
    {
        private readonly IPublisherRepo _repository;
        private readonly IMapper _mapper;
        public PublisherController(IPublisherRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        //TODO: Add CRUD operation APIs

        [HttpGet]
        [Route("api/[controller]")]
        public IEnumerable<PublisherReadDto> GetPublishers()
        {
            //TODO: Return list of publishers
            return _mapper.Map<IEnumerable<PublisherReadDto>>(_repository.GetPublishers());
        }

        [HttpPost]
        [Route("api/[controller]")]
        public void AddPublisher(PublisherCreateDto publisher)
        {
            //TODO: Get first publisher by name
            //      Check if it's deleted if so change IsDeleted flag
            //      Else add new publisher
            _repository.CreatePublisher(_mapper.Map<Publisher>(publisher));
        }
        
        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public void DeletePublisher(int id)
        {
            //TODO: Get first publisher by id
            //      Check if it's null otherwise change
            //      it's IsDeleted flag to true


            _repository.DeletePublisher(id);
        }
    }
}
