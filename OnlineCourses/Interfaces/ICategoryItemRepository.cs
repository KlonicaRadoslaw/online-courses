using OnlineCourses.Entities;

namespace OnlineCourses.Interfaces
{
    public interface ICategoryItemRepository
    {
        Task<IEnumerable<CategoryItem>> GetAll();
        Task<CategoryItem> GetById(int id);
        bool Add(CategoryItem categoryItem);
        bool Update(CategoryItem categoryItem);
        bool Delete(CategoryItem categoryItem);
        bool Save();
        bool DoesExist(int id);
    }
}
