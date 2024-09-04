using OnlineCourses.Entities;

namespace OnlineCourses.Interfaces
{
    public interface ICategoriesToUserRepository
    {
        Task<List<Category>> GetCategoriesThatHaveContent();
        Task<List<Category>> GetCategoriesCurrentlySavedForUser(string userId);
        Task<List<UserCategory>> GetCategoriesToDeleteForUser(string userId);
        List<UserCategory> GetCategoriesToAddForUser(string[] categoriesSelected, string userId);
        Task UpdateUserCategoryEntityAsync(List<UserCategory> userCategoryItemsToDelete, List<UserCategory> userCategoryItemsToAdd);
    }
}
