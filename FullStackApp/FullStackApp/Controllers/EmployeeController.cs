using FullStackApp.Models;
using FullStackApp.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace FullStackApp.Controllers
{
    [EnableCors("http://localhost:4200", "*", "*")]
    public class EmployeeController : ApiController
    {
        private UnitOfWork workUnit = null;
        public EmployeeController()
        {
            this.workUnit = new UnitOfWork(new ApplicationDbContext());
        }

        [Route("api/addEmployee")]
        [HttpPost]
        public IHttpActionResult AddEmployee(Employee employee)
        {
            try
            {
                using (workUnit)
                {
                    workUnit.Employees.Add(employee);
                    workUnit.Complete();
                    workUnit.Dispose();
                    return Ok();
                }
            }
            catch(Exception)
            {
                return InternalServerError();
            }   
        }

        [Route("api/getEmployee")]
        [HttpGet]
        public IHttpActionResult GetEmployee(int id)
        {
            try
            {
                using (workUnit)
                {
                    Employee employee = workUnit.Employees.GetEntity(id);
                    workUnit.Dispose();
                    if (employee != null)
                        return Ok(employee);
                    else
                        return NotFound();
                }
            }
            catch(Exception)
            {
                return InternalServerError();
            }
            
        }

        [Route("api/getAllEmployees")]
        [HttpGet]
        public IHttpActionResult GetAllEmployees()
        {
            try
            {
                using (workUnit)
                {
                    IEnumerable<Employee> allEmployees = workUnit.Employees.GetAll();
                    workUnit.Dispose();
                    if (allEmployees != null)
                        return Ok(allEmployees);
                    else
                        return NotFound();
                }
            }
            catch(Exception)
            {
                return InternalServerError();
            }
        }

        [Route("api/deleteEmployee")]
        [HttpDelete]
        public IHttpActionResult DeleteEmployee(Employee employee)
        {
            try
            {
                using (workUnit)
                {
                    workUnit.Employees.Remove(employee);
                    return Ok(employee);
                }
            }
            catch(Exception)
            {
                return InternalServerError();
            }
            
        }

        [Route("api/findElderEmployees")]
        [HttpGet]
        public IHttpActionResult FindElderEmployees(Employee employee)
        {
            try
            {
                using (workUnit)
                {
                    IEnumerable<Employee> elderEmployees =
                    workUnit.Employees.FindElderEmployees(employee.Age);
                    if (elderEmployees != null)
                        return Ok(elderEmployees);
                    else
                        return NotFound();
                }
            }
            catch(Exception)
            {
                return InternalServerError();
            }
        }
    }
}
