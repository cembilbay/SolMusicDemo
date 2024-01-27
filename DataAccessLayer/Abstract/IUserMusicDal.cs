using EntityLayer.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IUserMusicDal:IGenericDal<UserMusic>
    {
        bool CheckIfExists(int? userId, string songName);
        UserMusic GetByUserIdAndSongName(int? userId, string songName);
        List<UserMusic> GetUserMusicsByUser(int userId);
    }
}
