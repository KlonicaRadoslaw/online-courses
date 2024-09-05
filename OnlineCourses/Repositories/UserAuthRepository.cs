using Microsoft.EntityFrameworkCore;
using OnlineCourses.Data;
using OnlineCourses.Entities;
using OnlineCourses.Interfaces;

namespace OnlineCourses.Repositories
{
    public class UserAuthRepository : IUserAuthRepository
    {
        private readonly ApplicationDbContext _context;

        public UserAuthRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddCategoryToUser(string userId, int categoryId)
        {
            UserCategory userCategory = new UserCategory();
            userCategory.CategoryId = categoryId;
            userCategory.UserId = userId;

            _context.UserCategory.Add(userCategory);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UserNameExists(string username)
        {
            var userNameExists = await _context.Users.AnyAsync(u => u.UserName.ToUpper() == username.ToUpper());

            if (userNameExists)
                return true;

            return false;
        }
    }
}
