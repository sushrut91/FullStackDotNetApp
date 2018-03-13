using FullStackApp.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FullStackApp.Services
{
    public class UserService
    {
        private UnitOfWork workUnit = null;
        public UserService(UnitOfWork workUnit)
        {
            this.workUnit = workUnit;
        }

        public bool CheckUserExistanceInDB(string userName, string passwd)
        {
            using (workUnit)
            {
                var usr = workUnit.Users.Find(u => u.UserName == userName && u.Password == passwd).FirstOrDefault();
                if (usr == null)
                    return false;
                else
                    return true;
            }
        }
    }
}