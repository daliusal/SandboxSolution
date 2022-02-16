using GameStoreAPI.Dtos;
using System.Net;

namespace GameStoreAPI.Models
{
    public class GameRepo : IGameRepo
    {
        readonly GameStoreDBContext _context;
        //TODO: Implement interface
        public GameRepo(GameStoreDBContext context)
        {
            _context = context;
        }
        public void AddStock(int id, int stock)
        {
            if (_context.Games.Any(x => x.Id == id))
            {
                var _game = _context.Games.FirstOrDefault(x => x.Id == id);
                _game.Quantity = _game.Quantity + stock > 0 ? _game.Quantity + stock : 0;
                _game.IsOutOfStock = _game.Quantity == 0;
                _context.Games.Update(_game);
                SaveChanges();
            }
        }

        public void CreateGame(Game game)
        {
            Game _game;
            if (_context.Games.Any(g => g.Name == game.Name &&
                                        g.PublisherId == game.PublisherId))
            {
                _game = _context.Games.First(g => g.Name == game.Name &&
                                                  g.PublisherId == game.PublisherId);
                _game.Quantity += game.Quantity;
                _game.IsDeleted = false;
                _context.Games.Update(_game);
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
            }

            SaveChanges();
        }

        public void DeleteGame(int id)
        {
            var _game = _context.Games.FirstOrDefault(g => g.Id == id);
            if (_game == null)
                return;

            _game.IsDeleted = true;
            _context.Games.Update(_game);
            SaveChanges();
        }

        public IEnumerable<Game> GetAllGames()
        {
            return _context.Games.OrderByDescending(game => game.CreationTime)
                                 .ToList();
        }

        public Game GetGameById(int id)
        {
            return _context.Games.FirstOrDefault(game => game.Id == id);
        }

        public IEnumerable<Game> GetGames()
        {
            return _context.Games.Where(game => game.IsOutOfStock == false && game.IsDeleted == false)
                                 .OrderByDescending(game => game.CreationTime)
                                 .ToList();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public void UpdateGame(int id, Game game)
        {
            if (!_context.Games.Any(x => x.Id == id))
            {
                return;
            }

            var _game = _context.Games.FirstOrDefault(game => game.Id == id);

            _game.Name = string.IsNullOrEmpty(game.Name) ? _game.Name : game.Name;
            _game.Description = string.IsNullOrEmpty(game.Description) ? _game.Description :
                                                                        game.Description;
            _game.Quantity = game.Quantity;
            _game.Price = game.Price;
            _game.PublisherId = game.PublisherId;
            _game.Publisher = _context.Publishers.FirstOrDefault(x => x.Id == game.PublisherId);

            _context.Games.Update(_game);
            SaveChanges();
        }
    }
}
