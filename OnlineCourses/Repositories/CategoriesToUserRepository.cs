using Microsoft.EntityFrameworkCore;
using OnlineCourses.Data;
using OnlineCourses.Entities;
using OnlineCourses.Interfaces;

namespace OnlineCourses.Repositories
{
    public class CategoriesToUserRepository : ICategoriesToUserRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoriesToUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetCategoriesCurrentlySavedForUser(string userId)
        {
            var categoriesCurrentlySavedForUser = await (from userCategory in _context.UserCategory
                                                         where userCategory.UserId == userId
                                                         select new Category
                                                         {
                                                             Id = userCategory.CategoryId,

                                                         }).ToListAsync();
            return categoriesCurrentlySavedForUser;
        }

        public async Task<List<Category>> GetCategoriesThatHaveContent()
        {
            var categoriesThatHaveContent = await (from category in _context.Category
                                                   join categoryItem in _context.CategoryItem
                                                   on category.Id equals categoryItem.CategoryId
                                                   join content in _context.Content
                                                   on categoryItem.Id equals content.CategoryItem.Id
                                                   select new Category
                                                   {
                                                       Id = category.Id,
                                                       Title = category.Title,
                                                       Description = category.Description
                                                   }).Distinct().ToListAsync();
            return categoriesThatHaveContent;
        }

        public List<UserCategory> GetCategoriesToAddForUser(string[] categoriesSelected, string userId)
        {
            var categoriesToAdd = (from categoryId in categoriesSelected
                                   select new UserCategory
                                   {
                                       UserId = userId,
                                       CategoryId = int.Parse(categoryId)
                                   }).ToList();
            return categoriesToAdd;
        }

        public async Task<List<UserCategory>> GetCategoriesToDeleteForUser(string userId)
        {
            var categoriesToDelete = await (from userCat in _context.UserCategory
                                            where userCat.UserId == userId
                                            select new UserCategory
                                            {
                                                Id = userCat.Id,
                                                CategoryId = userCat.CategoryId,
                                                UserId = userId
                                            }).ToListAsync();
            return categoriesToDelete;
        }

        public Task UpdateUserCategoryEntityAsync(List<UserCategory> userCategoryItemsToDelete, List<UserCategory> userCategoryItemsToAdd)
        {
            throw new NotImplementedException();
        }
    }
}
