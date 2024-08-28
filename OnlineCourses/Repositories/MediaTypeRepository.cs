using Microsoft.EntityFrameworkCore;
using OnlineCourses.Data;
using OnlineCourses.Entities;
using OnlineCourses.Interfaces;

namespace OnlineCourses.Repositories
{
    public class MediaTypeRepository : IMediaTypeRepository
    {
        private readonly ApplicationDbContext _context;
        public MediaTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(MediaType mediaType)
        {
            _context.Add(mediaType);
            return Save();
        }

        public bool Delete(MediaType mediaType)
        {
            _context.Remove(mediaType);
            return Save();
        }

        public bool DoesExist(int id)
        {
            return (_context.MediaType?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IEnumerable<MediaType>> GetAll()
        {
            return await _context.MediaType.ToListAsync();
        }

        public async Task<MediaType> GetById(int id)
        {
            return await _context.MediaType
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(MediaType mediaType)
        {
            _context.Update(mediaType);
            return Save();
        }
    }
}
