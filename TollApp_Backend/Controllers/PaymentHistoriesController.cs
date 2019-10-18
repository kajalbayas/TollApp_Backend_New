using System;
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
        private TollAppDBEntities db = new TollAppDBEntities();
        
        // GET: api/PaymentHistories/5
        public IQueryable GetPaymentHistory(int id)
        {
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
        public HttpResponseMessage PostPaymentHistory(PaymentHistory paymentHistory)
        {
            using (System.Data.Entity.DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    //https://www.codeproject.com/Articles/14403/Generating-Unique-Keys-in-Net
                    paymentHistory.TranscationId = DateTime.Now.ToString().GetHashCode().ToString("x");
                    paymentHistory.CreatedDate = DateTime.Now;
                    db.PaymentHistories.Add(paymentHistory);
                    db.SaveChanges();


                    var userdetails = db.Users.Where(u => u.Id == paymentHistory.UserId).FirstOrDefault();
                    userdetails.Balance_Amount = userdetails.Balance_Amount - paymentHistory.Amount;
                    db.Entry(userdetails).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }

                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    paymentHistory.Amount,
                    paymentHistory.CreatedDate,
                    paymentHistory.TranscationId,
                    Routes = db.Routes.Where(r => r.RouteId == paymentHistory.RouteId).Select(r => new { r.From, r.To }),
                    exitlocation = db.TollPlazas.Where(tp => tp.Id == paymentHistory.ExitLocId).Select(tp => tp.Location)
                });
            }
        }
    }
}
 

















