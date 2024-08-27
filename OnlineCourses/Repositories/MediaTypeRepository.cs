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
        public async Task<IEnumerable<MediaType>> GetAll()
        {
            return await _context.MediaType.ToListAsync();
        }
    }
}
