using FullStackApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FullStackApp.Persistance
{
    public class EmployeeRepository : Repository<Employee>
    {
      
        public EmployeeRepository(ApplicationDbContext context) : base(context)
        {
        }
        //Specialized Methods Go Here
        public IEnumerable<Employee> FindElderEmployees(int age)
        {
            return Find(e => e.Age > age).ToList();
        }
    }
}