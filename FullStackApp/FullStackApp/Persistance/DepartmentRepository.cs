using FullStackApp.Models;
using FullStackApp.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FullStackApp.Persistance
{
    public class DepartmentRepository : Repository<Department>
    {
        public DepartmentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}