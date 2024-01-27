using Bitirme_projesi.Areas.Admin.Models;
using BusinessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bitirme_projesi.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminAboutController : Controller
    {
        AboutManager am = new AboutManager(new EfAboutRepository());
        [HttpGet]
        public IActionResult Index()
        {
            var values = am.TGetByID(1);

            // AboutModel sınıfını About sınıfından dönüştür
            var model = new AboutModel
            {
                AboutID = values.AboutID,
                AboutName1 = values.AboutName1,
                AboutName2 = values.AboutName2,
                AboutStatus = values.AboutStatus,
                AboutContent1 = values.AboutContent1,
                AboutContent2 = values.AboutContent2
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult Index(AboutModel model)
        {
            if (ModelState.IsValid)
            {
                var about = new About();
                if (model.AboutImageFile != null)
                {
                    var extension = Path.GetExtension(model.AboutImageFile.FileName);
                    var newimagename = Guid.NewGuid() + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/AboutImageFiles/", newimagename);
                    var stream = new FileStream(location, FileMode.Create);
                    model.AboutImageFile.CopyTo(stream);
                    about.AboutImage = newimagename;
                }


                about.AboutID = model.AboutID;
                about.AboutName1 = model.AboutName1;
                about.AboutName2 = model.AboutName2;
                about.AboutStatus = model.AboutStatus;
                about.AboutContent1 = model.AboutContent1;
                about.AboutContent2 = model.AboutContent2;


                am.TUpdate(about);


                return RedirectToAction("Index");
            }


            return View(model);
        }

    }
}
