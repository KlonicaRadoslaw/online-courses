using Microsoft.EntityFrameworkCore;
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

        public async Task<List<UserModel>> GetAllUsers()
        {
            var allUsers = await(from user in _context.Users
                                 select new UserModel
                                 {
                                     Id = user.Id,
                                     UserName = user.UserName,
                                     FirstName = user.FirstName,
                                     LastName = user.LastName
                                 }
                                  ).ToListAsync();
            return allUsers;
        }

        public Task<List<UserModel>> GetSavedSelectedUsersForCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserCategory>> GetUsersForCategoryToAdd(UsersCategoryListModel usersCategoryListModel)
        {
            var usersForCategoryToAdd = (from userCat in usersCategoryListModel.UsersSelected
                                               select new UserCategory
                                               {
                                                   CategoryId = usersCategoryListModel.CategoryId,
                                                   UserId = userCat.Id
                                               }).ToList();
            return await Task.FromResult(usersForCategoryToAdd);
        }

        public async Task<List<UserCategory>> GetUsersFromCategoryToDelete(int categoryId)
        {
            var usersForCategoryToDelete = await (from userCat in _context.UserCategory
                                                  where userCat.CategoryId == categoryId
                                                  select new UserCategory
                                                  {
                                                      Id = userCat.Id,
                                                      CategoryId = categoryId,
                                                      UserId = userCat.UserId
                                                  }).ToListAsync();

            return await Task.FromResult(usersForCategoryToDelete);
        }

        public async Task UsersForCategoryAddAndDeleteTransactionAsync(List<UserCategory> usersSelectedForCategoryToAdd, List<UserCategory> usersSelectedForCategoryToDelete)
        {
            using (var dbContextTransaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    _context.RemoveRange(usersSelectedForCategoryToDelete);
                    await _context.SaveChangesAsync();

                    if (usersSelectedForCategoryToAdd != null)
                    {
                        _context.AddRange(usersSelectedForCategoryToAdd);
                        await _context.SaveChangesAsync();
                    }
                    await dbContextTransaction.CommitAsync();

                }

                catch (Exception ex)
                {
                    await dbContextTransaction.DisposeAsync();
                }
            }
        }
    }
}
