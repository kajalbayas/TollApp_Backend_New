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

    }
}