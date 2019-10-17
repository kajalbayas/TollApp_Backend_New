using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TollApp_Backend.Models;

namespace TollApp_Backend.Controllers
{
    public class TollsController : ApiController
    {
        private TollAppDBEntities db = new TollAppDBEntities();

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

                VehicleType = db.Vehicles.Where(v => v.VehicleTypeId == t.VehicleTypeId).Select(v => new
                {
                    v.VehicleTypeId,
                    v.VehicleType
                }),

                Cost = t.Cost

            });
             return query;
        }
    }
}






