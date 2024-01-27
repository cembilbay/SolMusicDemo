using BusinessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace Bitirme_projesi.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminContactController : Controller
    {
        ContactManager cm = new ContactManager(new EfContactRepository());
        public IActionResult Index(int page = 1)
        {
            var contactlist = cm.GetList().ToPagedList(page, 10);
            return View(contactlist);
        }
        public IActionResult Details(int id)
        {
            var contact = cm.TGetByID(id);

            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }
        public IActionResult Delete(int id)
        {
            var contact = cm.TGetByID(id);

            if (contact == null)
            {
                return NotFound();
            }

            cm.TDelete(contact);


            TempData["SuccessMessage"] = "İletişim Mesajı Başarıyla Silindi.";
            return RedirectToAction("Index");
        }
    }
}
