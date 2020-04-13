using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WebService.Data;
using WebService.Models;

namespace WebService.Controllers
{
    public class DepartmentsController : ApiController
    {
        private readonly EmployeeContext db = new EmployeeContext();

        [HttpGet]
        public IQueryable<Department> AllDepartmens()
        {
            return db.Departments;
        }

        [HttpGet]
        [ResponseType(typeof(Department))]
        public IHttpActionResult DepartmentById(int id)
        {
            var department = db.Departments.Find(id);

            if (department == null)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpPost]
        [ResponseType(typeof(Department))]
        public IHttpActionResult NewDepartment(Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Departments.Add(department);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = department.Id }, department);
        }

        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult Edit(int id, Department department)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }

            if (id != department.Id)
            {
                return NotFound();
            }

            db.Entry(department).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExistsDepartment(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        [ResponseType(typeof(Department))]
        public IHttpActionResult DeleteDepartment(int id)
        {
            var department = db.Departments.Find(id);

            if (department == null)
            {
                return NotFound();
            }
            db.Departments.Remove(department);
            db.SaveChanges();

            return Ok(department);
        }

        private bool ExistsDepartment(int id)
        {
            return db.Employees.Count(e => e.Id == id) > 0;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
