using FullStackApp.Models;
using FullStackApp.Persistance;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;

namespace FullStackApp.Controllers
{
    [EnableCors("http://localhost:4200","*","*")]
    public class DepartmentController : ApiController
    {
        private UnitOfWork workUnit = null;
        
        public DepartmentController(UnitOfWork workUnit)
        {
            this.workUnit = workUnit;
        }
        [Route("api/departments")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                using (workUnit)
                {
                    IEnumerable<Department> departments = workUnit.Departments.GetAll();
                    return Ok(departments);
                }
            }
            catch(Exception)
            {
                return InternalServerError();
            }
        }

        [Route("api/departments/departmentTypes")]
        [HttpGet]
        public IHttpActionResult GetDepartmentTypes()
        {
            try
            {
                Dictionary<int, string> depTypes = new Dictionary<int, string>();

                foreach (DepartmentType item in Enum.GetValues(typeof(DepartmentType)))
                {
                    depTypes.Add(Convert.ToInt32(item), item.ToString());
                }
             
                return Ok(depTypes.ToList()); 
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
        
        [Route("api/departments/{id}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                using (workUnit)
                {
                    Department department = workUnit.Departments.GetEntity(id);
                    if (department == null)
                        return NotFound();
                    else
                        return Ok(department);
                }
            }
            catch(Exception)
            {
                return InternalServerError();
            }
           
        }

        [Route("api/departments/add")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]Department department)
        {
            try
            {
                using (workUnit)
                {
                    workUnit.Departments.Add(department);
                    workUnit.Complete();
                    return Ok();
                }
            }
            catch(Exception)
            {
                return InternalServerError();
            }
        }

    
        [Route("api/departments/update/{id}")]
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]Department department)
        {
            try
            {
                using (workUnit)
                {
                    Department editDepartment = workUnit.Departments.GetEntity(id);
                    if(editDepartment!=null)
                    { 
                        editDepartment.DepartmentName = department.DepartmentName;
                        editDepartment.NoOfEmployees = department.NoOfEmployees;
                        workUnit.Departments.Update(editDepartment);
                        workUnit.Complete();
                        return Ok(department.DepartmentId);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                
            }
            catch(Exception)
            {
                return InternalServerError();
            }
         
        }

        [Route("api/departments/delete/{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                using (workUnit)
                {
                    Department department = workUnit.Departments.GetEntity(id);
                    if(department != null)
                    { 
                        workUnit.Departments.Remove(department);
                        workUnit.Complete();
                        return Ok(department.DepartmentId);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            catch(Exception)
            {
                return InternalServerError();
            }
        }
    }
}
