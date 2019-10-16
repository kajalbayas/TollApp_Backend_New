using System;
using System.Linq;
using System.Net;
using System.Data.Entity;
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
        [ResponseType(typeof(PaymentHistory))]
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
        [ResponseType(typeof(PaymentHistory))]
        public HttpResponseMessage PostPaymentHistory(PaymentHistory paymentHistory)
        {

            try
            {
                if(paymentHistory.UserId == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Conflict, "User Id is required");
                }

                if (paymentHistory.VehicleTypeId == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Conflict, "Vehicle Type Id is required");
                }

                if (paymentHistory.VehicleNumber == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Conflict, "VehicleNumber is required");
                }

                else
                {
                   

                    paymentHistory.TranscationId = Guid.NewGuid().ToString().GetHashCode().ToString("x");
                    DateTime now = DateTime.Now;
                    paymentHistory.CreatedDate = now;
                    db.PaymentHistories.Add(paymentHistory);

                    // update user balance_amount 
                    var userdetails = db.Users.Where(u => u.Id == paymentHistory.UserId).FirstOrDefault();
                    userdetails.Balance_Amount = userdetails.Balance_Amount - paymentHistory.Amount;
                    db.Entry(userdetails).State = EntityState.Modified;
                    db.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, new
                    {
                      paymentHistory.Amount,
                      paymentHistory.CreatedDate,
                      Routes = db.Routes.Where(r => r.RouteId == paymentHistory.RouteId).Select(r => new { r.From, r.To }),
                      exitlocation = db.TollPlazas.Where(tp => tp.Id == paymentHistory.ExitLocId).Select(tp => new { tp.Location })
                    });
                }
            }
            catch(Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, e);
            }
        }
    }
}


















