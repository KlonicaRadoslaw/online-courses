using OnlineCourses.Data;
using OnlineCourses.Entities;
using OnlineCourses.Interfaces;
using OnlineCourses.Models;

namespace OnlineCourses.Repositories
{
    public class UsersToCategoryRepository : IUsersToCategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public UsersToCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<List<UserModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<UserModel>> GetSavedSelectedUsersForCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserCategory>> GetUsersForCategoryToAdd(UsersCategoryListModel usersCategoryListModel)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserCategory>> GetUsersFromCategoryToDelete(int categoryId)
        {
            throw new NotImplementedException();
        }
    }
}
