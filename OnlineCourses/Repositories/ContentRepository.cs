using Microsoft.EntityFrameworkCore;
using OnlineCourses.Data;
using OnlineCourses.Entities;
using OnlineCourses.Interfaces;

namespace OnlineCourses.Repositories
{
    public class ContentRepository : IContentRepository
    {
        private readonly ApplicationDbContext _context;

        public ContentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Content content)
        {
            _context.Add(content);
            return Save();
        }

        public bool Delete(Content content)
        {
            _context.Remove(content);
            return Save();
        }

        public bool DoesExist(int id)
        {
            return (_context.Content?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IEnumerable<Content>> GetAll()
        {
            return await _context.Content.ToListAsync();
        }

        public async Task<Content> GetById(int id)
        {
            return await _context.Content
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Content content)
        {
            _context.Update(content);
            return Save();
        }
    }
}
