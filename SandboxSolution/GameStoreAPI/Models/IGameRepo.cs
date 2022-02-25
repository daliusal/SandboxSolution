using GameStoreAPI.Dtos;

namespace GameStoreAPI.Models
{
    public interface IGameRepo
    {
        Task<bool> SaveChanges();
        Task<IQueryable<Game>> GetGames();
        Task<IQueryable<Game>> GetAllGames(CancellationToken token);
        Task<Game> GetGameById(int id);
        Task CreateGame(Game game);
        Task UpdateGame(Game game);
        Task DeleteGame(int id);
        Task AddStock(int id, int stock);
    }
}
