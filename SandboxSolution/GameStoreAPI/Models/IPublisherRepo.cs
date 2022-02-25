namespace GameStoreAPI.Models
{
    public interface IPublisherRepo
    {
        Task<IQueryable<Publisher>> GetPublishers();
        Task CreatePublisher(Publisher publisher);
        Task<Publisher> GetPublisher(int id);
        Task UpdatePublisher(Publisher publisher);
        Task DeletePublisher(int id);
        Task<bool> SaveChanges();
    }
}
