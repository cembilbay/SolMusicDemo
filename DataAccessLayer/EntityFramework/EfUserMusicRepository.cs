using DataAccessLayer.Abstract;
using DataAccessLayer.Concrate;
using DataAccessLayer.Repositories;
using EntityLayer.Concrate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfUserMusicRepository:GenericRepository<UserMusic>,IUserMusicDal
    {

        public bool CheckIfExists(int? userId, string songName)
        {
            using (var c = new Context())
            {
                return c.UserMusics.Any(x => x.UserID == userId && x.SongName == songName);
            }
        }
        public UserMusic GetByUserIdAndSongName(int? userId, string songName)
        {
            using (var context = new Context())
            {
                return context.UserMusics
                    .FirstOrDefault(x => x.UserID == userId && x.SongName == songName);
            }
        }
        public List<UserMusic> GetUserMusicsByUser(int userId)
        {
            using (var context = new Context())
            {
                // Kullanıcının müziklerini getirme işlemleri
                var userMusics = context.UserMusics.Where(u => u.UserID == userId).ToList();

                return userMusics;
            }
        }

    }
}
