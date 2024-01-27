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
    public class UserManager : IUserService
    {
        IUserDal _userdal;

        public UserManager(IUserDal userdal)
        {
            _userdal = userdal;
        }

        public List<AppUser> GetList()
        {
            return _userdal.GetListAll();
        }

        public void TAdd(AppUser t)
        {
            _userdal.Insert(t);
        }

        public void TDelete(AppUser t)
        {
            _userdal.Delete(t);
        }

        public AppUser TGetByID(int id)
        {
            return _userdal.GetByID(id);
        }

        public void TUpdate(AppUser t)
        {
            _userdal.Update(t);

        }
    }
}
