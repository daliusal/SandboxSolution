namespace GameStoreAPI.Models
{
    public interface IPublisherRepo
    {
        IEnumerable<Publisher> GetPublishers();
        void CreatePublisher(Publisher publisher);
        void DeletePublisher(int id);

        bool SaveChanges();
    }
}
