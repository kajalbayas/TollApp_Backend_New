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
    public class UsersController : ApiController
    {
        private TOLL_LocalDBEntities1 db = new TOLL_LocalDBEntities1();

        // GET: api/Users
        //public IQueryable<User> GetUsers()
        //{
        //    return db.Users;
        //}

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IQueryable GetUser(int id, int locId, int vehicleId)
        {
            //Request.QueryString["name"].ToString();
            var getVehicles = db.Users.Where(u => u.Id == id).Select(x => new
            {
                x.Id,
                getVehicle = db.Vehicles.Join(db.UserVehicles, v => v.VehicleTypeId, uv => uv.VehicleTypeId, (v, uv) => new
                {
                   v.VehicleType,
                   uv.VehicleNumber
                }),

                TollCost = (from t in db.Tolls join tp in db.TollPlazas 
                            on t.ToLocationId  equals tp.Id join v in db.Vehicles
                            on t.VehicleTypeId equals v.VehicleTypeId
                            where t.ToLocationId == locId && t.VehicleTypeId==vehicleId
                            select new { t.Cost, t.Id })


                //TollCost =   db.Tolls.Where(q => (q.ToLocationId == db.TollPlazas.Select(e => e.Id).FirstOrDefault()) &&
                //             (q.VehicleTypeId == db.Vehicles.Select(v => v.VehicleTypeId).FirstOrDefault()))
                //             .Select(c => c.Cost)

            });

           return getVehicles;
 }

        // PUT: api/Users/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutUser(int id, User user)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != user.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(user).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UserExists(id))
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

        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            user.Balance_Amount = 1000;
            db.Users.Add(user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        //[ResponseType(typeof(User))]
        //public IHttpActionResult DeleteUser(int id)
        //{
        //    User user = db.Users.Find(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Users.Remove(user);
        //    db.SaveChanges();

        //    return Ok(user);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool UserExists(int id)
        //{
        //    return db.Users.Count(e => e.Id == id) > 0;
        //}
    }
}