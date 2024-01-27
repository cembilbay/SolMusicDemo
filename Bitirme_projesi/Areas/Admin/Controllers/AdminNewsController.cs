using Bitirme_projesi.Areas.Admin.Models;
using BusinessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using DocumentFormat.OpenXml.Drawing.Charts;
using EntityLayer.Concrate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using X.PagedList;

namespace Bitirme_projesi.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminNewsController : Controller
    {
        NewsManager nm = new NewsManager(new EfNewsRepository());
        public IActionResult Index(int page = 1)
        {
            var values = nm.GetList().ToPagedList(page, 5);
            return View(values);
        }
        public IActionResult Delete(int id)
        {
            var news = nm.TGetByID(id);
            nm.TDelete(news);

            return RedirectToAction("Index");
        }

        public IActionResult Add()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Add(NewsViewModel p)
        {
            if (ModelState.IsValid)
            {
                News news = new News();

                if (p.NewsImage != null)
                {
                    var extension = Path.GetExtension(p.NewsImage.FileName);
                    var newimagename = Guid.NewGuid() + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/NewsImageFiles/", newimagename);
                    var stream = new FileStream(location, FileMode.Create);
                    p.NewsImage.CopyTo(stream);
                    news.NewsImage = newimagename;
                }

                news.NewsTitle = p.NewsTitle;
                news.NewsContent = p.NewsContent;
                news.NewsStatus = true;
                news.NewsCreateTime = DateTime.Parse(DateTime.Now.ToShortDateString());
                news.NewsImage = "/NewsImageFiles/" + news.NewsImage;

                nm.TAdd(news);
                return RedirectToAction("Index");
            }

            // ModelState içindeki hataları kontrol et
            foreach (var modelStateKey in ModelState.Keys)
            {
                var modelStateVal = ModelState[modelStateKey];
                foreach (var error in modelStateVal.Errors)
                {
                    var errorMessage = error.ErrorMessage;
                    Console.WriteLine(errorMessage);
                }
            }

            // Hata durumunda aynı sayfaya geri dön
            return View(p);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var news = nm.TGetByID(id);
            var viewmodel = new NewsViewModel
            {
                NewsTitle = news.NewsTitle,
                NewsContent = news.NewsContent
            };
            ViewBag.NewsID=news.NewsID;
            
            return View(viewmodel);
        }

        [HttpPost]
        
        public IActionResult Edit(NewsViewModel p)
        {
            

            if (ModelState.IsValid)
            {
                try
                {

                    var id = p.NewsID;
                    var news = nm.TGetByID(id);

                    news.NewsTitle = p.NewsTitle;
                    news.NewsContent = p.NewsContent;

                    if (p.NewsImage != null)
                    {
                        // Eğer yeni bir resim seçildiyse, eski resmi sil ve yeni resmi kaydet
                        var extension = Path.GetExtension(p.NewsImage.FileName);
                        var newImageName = Guid.NewGuid() + extension;
                        var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/NewsImageFiles/", newImageName);
                        var stream = new FileStream(location, FileMode.Create);
                        p.NewsImage.CopyTo(stream);
                        news.NewsImage = newImageName;
                        news.NewsImage = "/NewsImageFiles/" + news.NewsImage;
                    }

                    nm.TUpdate(news);

                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return NotFound();
                }
            }

            return View(p);
        }
    }
}
