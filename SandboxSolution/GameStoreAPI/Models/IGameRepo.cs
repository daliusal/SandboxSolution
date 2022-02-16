using GameStoreAPI.Dtos;

namespace GameStoreAPI.Models
{
    public interface IGameRepo
    {
        bool SaveChanges();
        IEnumerable<Game> GetGames();
        IEnumerable<Game> GetAllGames();
        Game GetGameById(int id);
        void CreateGame(Game game);
        void UpdateGame(int id, Game game);
        void DeleteGame(int id);
        void AddStock(int id, int stock);
    }
}
