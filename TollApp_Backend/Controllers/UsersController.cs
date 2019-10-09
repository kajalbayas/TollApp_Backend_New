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


        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IQueryable GetUser(int id)
        {
            var VehicleTypeId = 0;
            var UserRouteId = 0;
            var TollPlazaId = 0;

          
            var query = db.Users.Where(x => x.Id == id).Select(x => new {
                x.Name,
                UserRouteId = x.RouteId,
                UserVehicle = x.UserVehicles.Where(y => y.UserId == id).Select(y => new {
                    y.VehicleNumber,
                }),

                TollPlazaList = db.TollPlazas.Select(p => new
                {
                    TollPlazaId = p.Id,
                    p.Location
                }),

                PaymentHistory = x.PaymentHistories.Where(z => z.UserId == id).Select(z => new {
                    z.CreatedDate,
                    z.TranscationId,
                    z.Amount
                }),

                Route = db.Routes.Where(y => y.RouteId == x.RouteId).Select(z => new { z.From, z.To }),
                vehicles = db.Vehicles.Where(q => q.UserId == id).Select(w => new
                {
                    w.VehicleType,
                    VehicleTypeId = w.VehicleTypeId,
                }),

                Cost = db.Tolls.Where(q => q.ToLocationId == TollPlazaId).Select( c => c.Cost)
          
            });


            return query;

        }
    }
}






