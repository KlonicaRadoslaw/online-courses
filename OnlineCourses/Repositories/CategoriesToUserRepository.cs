using OnlineCourses.Entities;
using OnlineCourses.Interfaces;

namespace OnlineCourses.Repositories
{
    public class CategoriesToUserRepository : ICategoriesToUserRepository
    {
        public Task<List<Category>> GetCategoriesCurrentlySavedForUser(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Category>> GetCategoriesThatHaveContent()
        {
            throw new NotImplementedException();
        }

        public List<UserCategory> GetCategoriesToAddForUser(string[] categoriesSelected, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserCategory>> GetCategoriesToDeleteForUser(string userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserCategoryEntityAsync(List<UserCategory> userCategoryItemsToDelete, List<UserCategory> userCategoryItemsToAdd)
        {
            throw new NotImplementedException();
        }
    }
}
