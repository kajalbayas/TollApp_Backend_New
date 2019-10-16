using System;
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
        private TollAppDBEntities db = new TollAppDBEntities();

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IQueryable GetUser(int id, int locId, int vehicleId)
        {
            var getVehicles = db.Users.Where(u => u.Id == id).Select(x => new
            {
                x.Id,
                getVehicle = db.Vehicles.Join(db.UserVehicles, v => v.VehicleTypeId, uv => uv.VehicleTypeId, (v, uv) => new
                {
                   v.VehicleType,
                   uv.VehicleNumber
                }),

                TollCost = (from t in db.Tolls
                            join tp in db.TollPlazas
                            on t.ToLocationId equals tp.Id
                            join v in db.Vehicles
                            on t.VehicleTypeId equals v.VehicleTypeId
                            where t.ToLocationId == locId && t.VehicleTypeId == vehicleId
                            select new
                            {
                                t.Cost,
                                t.Id
                            })
                        });
                return getVehicles;
        }


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

    }
}