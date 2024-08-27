using OnlineCourses.Entities;

namespace OnlineCourses.Interfaces
{
    public interface ICategoryItemRepository
    {
        Task<IEnumerable<CategoryItem>> GetAll();
        Task<IEnumerable<CategoryItem>> GetAll(int categoryId);
        Task<CategoryItem> GetById(int id);
        bool Add(CategoryItem categoryItem);
        bool Update(CategoryItem categoryItem);
        bool Delete(CategoryItem categoryItem);
        bool Save();
        bool DoesExist(int id);
    }
}
