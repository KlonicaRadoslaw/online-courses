using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineCourses.Entities;
using OnlineCourses.Interfaces;
using OnlineCourses.Models;

namespace OnlineCourses.Controllers
{
    public class UserAuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserAuthRepository _userAuthRepository;

        public UserAuthController(UserManager<ApplicationUser> userManager, 
                                  SignInManager<ApplicationUser> signInManager,
                                  IUserAuthRepository userAuthRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userAuthRepository = userAuthRepository;
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            loginModel.LoginInValid = "true";

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginModel.Email,
                                                                      loginModel.Password,
                                                                      loginModel.RememberMe,
                                                                      lockoutOnFailure: false);
                if (result.Succeeded)
                    loginModel.LoginInValid = "";
                else
                    ModelState.AddModelError(string.Empty, "Invalid login attempt");
            }
            return PartialView("_UserLoginPartial", loginModel);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterUser(RegistrationModel registrationModel)
        {
            registrationModel.RegistrationInValid = "true";

            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = registrationModel.Email,
                    Email = registrationModel.Email,
                    PhoneNumber = registrationModel.PhoneNumber,
                    FirstName = registrationModel.FirstName,
                    LastName = registrationModel.LastName,
                    Address1 = registrationModel.Address1,
                    Address2 = registrationModel.Address2,
                    PostCode = registrationModel.PostCode
                };

                var result = await _userManager.CreateAsync(user, registrationModel.Password);

                if (result.Succeeded)
                {
                    registrationModel.RegistrationInValid = "";
                    
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    if(registrationModel.CategoryId != 0)
                    {
                        await _userAuthRepository.AddCategoryToUser(user.Id, registrationModel.CategoryId);
                    }

                    return PartialView("_UserRegistrationPartial", registrationModel);
                }

                AddErrorsToModelState(result);
            }
            return PartialView("_UserRegistrationPartial", registrationModel);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();

            if (returnUrl != null)
                return LocalRedirect(returnUrl);
            else
                return RedirectToAction("Index", "Home");
        }

        public async Task<bool> UserNameExists(string username)
        {
            return await _userAuthRepository.UserNameExists(username);
        }

        private void AddErrorsToModelState(IdentityResult result)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
        }
    }
}
