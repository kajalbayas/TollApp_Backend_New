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
    public class TollPlazasController : ApiController
    {
        private TOLL_LocalDBEntities1 db = new TOLL_LocalDBEntities1();

        // GET: api/TollPlazas
        public IQueryable  GetTollPlazas()
        {
            //return db.TollPlazas;
            var TollPlazaList = db.TollPlazas.Select(t => t.Location);
            return TollPlazaList;
        }

        // GET: api/TollPlazas/5
        // [ResponseType(typeof(TollPlaza))]
        public IQueryable GetTollPlaza(int id)
        {
            var ExitLocation = db.TollPlazas.Where(e => e.RouteId == id).Select(l=> l.Location);
            return ExitLocation;
   
        }

        // PUT: api/TollPlazas/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutTollPlaza(int id, TollPlaza tollPlaza)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != tollPlaza.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(tollPlaza).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TollPlazaExists(id))
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

        //// POST: api/TollPlazas
        //[ResponseType(typeof(TollPlaza))]
        //public IHttpActionResult PostTollPlaza(TollPlaza tollPlaza)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.TollPlazas.Add(tollPlaza);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = tollPlaza.Id }, tollPlaza);
        //}

        //// DELETE: api/TollPlazas/5
        //[ResponseType(typeof(TollPlaza))]
        //public IHttpActionResult DeleteTollPlaza(int id)
        //{
        //    TollPlaza tollPlaza = db.TollPlazas.Find(id);
        //    if (tollPlaza == null)
        //    {
        //        return NotFound();
        //    }

        //    db.TollPlazas.Remove(tollPlaza);
        //    db.SaveChanges();

        //    return Ok(tollPlaza);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool TollPlazaExists(int id)
        //{
        //    return db.TollPlazas.Count(e => e.Id == id) > 0;
        //}
    }
}