using EntityLayer.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IUserMusicService:IGenericService<UserMusic>
    {
        bool CheckIfExists(int? userId, string songName);
        public UserMusic GetByUserIdAndSong(int userId, string songName);
        public List<UserMusic> GetUserMusicsByUser(int userId);


    }
}
