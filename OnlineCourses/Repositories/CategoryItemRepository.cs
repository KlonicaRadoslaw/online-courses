using Microsoft.EntityFrameworkCore;
using OnlineCourses.Data;
using OnlineCourses.Entities;
using OnlineCourses.Interfaces;

namespace OnlineCourses.Repositories
{
    public class CategoryItemRepository : ICategoryItemRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(CategoryItem categoryItem)
        {
            _context.Add(categoryItem);
            return Save();
        }

        public bool Delete(CategoryItem categoryItem)
        {
            _context.Remove(categoryItem);
            return Save();
        }

        public bool DoesExist(int id)
        {
            return (_context.CategoryItem?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IEnumerable<CategoryItem>> GetAll()
        {
            return await _context.CategoryItem.ToListAsync();
        }

        public async Task<IEnumerable<CategoryItem>> GetAll(int categoryId)
        {
            var list = await(from catItem in _context.CategoryItem
                             where catItem.CategoryId == categoryId
                             select new CategoryItem
                             {
                                 Id = catItem.Id,
                                 Title = catItem.Title,
                                 Description = catItem.Description,
                                 DateTimeItemReleased = catItem.DateTimeItemReleased,
                                 MediaTypeId = catItem.MediaTypeId,
                                 CategoryId = categoryId
                             }).ToListAsync();
            return list;
        }

        public async Task<CategoryItem> GetById(int id)
        {
            return await _context.CategoryItem
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(CategoryItem categoryItem)
        {
            _context.Update(categoryItem);
            return Save();
        }
    }
}
