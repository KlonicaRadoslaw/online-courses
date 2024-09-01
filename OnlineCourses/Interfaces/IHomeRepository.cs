using OnlineCourses.Models;

namespace OnlineCourses.Interfaces
{
    public interface IHomeRepository
    {
        IEnumerable<GroupedCategoryItemsByCategoryModel> GetGroupedCategoryItemsByCategory(IEnumerable<CategoryItemDetailsModel> categoryItemDetailsModels);
        Task<IEnumerable<CategoryItemDetailsModel>> GetCategoryItemDetailsForUser(string userId);
    }
}
