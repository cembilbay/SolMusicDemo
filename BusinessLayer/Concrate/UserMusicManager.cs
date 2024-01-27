using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrate
{
    public class UserMusicManager:IUserMusicService
    {
        IUserMusicDal _usermusicdal;

        public UserMusicManager(IUserMusicDal usermusicdal)
        {
            _usermusicdal = usermusicdal;
        }

        public bool CheckIfExists(int? userId, string songName)
        {
            var existingRecord = _usermusicdal.GetByUserIdAndSongName(userId, songName);
            return existingRecord != null;
        }

        public UserMusic GetByUserIdAndSong(int userId, string songName)
        {
             return _usermusicdal.GetByUserIdAndSongName(userId, songName);
        }

        public List<UserMusic> GetList()
        {
            return _usermusicdal.GetListAll();
        }

        public List<UserMusic> GetUserMusicsByUser(int userId)
        {
            return _usermusicdal.GetUserMusicsByUser(userId);
        }

        public void TAdd(UserMusic t)
        {
            _usermusicdal.Insert(t);
        }

        public void TDelete(UserMusic t)
        {
            _usermusicdal.Delete(t);
        }

        public UserMusic TGetByID(int id)
        {
            return _usermusicdal.GetByID(id);
        }

        public void TUpdate(UserMusic t)
        {
            _usermusicdal.Update(t);
        }
    }
}
