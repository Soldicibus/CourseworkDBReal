using CourseworkDB.Data.Models;

namespace CourseworkDB.Data.Repositories
{
    public interface IPublisherRepository
    {
        Task<Publisher> AddPublisherAsync(Publisher publisher);
        Task DeletePublishersAsync(int publisherId);
        Task<ICollection<Publisher>> GetAllPublishersAsync();
        Task<Publisher> GetPublisherByIdAsync(int id);
        Task<IEnumerable<Publisher>> GetPublishersByUserId(int userId);
        bool PublisherExist(int PublisherId);
        Task<Publisher> UpdatePublisherAsync(Publisher publisher);
    }
}