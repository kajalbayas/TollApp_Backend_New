using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using TollApp_Backend.Models;

namespace TollApp_Backend.Controllers
{
    public class VehiclesController : ApiController
    {
        private TollAppDBEntities db = new TollAppDBEntities();

        // GET: api/Vehicles
        public IQueryable GetVehicles()
        {
            var GetVehicleList = db.Vehicles.Select(v => new
            { 
                v.VehicleTypeId,
                v.VehicleType,
                v.VehicleImg
                
            });
            return GetVehicleList;
        }
     
    }
}