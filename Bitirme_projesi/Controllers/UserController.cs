using CoreDemo.Models;
using EntityLayer.Concrate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bitirme_projesi.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UserProfile()
        {

            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            UserEditModel model = new UserEditModel();
            model.UserMail = values.Email;
            model.NameSurname = values.NameSurname;
            model.UserName = values.UserName;


            return View(model);


        }
        [HttpPost]
        public async Task<IActionResult> UserProfile(UserEditModel model)
        {

            if (ModelState.IsValid)
            {
                var values = await _userManager.FindByNameAsync(User.Identity.Name);


                values.UserName = model.UserName;
                values.Email = model.UserMail;
                values.NameSurname = model.NameSurname;

                values.PasswordHash = _userManager.PasswordHasher.HashPassword(values, model.UserPassword);

                var result = await _userManager.UpdateAsync(values);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(model);
        }
    }
}
