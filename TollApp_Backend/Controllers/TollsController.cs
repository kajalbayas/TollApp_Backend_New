using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TollApp_Backend.Models;

namespace TollApp_Backend.Controllers
{
    public class TollsController : ApiController
    {
        private TOLL_LocalDBEntities1 db = new TOLL_LocalDBEntities1();

        // GET: api/Tolls
        public IQueryable<Toll> GetTolls()
        {
            return db.Tolls;
        }

        // GET: api/Tolls/5
        [ResponseType(typeof(Toll))]
        public IHttpActionResult GetToll(int id)
        {
            Toll toll = db.Tolls.Find(id);
            if (toll == null)
            {
                return NotFound();
            }

            return Ok(toll);
        }

        // PUT: api/Tolls/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutToll(int id, Toll toll)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != toll.Id)
            {
                return BadRequest();
            }

            db.Entry(toll).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TollExists(id))
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

        // POST: api/Tolls
        [ResponseType(typeof(Toll))]
        public IHttpActionResult PostToll(Toll toll)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tolls.Add(toll);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = toll.Id }, toll);
        }

        // DELETE: api/Tolls/5
        [ResponseType(typeof(Toll))]
        public IHttpActionResult DeleteToll(int id)
        {
            Toll toll = db.Tolls.Find(id);
            if (toll == null)
            {
                return NotFound();
            }

            db.Tolls.Remove(toll);
            db.SaveChanges();

            return Ok(toll);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TollExists(int id)
        {
            return db.Tolls.Count(e => e.Id == id) > 0;
        }
    }
}