using AutoMapper;

namespace GameStoreAPI.Models
{
    public class PublisherRepo : IPublisherRepo
    {
        private readonly GameStoreDBContext _context;

        public PublisherRepo(GameStoreDBContext context)
        {
            _context = context;
        }
        public async Task CreatePublisher(Publisher publisher)
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

            await SaveChanges();
        }

        public async Task DeletePublisher(int id)
        {
            if (!_context.Publishers.Any(pub => pub.Id == id))
                return;

            var _publisher = _context.Publishers.First(pub => pub.Id == id);
            _publisher.IsDeleted = true;
            _context.Publishers.Update(_publisher);
            await SaveChanges();
        }

        public async Task<IQueryable<Publisher>> GetPublishers()
        {
            return _context.Publishers;
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task<Publisher> GetPublisher(int id)
        {
            if (_context.Publishers.Any(pub => pub.Id == id))
                return _context.Publishers.FirstOrDefault(pub => pub.Id == id);
            return null;
        }

        public async Task UpdatePublisher(Publisher publisher)
        {
            if (!_context.Publishers.Any(p => p.Id == publisher.Id))
            {
                return;
            }

            var _publisher = _context.Publishers.FirstOrDefault(p => p.Id == publisher.Id);

            _publisher.Name = string.IsNullOrEmpty(publisher.Name) ? _publisher.Name : publisher.Name;
            _publisher.IsDeleted = publisher.IsDeleted;

            _context.Publishers.Update(_publisher);
            await SaveChanges();
        }
    }
}
