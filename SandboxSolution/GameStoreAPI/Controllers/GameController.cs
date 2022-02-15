using GameStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Cors;

namespace GameStoreAPI.Controllers
{
    public class GameController : Controller
    {
        readonly GameStoreDBContext _context;
        public GameController(GameStoreDBContext context)
        {
            _context = context;
        }
        //TODO: Add CRUD operation APIs
        [HttpGet]
        [Route("api/[controller]/getall")]
        public List<Game> GetAllGames()
        {
            //TODO: Return list of games
            return _context.Games.OrderByDescending(game => game.CreationTime)
                                 .ToList();
        }
        [HttpGet]
        [Route("api/[controller]")]
        public List<Game> GetGames()
        {
            //TODO: Return list of games
            return _context.Games.Where(game => game.IsOutOfStock == false && game.IsDeleted == false)
                                 .OrderByDescending(game => game.CreationTime)
                                 .ToList();
        }

        [HttpPost]
        [Route("api/[controller]")]
        public async Task<Game> AddGame(GamePOST game)
        {
            //TODO: Check if game already exists
            //      if so change IsDeleted flag to false
            //      and add quantity
            //TODO: If game doesn't exist create a new game
            //      and add it into DB
            Game _game;

            if(_context.Games.Any(g => g.Name == game.Name &&
                                       g.PublisherId == game.PublisherId))
            {
                _game = _context.Games.First(g => g.Name == game.Name &&
                                                  g.PublisherId == game.PublisherId);
                _game.Quantity += game.Quantity;
                _game.IsDeleted = false;
                _context.Games.Update(_game);
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.Games.Add(new Game()
                {
                    Name = game.Name,
                    Description = game.Description,
                    PublisherId = game.PublisherId,
                    Quantity = game.Quantity,
                    IsDeleted = false,
                    CreationTime = DateTime.UtcNow,
                    IsOutOfStock = game.Quantity == 0,
                    Price = game.Price,
                    Publisher = _context.Publishers.First(x => x.Id == game.PublisherId)
                    
                });
                await _context.SaveChangesAsync();
                _game = _context.Games.OrderByDescending(game => game.CreationTime).Last();
            }           
            return _game;
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public async Task<HttpStatusCode> DeleteGame(int id)
        {
            //TODO: Check if game exists
            //      if so change change IsDeleted flag to true
            var _game = _context.Games.FirstOrDefault(g => g.Id == id);
            if(_game == null)
                return HttpStatusCode.NotFound;

            _game.IsDeleted = true;
            _context.Games.Update(_game);
            await _context.SaveChangesAsync();

            return HttpStatusCode.OK;
        }

        [HttpPatch]
        [Route("api/[controller]")]
        public async Task<HttpStatusCode> UpdateGame(int id, GamePOST game)
        {
            //TODO: Check if game exists if so
            //      update game values
            if(!_context.Games.Any(x => x.Id == id))
            {
                return HttpStatusCode.NotFound;
            }

            var _game = _context.Games.FirstOrDefault(game => game.Id == id);

            _game.Name = string.IsNullOrEmpty(game.Name)? _game.Name: game.Name;
            _game.Description = string.IsNullOrEmpty(game.Description)? _game.Description: 
                                                                        game.Description;
            _game.Quantity = game.Quantity;
            _game.Price = game.Price;
            _game.PublisherId = game.PublisherId;
            _game.Publisher = _context.Publishers.FirstOrDefault(x => x.Id == game.PublisherId);

            _context.Games.Update(_game);
            await _context.SaveChangesAsync();

            return HttpStatusCode.OK;
        }

        [HttpPatch]
        [Route("api/[controller]/addstock")]
        public async Task<HttpStatusCode> AddStock(int id, int quantity)
        {
            //TODO: Check if game with given id exist
            //      if so add quantity to it
            if(_context.Games.Any(x => x.Id == id))
            {
                var _game = _context.Games.FirstOrDefault(x => x.Id == id);
                _game.Quantity = _game.Quantity + quantity > 0? _game.Quantity + quantity: 0;
                _game.IsOutOfStock = _game.Quantity == 0;
                _context.Games.Update(_game);
                await _context.SaveChangesAsync();
                return HttpStatusCode.OK;
            }

            return HttpStatusCode.NotFound;
        }
    }
}
