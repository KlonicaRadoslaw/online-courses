using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCourses.Entities;
using OnlineCourses.Interfaces;
using OnlineCourses.Models;

namespace OnlineCourses.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersToCategoryController : Controller
    {
        private readonly IUsersToCategoryRepository _usersToCategoryRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IDataFunctions _dataFunctions;

        public UsersToCategoryController(IUsersToCategoryRepository usersToCategoryRepository,
                                         ICategoryRepository categoryRepository,
                                         IDataFunctions dataFunctions)
        {
            _usersToCategoryRepository = usersToCategoryRepository;
            _categoryRepository = categoryRepository;
            _dataFunctions = dataFunctions;
        }

        public async Task<IActionResult> GetUsersForCategory(int categoryId)
        {
            var usersCategoryListModel = new UsersCategoryListModel();

            var allUsers = await _usersToCategoryRepository.GetAllUsers();
            var selectedUsersForCategory = await _usersToCategoryRepository.GetSavedSelectedUsersForCategory(categoryId);

            usersCategoryListModel.Users = allUsers;
            usersCategoryListModel.UsersSelected = selectedUsersForCategory;

            return PartialView("_UsersListViewPartial", usersCategoryListModel);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _categoryRepository.GetAll());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveSelectedUsers([Bind("CategoryId, UsersSelected")] UsersCategoryListModel usersCategoryListModel)
        {
            List<UserCategory> usersSelectedForCategoryToAdd = null;

            if (usersCategoryListModel.UsersSelected != null)
                usersSelectedForCategoryToAdd = await _usersToCategoryRepository.GetUsersForCategoryToAdd(usersCategoryListModel);

            var usersSelectedForCategoryToDelete = await _usersToCategoryRepository.GetUsersFromCategoryToDelete(usersCategoryListModel.CategoryId);

            await _dataFunctions.UsersForCategoryAddAndDeleteTransactionAsync(usersSelectedForCategoryToAdd, usersSelectedForCategoryToDelete);

            usersCategoryListModel.Users = await _usersToCategoryRepository.GetAllUsers();

            return PartialView("_UsersListViewPartial", usersCategoryListModel);
        }
    }
}
