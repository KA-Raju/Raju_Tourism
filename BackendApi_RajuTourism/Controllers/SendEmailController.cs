using BackendApi_RajuTourism.Models;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Net;
using System.Net.Mail;
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

            /*  var email = new MimeMessage();
              email.From.Add(MailboxAddress.Parse(WebApplication.CreateBuilder().Configuration.GetConnectionString("mail")));
              email.To.Add(MailboxAddress.Parse(enquiry.Email));
              email.Subject = "Regarding your recent enquiry";
              email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
              {
                  Text = "Hello <b>" + enquiry.Name + "</b ,<br><br> Thank You for your interest in the Raju Tourism.<br><br>" +
                          "we will connect you with in 3 hrs with the package details to below details.<br>Email : " + enquiry.Email + "<br>Mobile No : " + enquiry.MobileNumber +
                          "<br><br><br><br><b> Regards & Thankyou </b><br>K.A.Raju<br> Raju Tourism."
              };*/
            string subject = "Test Email"; 
            string body = "This is a test email sent from C# using SMTP.";
            MailMessage mail = new MailMessage(WebApplication.CreateBuilder().Configuration.GetConnectionString("mail"), enquiry.Email, subject, body);
            using var smtp = new SmtpClient("smtp-mail.outlook.com", 587)
            { 
                Credentials = new NetworkCredential(WebApplication.CreateBuilder().Configuration.GetConnectionString("mail"), WebApplication.CreateBuilder().Configuration.GetConnectionString("pass")), 
                EnableSsl = true 
            };
            smtp.Send(mail);

            return Ok();

        }

        [Route("registermail")]
        [HttpPost]
        public IActionResult RegisterEmail(RegisterDetail details)
        {
            var email = new MimeMessage();
           /* email.From.Add(MailboxAddress.Parse(WebApplication.CreateBuilder().Configuration.GetConnectionString("mail")));
            email.To.Add(MailboxAddress.Parse(details.Email));
            email.Subject = "Regarding your recent registration";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = "Hi <b>" + details.Name + "</b> ,<br><br>Thank you for registring in the Raju Tourism website.<br><br>" +
                "You can Login into Raju Tourism website by using your credentials. <br> Email Id : " + details.Email +
                "<br><br><br><br><b> Regards & Thank You </b><br>K.A.Raju<br>Raju Tourism."
            };
            using var smtp = new SmtpClient();
            smtp.Connect("smtp-mail.outlook.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

            smtp.AuthenticationMechanisms()
            smtp.Authenticate(WebApplication.CreateBuilder().Configuration.GetConnectionString("mail"), WebApplication.CreateBuilder().Configuration.GetConnectionString("pass"));
            smtp.Send(email);
            smtp.Disconnect(true);*/


            string subject = "Test Email";
            string body = "This is a test email sent from C# using SMTP.";
            MailMessage mail = new MailMessage(WebApplication.CreateBuilder().Configuration.GetConnectionString("mail"), details.Email, subject, body);
            using var smtp = new SmtpClient("smtp-mail.outlook.com", 25)
            {
                Credentials = new NetworkCredential(WebApplication.CreateBuilder().Configuration.GetConnectionString("mail"), WebApplication.CreateBuilder().Configuration.GetConnectionString("pass")),
                EnableSsl = true
             
            };
            smtp.Send(mail);

            return Ok();

            return Ok();
        }

    }    
}
