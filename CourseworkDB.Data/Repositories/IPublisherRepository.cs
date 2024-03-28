using CourseworkDB.Data.Models;

namespace CourseworkDB.Data.Repositories
{
    public interface IPublisherRepository
    {
        Task<IEnumerable<Publisher>> GetAllPublishersAsync();
        Task<Publisher> GetPublisherByIdAsync(int id);
        bool PublisherExist(int PublisherId);
    }
}