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
        private TOLL_LocalDBEntities2 db = new TOLL_LocalDBEntities2();

        // GET: api/Tolls
        public IQueryable GetTolls()
        {
            var query = db.Tolls.Select(t => new
            {
                t.Id,
                Toll = db.TollPlazas.Where(tp => tp.Id == t.FromLocationId).Select(tp => new
                {
                    tp.Id,
                    tp.Location
                }),

                ExitLocation = db.TollPlazas.Where(tp => tp.Id == t.ToLocationId).Select(tp => new
                {
                    tp.Id,
                    tp.Location
                }),

                //Location=db.TollPlazas.Where(tp=> tp.Id == t.ToLocationId).Select(tp => new
                //{
                //    tp.Id,
                //    tp.Location
                //}),

                //Routes = db.Routes.Where(r => r.RouteId ==  t.FromLocationId).Select(r=> new 
                //{ 
                //   r.RouteId,
                //   r.From,
                //   r.To

                //}),

                VehicleType = db.Vehicles.Where(v => v.VehicleTypeId == t.VehicleTypeId).Select(v => new
                {
                    v.VehicleTypeId,
                    v.VehicleType
                }),

                Cost = t.Cost

            }); ;
            return query;
        }

        // GET: api/Tolls/5
        //[ResponseType(typeof(Toll))]
        //public IHttpActionResult GetToll(int id)
        //{
        //    Toll toll = db.Tolls.Find(id);
        //    if (toll == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(toll);
        //}

        //// PUT: api/Tolls/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutToll(int id, Toll toll)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != toll.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(toll).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TollExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/Tolls
        //[ResponseType(typeof(Toll))]
        //public IHttpActionResult PostToll(Toll toll)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Tolls.Add(toll);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = toll.Id }, toll);
        //}

        //// DELETE: api/Tolls/5
        //[ResponseType(typeof(Toll))]
        //public IHttpActionResult DeleteToll(int id)
        //{
        //    Toll toll = db.Tolls.Find(id);
        //    if (toll == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Tolls.Remove(toll);
        //    db.SaveChanges();

        //    return Ok(toll);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool TollExists(int id)
        //{
        //    return db.Tolls.Count(e => e.Id == id) > 0;
        //}
    }
}