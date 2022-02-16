using AutoMapper;

namespace GameStoreAPI.Models
{
    public class PublisherRepo : IPublisherRepo
    {
        private readonly GameStoreDBContext _context;
        private readonly IMapper _mapper;

        public PublisherRepo(GameStoreDBContext context)
        {
            _context = context;
        }
        public void CreatePublisher(Publisher publisher)
        {
            Publisher _publisher;
            if (_context.Publishers.Any(pub => pub.Name == publisher.Name))
            {
                _publisher = _context.Publishers.First(pub => pub.Name == publisher.Name);
                _publisher.IsDeleted = false;
                _context.Publishers.Update(_publisher);
            }
            else
            {
                _context.Publishers.Add(new Publisher()
                {
                    Name = publisher.Name,
                    IsDeleted = false
                });
                _publisher = _context.Publishers.OrderBy(pub => pub.Id).FirstOrDefault();
            }

            SaveChanges();
        }

        public void DeletePublisher(int id)
        {
            if (!_context.Publishers.Any(pub => pub.Id == id))
                return;

            var _publisher = _context.Publishers.First(pub => pub.Id == id);
            _publisher.IsDeleted = true;
            _context.Publishers.Update(_publisher);
            SaveChanges();
        }

        public IEnumerable<Publisher> GetPublishers()
        {
            return _context.Publishers.Where(pub => pub.IsDeleted == false);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
