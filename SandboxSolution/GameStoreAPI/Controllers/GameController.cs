using GameStoreAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using GameStoreAPI.Dtos;

namespace GameStoreAPI.Controllers
{
    public class GameController : ODataController
    {
        private readonly IGameRepo _repository;
        private readonly IMapper _mapper;

        public GameController(IGameRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //TODO: Add CRUD operation APIs
        [EnableQuery]
        public async Task<IEnumerable<Game>> Get(CancellationToken token)
        {
            //TODO: Return list of games
            //var games = _repository.GetAllGames();
            //var result = new PagingResult<GameReadDto>()
            //{
            //    Count = games.Count(),
            //    Result = _mapper.Map<IEnumerable<GameReadDto>>(games)
            //};
            return await _repository.GetAllGames(token);
        }

        /*[HttpGet]
        [EnableQuery]
        //[Route("api/[controller]")]
        public IEnumerable<GameReadDto> GetGames()
        {
            //TODO: Return list of games
            return _mapper.Map<IEnumerable<GameReadDto>>(_repository.GetGames());
        }*/

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public async Task<GameEditDto> GetGameById(int id)
        {
            return _mapper.Map<GameEditDto>(await _repository.GetGameById(id));
        }

        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> CreateGame([FromBody]GameCreateDto game)
        {
            //TODO: Check if game already exists
            //      if so change IsDeleted flag to false
            //      and add quantity
            //TODO: If game doesn't exist create a new game
            //      and add it into DB
            await _repository.CreateGame(_mapper.Map<Game>(game));
            return Created(game);
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            //TODO: Check if game exists
            //      if so change change IsDeleted flag to true
            var game = await _repository.GetGameById(id);
            if (game != null)
            {
                await _repository.DeleteGame(id);
                return StatusCode(StatusCodes.Status204NoContent);
            }

            return NotFound();
        }

        [HttpPatch]
        [Route("api/[controller]")]
        public async Task UpdateGame([FromBody]GameReadDto game)
        {
            //TODO: Check if game exists if so
            //      update game values
            await _repository.UpdateGame(_mapper.Map<Game>(game));
        }

        [HttpPatch]
        [Route("api/[controller]/addstock")]
        public async Task AddStock(int id, int quantity)
        {
            //TODO: Check if game with given id exist
            //      if so add quantity to it
            await _repository.AddStock(id, quantity);
        }
    }
}
