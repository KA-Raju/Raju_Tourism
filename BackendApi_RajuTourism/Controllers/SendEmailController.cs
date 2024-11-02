using BackendApi_RajuTourism.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace BackendApi_RajuTourism.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendEmailController : Controller
    {
        [Route("enquiryemail")]
        [HttpPost]
        public IActionResult EnquiryEmail(Enquiry enquiry)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("dummyemail@outlook.com"));
            email.To.Add(MailboxAddress.Parse(enquiry.Email));
            email.Subject = "Regarding your recent enquiry";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = "Hello <b>" + enquiry.Name + "</b ,<br><br> Thank You for your interest in the Raju Tourism.<br><br>" +
                        "we will connect you with in 3 hrs with the package details to below details.<br>Email : " + enquiry.Email + "<br>Mobile No : " + enquiry.MobileNumber +
                        "<br><br><br><br><b> Regards & Thankyou </b><br>K.A.Raju<br> Raju Tourism."
            };
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.office365.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate("", "");
            smtp.Send(email);
            smtp.Disconnect(true);

            return Ok();

        }

        [Route("registermail")]
        [HttpPost]
        public IActionResult RegisterEmail(RegisterDetail details)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("dummyemail@outlook.com"));
            email.To.Add(MailboxAddress.Parse(details.Email));
            email.Subject = "Regarding your recent registration";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = "Hi <b>" + details.Name + "</b> ,<br><br>Thank you for registring in the Raju Tourism website.<br><br>" +
                "You can Login into Raju Tourism website by using your credentials. <br> Email Id : " + details.Email +
                "<br><br><br><br><b> Regards & Thank You </b><br>K.A.Raju<br>Raju Tourism."
            };
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.office365.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate("", "");
            smtp.Send(email);
            smtp.Disconnect(true);

            return Ok();
        }


        public bool RegisterEmailMethod(RegisterDetail details)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("dummyemail@outlook.com"));
            email.To.Add(MailboxAddress.Parse(details.Email));
            email.Subject = "Regarding your recent registration";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = "Hi <b>" + details.Name + "</b> ,<br><br>Thank you for registring in the Raju Tourism website.<br><br>" +
                "You can Login into Raju Tourism website by using your credentials. <br> Email Id : " + details.Email +
                "<br><br><br><br><b> Regards & Thank You </b><br>K.A.Raju<br>Raju Tourism."
            };
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.office365.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate("", "");
            smtp.Send(email);
            smtp.Disconnect(true);

            return true;
        }
    }
}
