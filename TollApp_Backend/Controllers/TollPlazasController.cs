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
        private TOLL_LocalDBEntities db = new TOLL_LocalDBEntities();

        // GET: api/TollPlazas
        public IQueryable<TollPlaza> GetTollPlazas()
        {
            return db.TollPlazas;
        }   
    }
}