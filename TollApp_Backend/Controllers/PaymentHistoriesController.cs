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
    public class PaymentHistoriesController : ApiController
    {
        private TOLL_LocalDBEntities2 db = new TOLL_LocalDBEntities2();


       // GET: api/PaymentHistories/5
        [ResponseType(typeof(PaymentHistory))]
        public IQueryable GetPaymentHistory(int id)
        {
            List<Route> routes = db.Routes.ToList();
            List<Toll> toll = db.Tolls.ToList();

            var query = db.PaymentHistories.Where(p => p.UserId == id).Select(p => new
            {
                p.TranscationId,
                p.CreatedDate,
                p.Amount,
                p.VehicleNumber,
                Routes = db.Routes.Where(r => r.RouteId == p.RouteId).Select(r => new
                {
                    r.From,
                    r.To
                }),

                exitlocation = db.TollPlazas.Where(tp => tp.Id == p.ExitLocId).Select(tp => new
                {
                    tp.Location
                })

            });
            return query;

        }


        // POST: api/PaymentHistories
        //[ResponseType(typeof(PaymentHistory))]
        public HttpResponseMessage PostPaymentHistory(PaymentHistory paymentHistory)
        {
         
            paymentHistory.TranscationId = Guid.NewGuid().ToString().GetHashCode().ToString("x");
            DateTime now = DateTime.Now;
            paymentHistory.CreatedDate = now;
            db.PaymentHistories.Add(paymentHistory);
           
            
            // can use Attach
            var userdetails = db.Users.Where(u => u.Id == paymentHistory.UserId).FirstOrDefault();
            userdetails.Balance_Amount = userdetails.Balance_Amount - paymentHistory.Amount;
            db.Entry(userdetails).State = EntityState.Modified;
            db.SaveChanges();

            var query = db.PaymentHistories.Where(ph => ph.UserId == paymentHistory.UserId).Select(ph => new
            {
                ph.TranscationId,
                ph.Amount,
                ph.CreatedDate,
                Routes = db.Routes.Where(r => r.RouteId == paymentHistory.RouteId).Select(r => new
                {
                    r.From,
                    r.To
                }),

                exitlocation = db.TollPlazas.Where(tp => tp.Id == paymentHistory.ExitLocId).Select(tp => new
                {
                    tp.Location
                })

            });
            return Request.CreateResponse(HttpStatusCode.OK, query);
        }
       }
    }






//var query = db.PaymentHistories.Where(u => u.UserId == id).Select(ph => new
//{
//    ph.TranscationId,
//    ph.CreatedDate,
//    ph.Amount,
//    ExitLocation = (from p in db.PaymentHistories
//                    join t in db.Tolls on p.TollId equals t.Id
//                    join tp in db.TollPlazas on t.ToLocationId equals tp.Id
//                    where p.UserId == id
//                    select new
//                    {
//                        tp.Location,
//                        t.Cost
//                    }),


//    Routes = (from p in db.PaymentHistories
//              join t in db.Tolls on p.TollId equals t.Id
//              join r in db.Routes on t.FromLocationId equals r.RouteId
//              where p.UserId == id
//              select new
//              {
//                  r.From,
//                  r.To
//              }),

//    VehicleNumber = (from p in db.PaymentHistories
//                     join t in db.Tolls on p.TollId equals t.Id
//                     join v in db.Vehicles on t.VehicleTypeId equals v.VehicleTypeId
//                     join uv in db.UserVehicles on v.VehicleTypeId equals uv.VehicleTypeId
//                     where p.UserId == id
//                     select new
//                     {
//                         uv.VehicleNumber
//                     })

//});














