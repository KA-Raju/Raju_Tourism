using BackendApi_RajuTourism.Models;
using MimeKit;
using MailKit.Net.Smtp;
using Org.BouncyCastle.Crypto;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Azure.Messaging;
using Microsoft.Identity.Client;
using Org.BouncyCastle.Asn1.Cms;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BackendApi_RajuTourism.Common
{
    public class CommonClass
    {
        public bool EnquiryEmailBySMTP(Enquiry enquiry)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(WebApplication.CreateBuilder().Configuration.GetConnectionString("mail")));
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
            smtp.Authenticate(WebApplication.CreateBuilder().Configuration.GetConnectionString("mail"),WebApplication.CreateBuilder().Configuration.GetConnectionString("pass"));
            smtp.Send(email);
            smtp.Disconnect(true);

            return true;

        }
        public bool RegisterEmailBySMTP(RegisterDetail details)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(WebApplication.CreateBuilder().Configuration.GetConnectionString("mail")));
            email.To.Add(MailboxAddress.Parse(details.Email));
            email.Subject = "Regarding your recent registration";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = "Hi <b>" + details.Name + "</b> ,<br><br>Thank you for registring in the Raju Tourism website.<br><br>" +
                "You can Login into Raju Tourism website by using your credentials. <br> Email Id : " + details.Email +
                "<br><br><br><br><b> Regards & Thank You </b><br>K.A.Raju<br>Raju Tourism."
            };
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(WebApplication.CreateBuilder().Configuration.GetConnectionString("mail"), WebApplication.CreateBuilder().Configuration.GetConnectionString("pass"));
            smtp.Send(email);
            smtp.Disconnect(true);

            return true;
        }

        public async Task<bool> EnquiryEmailByGRaphAPI(Enquiry enquiry)
        {
            bool value = false;

            var AppId = WebApplication.CreateBuilder().Configuration.GetConnectionString("clientid");
            var AppSecret = WebApplication.CreateBuilder().Configuration.GetConnectionString("clientsecret");
            var TenantId = WebApplication.CreateBuilder().Configuration.GetConnectionString("tenantid");
            var fromid = WebApplication.CreateBuilder().Configuration.GetConnectionString("fromid");


            EmailMessage message = new();
            message.SaveToSentItems = true;
            EmailMessage.MessageContent content = new();
            var from = fromid;
            content.Subject = "Regarding your recent enquiry";
            List<EmailMessage.Recipient> toemail = new List<EmailMessage.Recipient>();
            toemail.Add(new EmailMessage.Recipient(new EmailMessage.EmailAddress(enquiry.Email)));
            content.ToRecipients = toemail;
            EmailMessage.MessageBody EmailBody = new();
            EmailBody.Content = "Hello <b>" + enquiry.Name + "</b ,<br><br> Thank You for your interest in the Raju Tourism.<br><br>" +
                        "we will connect you with in 3 hrs with the package details to below details.<br>Email : " + enquiry.Email + "<br>Mobile No : " + enquiry.MobileNumber +
                        "<br><br><br><br><b> Regards & Thankyou </b><br>K.A.Raju<br> Raju Tourism.";
            EmailBody.ContentType = "html";
            content.Body = EmailBody;
            message.Message = content;



            var graphApiEndpoint = string.Format("https://graph.microsoft.com/v1.0/users/{0}/sendMail", fromid);
            var emailRequest = new HttpRequestMessage(HttpMethod.Post, graphApiEndpoint);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(graphApiEndpoint);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetAccessToken(AppId, AppSecret, TenantId));
                var emailContent = message;
                emailRequest.Content = new StringContent(JsonConvert.SerializeObject(emailContent), Encoding.UTF8, "application/json");
                var emailResponse = client.PostAsync(graphApiEndpoint, emailRequest.Content);

                if (emailResponse.Result.StatusCode == System.Net.HttpStatusCode.Accepted || emailResponse.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    value = true;
                }

            }
            return value;
        }

        public async Task<bool> RegisterEmailByGRaphAPI(RegisterDetail details)
        {
            bool value = false;

            var AppId = WebApplication.CreateBuilder().Configuration.GetConnectionString("clientid");
            var AppSecret = WebApplication.CreateBuilder().Configuration.GetConnectionString("clientsecret");
            var TenantId = WebApplication.CreateBuilder().Configuration.GetConnectionString("tenantid");
            var fromid = WebApplication.CreateBuilder().Configuration.GetConnectionString("fromid");


            EmailMessage message = new();
            message.SaveToSentItems = true;
            EmailMessage.MessageContent content = new();
            var from = fromid;
            content.Subject = "Regarding your recent registration";
            List<EmailMessage.Recipient> toemail = new List<EmailMessage.Recipient>();
            toemail.Add(new EmailMessage.Recipient(new EmailMessage.EmailAddress(details.Email)));
            content.ToRecipients = toemail;
            EmailMessage.MessageBody EmailBody = new();
            EmailBody.Content = "Hi <b>" + details.Name + "</b> ,<br><br>Thank you for registering in the Raju Tourism website.<br><br>" +
                "You can Login into Raju Tourism website by using your credentials. <br> Email Id : " + details.Email +
                "<br><br><br><br><b> Regards & Thank You </b><br>K.A.Raju<br>Raju Tourism.";
            EmailBody.ContentType = "html";
            content.Body = EmailBody;
            message.Message = content;



            var graphApiEndpoint = string.Format("https://graph.microsoft.com/v1.0/users/{0}/sendMail", fromid);
            var emailRequest = new HttpRequestMessage(HttpMethod.Post, graphApiEndpoint);
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(graphApiEndpoint);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetAccessToken(AppId, AppSecret, TenantId));
                var emailContent = message;
                emailRequest.Content = new StringContent(JsonConvert.SerializeObject(emailContent), Encoding.UTF8, "application/json");
                var emailResponse = client.PostAsync(graphApiEndpoint, emailRequest.Content);

                if(emailResponse.Result.StatusCode == System.Net.HttpStatusCode.Accepted || emailResponse.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                   value = true;
                }
                
            }
            return value;
        }

        static async Task<string> GetAccessToken(string clientId, string clientSecret, string tenantId)
        {
            using(var client = new HttpClient())
            {
                var tokenEndpoint = string.Format("https://login.microsoftonline.com/{0}/oauth2/v2.0/token", tenantId);
                var tokenRequest = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string,string>("client_id",clientId),
                    new KeyValuePair<string,string>("scope","https://graph.microsoft.com/.default"),
                    new KeyValuePair<string,string>("client_secret",clientSecret),
                    new KeyValuePair<string,string>("grant_type","client_credentials"),
                });
                var tokenresponse = await client.PostAsync(tokenEndpoint, tokenRequest);
                var tokencontent = await tokenresponse.Content.ReadAsStringAsync();
                var token = JsonConvert.DeserializeObject<Dictionary<string, string>>(tokencontent)["access_token"];
                return token;           
            }
        }


        
    }

    public class EmailMessage
    {
        public MessageContent Message { get; set; }
        public bool SaveToSentItems { get; set; }
       public class MessageContent
        {
            public string Subject { get; set; }
            public MessageBody Body { get; set; }
            public List<Recipient> ToRecipients { get; set; }
        }
        public class MessageBody
        {
            public string ContentType { get; set; }
            public string Content { get; set; }
        }
        public class Recipient
        {
            public Recipient(EmailAddress emailAddress) {
                EmailAddress = emailAddress;
            }
            [JsonProperty(PropertyName = "emailAddress")]
            public EmailAddress EmailAddress { get; set; }
         
        }
        public class EmailAddress
        {
            [JsonProperty(PropertyName = "address")]
            public string Address { get; set; }
            public EmailAddress(string address)
            {
                Address = address;
            }
        }

    }
}
