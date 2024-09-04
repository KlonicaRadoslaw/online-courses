using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineCourses.Entities;
using OnlineCourses.Interfaces;

namespace OnlineCourses.Controllers
{
    public class CategoriesToUserController : Controller
    {
        private readonly ICategoriesToUserRepository _categoriesToUserRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public CategoriesToUserController(ICategoriesToUserRepository categoriesToUserRepository, UserManager<ApplicationUser> userManager)
        {
            _categoriesToUserRepository = categoriesToUserRepository;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
