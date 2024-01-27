using Bitirme_projesi.Models;
using BusinessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using X.PagedList;

namespace Bitirme_projesi.Controllers
{
    [AllowAnonymous]
    public class NewsController : Controller
    {
        NewsManager nm=new NewsManager(new EfNewsRepository());
      
        public IActionResult Index(int page=1)
        {
            var news = nm.GetList().ToPagedList(page, 3);
            return View(news);
        }
        

    }
}
