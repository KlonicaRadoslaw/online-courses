using OnlineCourses.Entities;

namespace OnlineCourses.Interfaces
{
    public interface IContentRepository
    {
        Task<IEnumerable<Content>> GetAll();
        Task<Content> GetById(int id);
        bool Add(Content content);
        bool Update(Content content);
        bool Delete(Content content);
        bool Save();
        bool DoesExist(int id);
    }
}
