using Microsoft.EntityFrameworkCore;
using OnlineCourses.Data;
using OnlineCourses.Entities;
using OnlineCourses.Interfaces;

namespace OnlineCourses.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Category category)
        {
            _context.Add(category);
            return Save();
        }

        public bool Delete(Category category)
        {
            _context.Remove(category);
            return Save();
        }

        public bool DoesExist(int id)
        {
            return (_context.Category?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _context.Category.ToListAsync();
        }

        public async Task<Category> GetById(int id)
        {
            return await _context.Category
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Category category)
        {
            _context.Update(category);
            return Save();
        }
    }
}
