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
    public class IncentivesController : ApiController
    {
        private readonly EmployeeContext db = new EmployeeContext();

        [HttpGet]
        public IQueryable<Incentive> AllIncentives()
        {
            return db.Incentives;
        }

        [HttpGet]
        [ResponseType(typeof(Incentive))]
        public IHttpActionResult IncentiveById(int id)
        {
            var incentive = db.Incentives.Find(id);

            if (incentive == null)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpPost]
        [ResponseType(typeof(Incentive))]
        public IHttpActionResult NewIncentive(Incentive incentive)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Incentives.Add(incentive);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = incentive.Id }, incentive);
        }

        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult Edit(int id, Incentive incentive)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }

            if (id != incentive.Id)
            {
                return NotFound();
            }

            db.Entry(incentive).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExistsIncentive(id))
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
        [ResponseType(typeof(Incentive))]
        public IHttpActionResult DeleteIncentive(int id)
        {
            var incentive = db.Incentives.Find(id);

            if (incentive == null)
            {
                return NotFound();
            }
            db.Incentives.Remove(incentive);
            db.SaveChanges();

            return Ok(incentive);
        }

        private bool ExistsIncentive(int id)
        {
            return db.Incentives.Count(e => e.Id == id) > 0;
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
