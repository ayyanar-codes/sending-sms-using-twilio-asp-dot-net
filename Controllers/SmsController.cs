using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using Twilio;
using Twilio_4_project.Models;
using Twilio.Rest.Api.V2010.Account;

namespace Twilio_4_project.Controllers
{
    

    public class SmsController : Controller
    {
        private const string TwilioAccountSid = "AC81d729949a1123f38d0526d39c2d5e78";
        private const string TwilioAuthToken = "672a30e870c60b702f55a32364db4710";
        private const string TwilioPhoneNumber = "+14235454188";

        //private const string TwilioAccountSid1 = "AC9b539a14eb8147e86cd40d174f8e598d";
        //private const string TwilioAuthToken1 = "1ff2ca83ddc46fcf537a43cc6fb912af";
        //private const string TwilioPhoneNumber1 = "+15177934651";

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SendSms(SmsModel model)
        {
            //string[] MultiNums = { TwilioPhoneNumber, TwilioPhoneNumber1 };

            

            // Split phone numbers by comma and remove whitespace
            string[] phoneNumbers = model.PhoneNumbers.Split(',')
                .Select(phone => phone.Trim())
                .ToArray();

            // Send SMS to each phone number
            foreach (var phoneNumber in phoneNumbers)
            {
                TwilioClient.Init(TwilioAccountSid, TwilioAuthToken);
                //TwilioClient.Init(TwilioAccountSid1, TwilioAuthToken1);
                var message = MessageResource.Create(
                    body: model.Message,
                    from: new Twilio.Types.PhoneNumber(TwilioPhoneNumber),

                    to: new Twilio.Types.PhoneNumber(phoneNumber)
                );

            } 

            return Json(new { success = true, message = "SMS sent successfully." });
        }

    }

}