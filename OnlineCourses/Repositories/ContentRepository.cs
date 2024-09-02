﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<Content> GetByCategoryItemId(int categoryItemId)
        {
            return await _context.Content.SingleOrDefaultAsync(item => item.CategoryItem.Id == categoryItemId);
        }

        public async Task<Content> GetById(int id)
        {
            return await _context.Content
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Content> GetContentByCategoryItemId(int categoryItemId)
        {
            return await (from item in _context.Content
                          where item.CategoryItem.Id == categoryItemId
                          select new Content
                          {
                              Title = item.Title,
                              VideoLink = item.VideoLink,
                              HTMLContent = item.HTMLContent
                          }).FirstOrDefaultAsync();
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
