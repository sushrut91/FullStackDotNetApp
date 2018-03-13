using FullStackApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FullStackApp.Persistance
{
    public class UnitOfWork : IDisposable,IUnitOfWork
    {
        private readonly ApplicationDbContext context;

        public EmployeeRepository Employees { get; set; }
        public DepartmentRepository Departments { get; set; }
        public UserRepository Users { get; set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            Employees = new EmployeeRepository(context);
            Departments = new DepartmentRepository(context);
            Users = new UserRepository(context);
        }

        public int Complete()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}