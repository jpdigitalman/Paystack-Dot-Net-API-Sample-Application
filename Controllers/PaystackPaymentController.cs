using PaystackPayment.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Paystack.Net.SDK.Transactions;

namespace PaystackPayment.Controllers
{
    public class PaystackPaymentController : Controller
    {
        // GET: PaystackPayment
        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> InitializePayment(PaystackCustomerModel model)
        {
            string secretKey = ConfigurationManager.AppSettings["PaystackSecret"];
            var paystackTransactionAPI = new PaystackTransaction(secretKey);
            var response = await paystackTransactionAPI.InitializeTransaction(model.Email, model.Amount, model.FirstName, model.LastName, "http://localhost:44329/order/callback");
            
                        
            //Note that callback url is optional
            if (response.status == true)
            {
                return Json(new { error = false, result = response }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { error = true, result = response }, JsonRequestBehavior.AllowGet);

        }

    }
}