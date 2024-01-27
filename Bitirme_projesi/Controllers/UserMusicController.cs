using BusinessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace Bitirme_projesi.Controllers
{
    
    public class UserMusicController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        UserMusicManager um = new UserMusicManager(new EfUserMusicRepository());

        public UserMusicController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [Authorize]
        public async Task<IActionResult> Index(int page = 1)
        {
            var user = await _userManager.GetUserAsync(User);
            var values = um.GetUserMusicsByUser(user.Id).ToPagedList(page,18);

            return View(values);
        }
        public IActionResult DeleteUserMusic(int id)
        {
            try
            {
                var usermusic = um.TGetByID(id);
                um.TDelete(usermusic);

                return Json(new { success = true, message = "Şarkı başarıyla favorilerden kaldırıldı.", deletedSong = usermusic });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Favorilerden kaldırma işleminde bir hata oluştu." });
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AddToFavorites(string song, string singer, string albumCover)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    
                    var user = await _userManager.GetUserAsync(User);
                    var userMusic = new UserMusic { UserID = user.Id, SongName = song, Singer = singer, AlbumCoverUrl = albumCover };
                    
                    if (!um.CheckIfExists(user.Id, song))
                    {
                        
                       

                        um.TAdd(userMusic);

                        return Json(new { success = true, message = "Şarkı favorilere eklendi." });
                    }
                    else
                    {
                        var existingUserMusic = um.GetByUserIdAndSong(user.Id, song);

                        
                        um.TDelete(existingUserMusic);

                        return Json(new { success = true, message = "Şarkı favorilerden çıkarıldı." });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Lütfen giriş yapınız." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Bir hata oluştu: " + ex.Message });
            }
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<JsonResult> IsFavorite(string song, string singer, string albumCover)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                   
                    var user = await _userManager.GetUserAsync(User);

                    
                    bool isFavorite = um.CheckIfExists(user.Id, song);

                    return Json(isFavorite);
                }
                else
                {
                    
                    return Json(false);
                }
            }
            catch (Exception ex)
            {
                
                return Json(false);
            }
        }

       
    
}

}
