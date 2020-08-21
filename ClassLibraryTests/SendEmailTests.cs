using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace ClassLibrary.Tests
{
    [TestClass()]
    public class SendEmailTests
    {
        [TestMethod()]
        public void SendUserEmailTest()
        {
            bool success = false;
            Random random = new Random();
            var str = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz+=!@#";
            string newPassword = "";
            for (int i = 0; i < 6; i++)
            {
                newPassword += random.Next(0, str.Length);
            }

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("msit1260710@gmail.com", "MCR", Encoding.UTF8);
            mailMessage.To.Add("msit1260710@gmail.com");
            mailMessage.Subject = "Retrieve your password";
            mailMessage.SubjectEncoding = Encoding.UTF8;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = $"<h1>Your new password is {newPassword}</h1><br/>" +
                "<h1>Please reset your password from below</h1><br/>" +
                "<a href='#'>Click here!</a>";
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
                success = true;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            Assert.IsTrue(success);
        }
    }
}