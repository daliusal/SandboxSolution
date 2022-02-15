using GameStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GameStoreAPI.Controllers
{
    public class PublisherController : Controller
    {
        readonly GameStoreDBContext _context;
        public PublisherController(GameStoreDBContext context)
        {
            _context = context;
        }
        //TODO: Add CRUD operation APIs
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("api/[controller]")]
        public List<Publisher> GetPublishers()
        {
            //TODO: Return list of publishers
            return _context.Publishers.Where(pub => pub.IsDeleted == false)
                                      .ToList<Publisher>();
        }

        [HttpPost]
        [Route("api/[controller]")]
        public async Task<Publisher> AddPublisher(PublisherPOST publisher)
        {
            //TODO: Get first publisher by name
            //      Check if it's deleted if so change IsDeleted flag
            //      Else add new publisher
            Publisher _publisher;
            if(_context.Publishers.Any(pub => pub.Name == publisher.Name))
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
            
            await _context.SaveChangesAsync();
            return _publisher;
        }
        
        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public async Task<HttpStatusCode> DeletePublisher(int id)
        {
            //TODO: Get first publisher by id
            //      Check if it's null otherwise change
            //      it's IsDeleted flag to true


            if (!_context.Publishers.Any(pub => pub.Id == id))
                return HttpStatusCode.NotFound;

            var _publisher = _context.Publishers.First(pub => pub.Id == id);
            _publisher.IsDeleted = true;
            _context.Publishers.Update(_publisher);
            await _context.SaveChangesAsync();

            return HttpStatusCode.OK;
        }
    }
}
