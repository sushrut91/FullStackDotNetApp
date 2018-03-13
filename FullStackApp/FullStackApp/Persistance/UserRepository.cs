using FullStackApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FullStackApp.Persistance
{
    public class UserRepository : Repository<AppUser>
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {

        }
        public bool AuthenticateUser(string userName, string passwd)
        {
            var usr = Find(e => e.UserName == userName && e.Password == passwd).FirstOrDefault();
            if (usr == null)
                return false;
            else
                return true;
        }
    }
}