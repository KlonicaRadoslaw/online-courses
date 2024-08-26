using OnlineCourses.Entities;

namespace OnlineCourses.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll();
        Task<Category> GetById(int id);
        bool Add(Category category);
        bool Update(Category category);
        bool Delete(Category category);
        bool Save();
        bool DoesExist(int id);
    }
}
