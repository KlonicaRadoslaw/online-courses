using OnlineCourses.Entities;

namespace OnlineCourses.Interfaces
{
    public interface IContentRepository
    {
        Task<IEnumerable<Content>> GetAll();
        Task<Content> GetById(int id);
        Task<Content> GetByCategoryItemId(int categoryItemId);
        Task<Content> GetContentByCategoryItemId(int categoryItemId);
        bool Add(Content content);
        bool Update(Content content);
        bool Delete(Content content);
        bool Save();
        bool DoesExist(int id);
    }
}
