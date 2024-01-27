using EntityLayer.Concrate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bitirme_projesi.ViewComponents.User
{
    [AllowAnonymous]
    public class ProfileDropdown:ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public ProfileDropdown(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            
            if (User.Identity.IsAuthenticated)
            {
                var username = User.Identity.Name;
                ViewBag.veri = username;

                var user = await _userManager.FindByNameAsync(username);
                ViewBag.user = new List<AppUser> { user };



                return View();
            }
            else
            {
                return View();
            }

           
        }
    }
}
