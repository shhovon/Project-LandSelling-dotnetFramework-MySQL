using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace landSelling.Email
{
    public class EmailClass
    {
        public void SendEmail(string receiver, string subject, string message)
        {
            try
            {
                var senderEmail = new MailAddress("mrbotff001@gmail.com", "PropertySelling");
                var receiverEmail = new MailAddress(receiver, "Receiver");
                var password = "MRbotff001_";
                var sub = subject;
                var body = message;
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, password)
                };
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(mess);
                }
                
            }
            catch (Exception)
            {
                //ViewBag.Error = "Some Error";
            }
        }
    }
}