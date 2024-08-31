using OnlineCourses.Entities;
using OnlineCourses.Models;

namespace OnlineCourses.Interfaces
{
    public interface IUsersToCategoryRepository
    {
        Task<List<UserModel>> GetAll();
        Task<List<UserCategory>> GetUsersForCategoryToAdd(UsersCategoryListModel usersCategoryListModel);
        Task<List<UserCategory>> GetUsersFromCategoryToDelete(int categoryId);
        Task<List<UserModel>> GetSavedSelectedUsersForCategory(int categoryId);
    }
}
