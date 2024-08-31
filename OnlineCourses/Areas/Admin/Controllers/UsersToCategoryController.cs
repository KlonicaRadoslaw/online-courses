using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public UsersToCategoryController(IUsersToCategoryRepository usersToCategoryRepository,
                                         ICategoryRepository categoryRepository)
        {
            _usersToCategoryRepository = usersToCategoryRepository;
            _categoryRepository = categoryRepository;
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
            var usersSelectedForCategoryToAdd = await _usersToCategoryRepository.GetUsersForCategoryToAdd(usersCategoryListModel);
            var usersSelectedForCategoryToDelete = await _usersToCategoryRepository.GetUsersFromCategoryToDelete(usersCategoryListModel.CategoryId);

            await _usersToCategoryRepository.UsersForCategoryAddAndDeleteTransactionAsync(usersSelectedForCategoryToAdd, usersSelectedForCategoryToDelete);

            usersCategoryListModel.Users = await _usersToCategoryRepository.GetAllUsers();

            return PartialView("_UsersListViewPartial", usersCategoryListModel);
        }
    }
}
