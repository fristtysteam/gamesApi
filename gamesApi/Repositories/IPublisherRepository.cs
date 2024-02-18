using gamesApi.Models;

namespace gamesApi.Repositories
{
    public interface IPublisherRepository
    {
       
        public Task<List<PublisherDto>> GetPublishers();
        public Task<PublisherDto> GetPublisherById(int id);
        public Task<bool> UpdatePublisher(int id, PublisherDto updatedPublisher);
        public Task<bool> AddPublisher(PublisherDto newPublisher);
        public Task<bool> DeletePublisher(int id);

    }
}
