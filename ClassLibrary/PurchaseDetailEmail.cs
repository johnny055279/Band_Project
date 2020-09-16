using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ClassLibrary
{
    public class PurchaseDetailEmail
    {
        public static void TicketDetailEmailSending(string email, string account, string date, string location, string venue, int quantity)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("msit1260710@gmail.com", "MCR", Encoding.UTF8);
            mailMessage.To.Add(email);
            mailMessage.Subject = "TOUR PURCHASE INFO";
            mailMessage.SubjectEncoding = Encoding.UTF8;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = $"<h1>Hello {account}!</h1><br/>" +
                "<h1>Thanks to purchase our stuff!</h1>" +
                "<p>Purchase Detail:</p>" +
                $"<ul><li>Date: {date}</li>" +
                $"<li>Location: {location}</li>" +
                $"<li>Venue: {venue}</li>" +
                $"<li>Quantity: {quantity}</li></ul>";
            mailMessage.BodyEncoding = Encoding.UTF8;
            mailMessage.Priority = MailPriority.High;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new NetworkCredential("msit1260710@gmail.com", "@MSIT1260710!");
            try
            {
                smtpClient.EnableSsl = true;
                smtpClient.Send(mailMessage);
                mailMessage.Dispose();
                smtpClient = null;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
    }
}