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
        private TOLL_LocalDBEntities1 db = new TOLL_LocalDBEntities1();


        // GET: api/PaymentHistories/5
        //[ResponseType(typeof(PaymentHistory))]
        public IQueryable GetPaymentHistory(int id)
        {
            List<Route> routes = db.Routes.ToList();
            List<Toll> toll = db.Tolls.ToList();

           var query = db.PaymentHistories.Where(u => u.UserId == id).Select(ph => new
            {
                ph.TranscationId,
                ph.CreatedDate,
                ph.Amount,
                ExitLocation = (from p in db.PaymentHistories
                                join t in db.Tolls on p.TollId equals t.Id
                                join tp in db.TollPlazas on t.ToLocationId equals tp.Id
                                where p.UserId == id
                                select new {
                                             tp.Location,
                                             t.Cost
                                }),

              
                Routes     =    (from p in db.PaymentHistories join t in db.Tolls on p.TollId equals t.Id
                                join r  in db.Routes on t.FromLocationId equals r.RouteId
                                where p.UserId == id
                                select new {
                                            r.From ,
                                            r.To
                                }),

               VehicleNumber = (from p in db.PaymentHistories join  t in db.Tolls on p.TollId equals t.Id 
                                join v in db.Vehicles on t.VehicleTypeId equals v.VehicleTypeId 
                                join uv in db.UserVehicles on v.VehicleTypeId equals uv.VehicleTypeId
                                where p.UserId == id
                                select new {
                                            uv.VehicleNumber
                                })

                              });

                        return query;

        }

     
        // POST: api/PaymentHistories
        //[ResponseType(typeof(PaymentHistory))]
        public IHttpActionResult PostPaymentHistory(PaymentHistory paymentHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            paymentHistory.TranscationId = Guid.NewGuid().ToString().GetHashCode().ToString("x");
            DateTime now = DateTime.Now;
            paymentHistory.CreatedDate = now;
            db.PaymentHistories.Add(paymentHistory);
           

            var userdetails = db.Users.Where(u => u.Id == paymentHistory.UserId).FirstOrDefault();
            userdetails.Balance_Amount = userdetails.Balance_Amount - paymentHistory.Amount;
            db.Entry(userdetails).State = EntityState.Modified;
            db.SaveChanges();
  
            return CreatedAtRoute("DefaultApi", new { id = paymentHistory.Id }, paymentHistory);
         }
       }
    }




















    //// PUT: api/PaymentHistories/5
    //[ResponseType(typeof(void))]
    //public IHttpActionResult PutPaymentHistory(int id, PaymentHistory paymentHistory)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return BadRequest(ModelState);
    //    }

//    if (id != paymentHistory.Id)
//    {
//        return BadRequest();
//    }

//    db.Entry(paymentHistory).State = EntityState.Modified;

//    try
//    {
//        db.SaveChanges();
//    }
//    catch (DbUpdateConcurrencyException)
//    {
//        if (!PaymentHistoryExists(id))
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

// POST: api/PaymentHistories
//[ResponseType(typeof(PaymentHistory))]
//public IHttpActionResult PostPaymentHistory(PaymentHistory paymentHistory)
//{
//    if (!ModelState.IsValid)
//    {
//        return BadRequest(ModelState);
//    }

//    db.PaymentHistories.Add(paymentHistory);

//    try
//    {
//        db.SaveChanges();
//    }
//    catch (DbUpdateException)
//    {
//        if (PaymentHistoryExists(paymentHistory.Id))
//        {
//            return Conflict();
//        }
//        else
//        {
//            throw;
//        }
//    }

//    return CreatedAtRoute("DefaultApi", new { id = paymentHistory.Id }, paymentHistory);
//}


//// DELETE: api/PaymentHistories/5
//[ResponseType(typeof(PaymentHistory))]
//public IHttpActionResult DeletePaymentHistory(int id)
//{
//    PaymentHistory paymentHistory = db.PaymentHistories.Find(id);
//    if (paymentHistory == null)
//    {
//        return NotFound();
//    }

//    db.PaymentHistories.Remove(paymentHistory);
//    db.SaveChanges();

//    return Ok(paymentHistory);
//}

//protected override void Dispose(bool disposing)
//{
//    if (disposing)
//    {
//        db.Dispose();
//    }
//    base.Dispose(disposing);
//}

//private bool PaymentHistoryExists(int id)
//{
//    return db.PaymentHistories.Count(e => e.Id == id) > 0;
//}  //var query = db.PaymentHistories.Where(u => u.UserId == id).Select(p => new
            //{
            //    p.TranscationId,
            //    p.CreatedDate,
            //    p.Amount,
            //    TollDetails = db.Tolls.Where(t => t.Id == p.TollId).Select(x => new
            //    {

            //        Location = db.Routes.Join(db.Tolls, r => r.RouteId, t => t.FromLocationId, (r, t) => new
            //        {
            //            r.From,
            //            r.To
            //        }),

            //        VehicleNumber = db.Vehicles.Join(db.Tolls, v => v.VehicleTypeId, u => u.VehicleTypeId, (v, u) => new
            //        {
            //            u.VehicleTypeId
            //        }).Join(db.UserVehicles, u => u.VehicleTypeId, uv => uv.VehicleTypeId, (u, uv) => new
            //        {
            //            uv.VehicleNumber
            //        }),


            //        ExitLocation = db.Tolls.Join(db.TollPlazas, t => t.ToLocationId, tp => tp.Id, (t, tp) => new
            //        {
            //            tp.Location
            //        })
            //    }),
            //});
