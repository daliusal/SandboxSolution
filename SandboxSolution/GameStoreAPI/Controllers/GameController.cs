using GameStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Cors;
using AutoMapper;
using GameStoreAPI.Dtos;
using Microsoft.AspNetCore.OData.Query;
using System.Linq;

namespace GameStoreAPI.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameRepo _repository;
        private readonly IMapper _mapper;

        public GameController(IGameRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        //TODO: Add CRUD operation APIs
        [HttpGet]
        [EnableQuery]
        [Route("api/[controller]/getall")]
        public IEnumerable<GameReadDto> GetAllGames()
        {
            //TODO: Return list of games
            return _mapper.Map<IEnumerable<GameReadDto>>(_repository.GetAllGames());
        }
        [HttpGet]
        [EnableQuery]
        [Route("api/[controller]")]
        public IEnumerable<GameReadDto> GetGames()
        {
            //TODO: Return list of games
            return _mapper.Map<IEnumerable<GameReadDto>>(_repository.GetGames());
        }
        [HttpGet]
        [Route("api/[controller]/edit/{id}")]
        public GameEditDto GetGameEditById(int id)
        {
            return _mapper.Map<GameEditDto>(_repository.GetGameById(id));
        }
        [HttpGet]
        [Route("api/[controller]/{id}")]
        public GameReadDto GetGameById(int id)
        {
            return _mapper.Map<GameReadDto>(_repository.GetGameById(id));
        }
        [HttpPost]
        [Route("api/[controller]")]
        public void CreateGame([FromBody]GameCreateDto game)
        {
            //TODO: Check if game already exists
            //      if so change IsDeleted flag to false
            //      and add quantity
            //TODO: If game doesn't exist create a new game
            //      and add it into DB
            _repository.CreateGame(_mapper.Map<Game>(game));
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public void DeleteGame(int id)
        {
            //TODO: Check if game exists
            //      if so change change IsDeleted flag to true
            _repository.DeleteGame(id);
        }

        [HttpPatch]
        [Route("api/[controller]")]
        public void UpdateGame(int id, GameReadDto game)
        {
            //TODO: Check if game exists if so
            //      update game values
            _repository.UpdateGame(id, _mapper.Map<Game>(game));
        }

        [HttpPatch]
        [Route("api/[controller]/addstock")]
        public void AddStock(int id, int quantity)
        {
            //TODO: Check if game with given id exist
            //      if so add quantity to it
            _repository.AddStock(id, quantity);
        }
    }
}
