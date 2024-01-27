using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrate
{
    public class MusicManager:IMusicService
    {
        IMusicDal _musicdal;

        public MusicManager(IMusicDal musicdal)
        {
            _musicdal = musicdal;
        }

        public List<Music> GetList()
        {
            return _musicdal.GetListAll();
        }

        public void TAdd(Music t)
        {
            _musicdal.Insert(t);
        }

        public void TDelete(Music t)
        {
            _musicdal.Delete(t);
        }

        public Music TGetByID(int id)
        {
            return _musicdal.GetByID(id);
        }

        public void TUpdate(Music t)
        {
            _musicdal.Update(t);
        }
    }
}
