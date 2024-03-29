﻿using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TollApp_Backend.Models;

namespace TollApp_Backend.Controllers
{
    public class RoutesController : ApiController
    {
        private TollAppDBEntities db = new TollAppDBEntities();

        // GET: api/Routes/5
        public IQueryable GetRoute(int id)
        {
            var Routes = db.Routes.Where(r => r.RouteId == id).Select(c => new
            {
                c.From,
                c.To,
                c.RouteId,
            });
            return Routes;
        }
    }
}


























// PUT: api/Routes/5
//[ResponseType(typeof(void))]
//public IHttpActionResult PutRoute(int id, Route route)
//{
//    if (!ModelState.IsValid)
//    {
//        return BadRequest(ModelState);
//    }

//    if (id != route.RouteId)
//    {
//        return BadRequest();
//    }

//    db.Entry(route).State = EntityState.Modified;

//    try
//    {
//        db.SaveChanges();
//    }
//    catch (DbUpdateConcurrencyException)
//    {
//        if (!RouteExists(id))
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

//// POST: api/Routes
//[ResponseType(typeof(Route))]
//public IHttpActionResult PostRoute(Route route)
//{
//    if (!ModelState.IsValid)
//    {
//        return BadRequest(ModelState);
//    }

//    db.Routes.Add(route);
//    db.SaveChanges();

//    return CreatedAtRoute("DefaultApi", new { id = route.RouteId }, route);
//}

//// DELETE: api/Routes/5
//[ResponseType(typeof(Route))]
//public IHttpActionResult DeleteRoute(int id)
//{
//    Route route = db.Routes.Find(id);
//    if (route == null)
//    {
//        return NotFound();
//    }

//    db.Routes.Remove(route);
//    db.SaveChanges();

//    return Ok(route);
//}

//protected override void Dispose(bool disposing)
//{
//    if (disposing)
//    {
//        db.Dispose();
//    }
//    base.Dispose(disposing);
//}

//private bool RouteExists(int id)
//{
//    return db.Routes.Count(e => e.RouteId == id) > 0;
//}
