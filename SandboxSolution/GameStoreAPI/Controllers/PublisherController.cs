using GameStoreAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using GameStoreAPI.Dtos;

namespace GameStoreAPI.Controllers
{
    public class PublisherController : ODataController
    {
        private readonly IPublisherRepo _repository;
        private readonly IMapper _mapper;
        public PublisherController(IPublisherRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        //TODO: Add CRUD operation APIs
        [EnableQuery]
        public async Task<IEnumerable<Publisher>> Get()
        {           
            return await _repository.GetPublishers();
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public async Task<Publisher> GetPublisherById(int id)
        {
            return await _repository.GetPublisher(id);
        }

        [HttpPost]
        [Route("api/[controller]")]
        public async Task AddPublisher([FromBody]PublisherCreateDto publisher)
        {
            //TODO: Get first publisher by name
            //      Check if it's deleted if so change IsDeleted flag
            //      Else add new publisher
            await _repository.CreatePublisher(_mapper.Map<Publisher>(publisher));
        }
        
        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public async Task DeletePublisher(int id)
        {
            //TODO: Get first publisher by id
            //      Check if it's null otherwise change
            //      it's IsDeleted flag to true


            await _repository.DeletePublisher(id);
        }

        [HttpPatch]
        [Route("api/[controller]")]
        public async Task UpdatePublisher([FromBody]Publisher publisher)
        {
            await _repository.UpdatePublisher(publisher);
        }
    }
}
