using Microsoft.EntityFrameworkCore;
using OnlineCourses.Data;
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
        public async Task<bool> UserNameExists(string username)
        {
            var userNameExists = await _context.Users.AnyAsync(u => u.UserName.ToUpper() == username.ToUpper());

            if (userNameExists)
                return true;

            return false;
        }
    }
}
