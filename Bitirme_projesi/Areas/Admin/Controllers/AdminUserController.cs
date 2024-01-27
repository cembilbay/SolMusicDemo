using CoreDemo.Areas.Admin.Models;
using DataAccessLayer.Concrate;
using EntityLayer.Concrate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace Bitirme_projesi.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminUserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        Context c = new Context();

        public AdminUserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index(string sortOrder, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.UserNameSortParam = string.IsNullOrEmpty(sortOrder) ? "username_desc" : "";
            ViewBag.EmailSortParam = sortOrder == "email" ? "email_desc" : "email";

            var users = _userManager.Users.AsQueryable();

            switch (sortOrder)
            {
                case "username_desc":
                    users = users.OrderByDescending(u => u.UserName);
                    break;
                case "email":
                    users = users.OrderBy(u => u.Email);
                    break;
                case "email_desc":
                    users = users.OrderByDescending(u => u.Email);
                    break;
                default:
                    users = users.OrderBy(u => u.UserName);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var pagedUsers = users.ToPagedList(pageNumber, pageSize);

            return View(pagedUsers);
        }
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                
                return RedirectToAction("Index", "AdminUser");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                
                return View("Edit", new UserEditModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                   
                });
            }
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = _userManager.FindByIdAsync(id).Result;
            if (user == null)
            {
                return NotFound();
            }

            var model = new UserEditModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                NameSurname = user.NameSurname
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(UserEditModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id.ToString());

                if (user == null)
                {
                    return NotFound();
                }

                
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.NameSurname = model.NameSurname;

                if (!string.IsNullOrEmpty(model.NewPassword) && model.NewPassword == model.ConfirmNewPassword)
                {
                    var newPasswordHash = _userManager.PasswordHasher.HashPassword(user, model.NewPassword);
                    user.PasswordHash = newPasswordHash;
                }

                var updateResult = await _userManager.UpdateAsync(user);

                if (updateResult.Succeeded)
                {
                    
                    return RedirectToAction("Index", "AdminUser");
                }
                else
                {
                    foreach (var error in updateResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            
            return View(model);
        }
    }
}
