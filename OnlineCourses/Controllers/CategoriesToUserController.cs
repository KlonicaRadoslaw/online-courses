using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineCourses.Entities;
using OnlineCourses.Interfaces;
using OnlineCourses.Models;

namespace OnlineCourses.Controllers
{
    [Authorize]
    public class CategoriesToUserController : Controller
    {
        private readonly ICategoriesToUserRepository _categoriesToUserRepository;
        private readonly IDataFunctions _dataFunctions;
        private readonly UserManager<ApplicationUser> _userManager;
        public CategoriesToUserController(ICategoriesToUserRepository categoriesToUserRepository, 
                                          UserManager<ApplicationUser> userManager,
                                          IDataFunctions dataFunctions)
        {
            _categoriesToUserRepository = categoriesToUserRepository;
            _userManager = userManager;
            _dataFunctions = dataFunctions;
        }

        public async Task<IActionResult> Index()
        {
            var categoriesToUserModel = new CategoriesToUserModel();

            var userId = _userManager.GetUserAsync(User).Result?.Id;

            categoriesToUserModel.Categories = await _categoriesToUserRepository.GetCategoriesThatHaveContent();
            categoriesToUserModel.CategoriesSelected = await _categoriesToUserRepository.GetCategoriesCurrentlySavedForUser(userId);
            categoriesToUserModel.UserId = userId;

            return View(categoriesToUserModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string[] categoriesSelected)
        {
            var userId = _userManager.GetUserAsync(User).Result?.Id;

            var userCategoriesToDelete = await _categoriesToUserRepository.GetCategoriesToDeleteForUser(userId);
            var userCategoriesToAdd = _categoriesToUserRepository.GetCategoriesToAddForUser(categoriesSelected, userId);

            await _dataFunctions.UsersForCategoryAddAndDeleteTransactionAsync(userCategoriesToAdd, userCategoriesToDelete);

            return RedirectToAction("Index", "Home");
        }
    }
}
