using gamesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace gamesApi.Repositories
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly GamesContext _context;

        public PublisherRepository(GamesContext context)
        {
            _context = context;
        }

        public async Task<List<PublisherDto>> GetPublishers()
        {
          
            var publishers = await _context.Publisher
                .Select(p => new PublisherDto(p.PublisherId)
                {
                    PublisherName = p.PublisherName,
                    PublisherId = p.PublisherId
                    
                })
                .ToListAsync();

            if (publishers != null)
            {
                return publishers;
            }
            return null;
        }
        


        public async Task<PublisherDto> GetPublisherById(int id)
        {
            var publisher = await _context.Publisher.FindAsync(id);
            if (publisher == null)
                return null;

            return new PublisherDto(publisher.PublisherId)
            {
                PublisherName = publisher.PublisherName,

            };
        }

        public async Task<bool> UpdatePublisher(int id, PublisherDto updatedPublisher)
        {
            var existingPublisher = await _context.Publisher.FindAsync(id);
            if (existingPublisher == null || id != updatedPublisher.PublisherId)
                return false;

            existingPublisher.PublisherName = updatedPublisher.PublisherName;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public async Task<bool> AddPublisher(PublisherDto newPublisher)
        {
            if (newPublisher == null)
                return false;

            var publisher = new Publisher
            {
                PublisherName = newPublisher.PublisherName,
            };

            _context.Publisher.Add(publisher);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePublisher(int id)
        {
            var publisher = await _context.Publisher.FindAsync(id);
            if (publisher == null)
                return false;

            _context.Publisher.Remove(publisher);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}