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
    public class EmployeesController : ApiController
    {
        private readonly EmployeeContext db = new EmployeeContext();

        [HttpGet]
        public IQueryable<Employee> Employees()
        {
            return db.Employees.Include(e => e.Department).Include(i => i.Incentive);
        }

        [HttpGet]
        [ResponseType(typeof(Employee))]
        public IHttpActionResult EmployeeById(int id)
        {
            var employee = db.Employees.Find(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpPost]
        [ResponseType(typeof(Employee))]
        public IHttpActionResult NewEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Employees.Add(employee);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = employee.Id }, employee);
        }

        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult Edit(int id, Employee employee)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }

            if (id != employee.Id)
            {
                return NotFound();
            }

            db.Entry(employee).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExistsEmployee(id))
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
        public IHttpActionResult DeleteEmployee(int id)
        {
            var employee = db.Employees.Find(id);

            if (employee == null)
            {
                return NotFound();
            }
            db.Employees.Remove(employee);
            db.SaveChanges();

            return Ok(employee);
        }

        private bool ExistsEmployee(int id)
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
