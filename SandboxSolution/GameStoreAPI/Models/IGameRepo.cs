using GameStoreAPI.Dtos;

namespace GameStoreAPI.Models
{
    public interface IGameRepo
    {
        bool SaveChanges();
        IQueryable<Game> GetGames();
        IQueryable<Game> GetAllGames();
        Game GetGameById(int id);
        void CreateGame(Game game);
        void UpdateGame(Game game);
        void DeleteGame(int id);
        void AddStock(int id, int stock);
    }
}
