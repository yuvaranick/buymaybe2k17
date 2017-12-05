using RazorEngine;
using SampleAuth1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace SampleAuth1.Utils
{
    public class EmailClient
    {
        public void sendEmail(string ToEmail, List<Product> products)
        {
            var fromAddress = new MailAddress("projectamazon17@gmail.com", "From Name");
            var toAddress = new MailAddress(ToEmail, "To Name");
            string fromPassword = "YW1hem9uMTIz";
            string subject = "Test Email from C#";
            string body = createHTMLBody(products);
            byte[] decrypt = Convert.FromBase64String(fromPassword);

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, System.Text.Encoding.UTF8.GetString(decrypt))
            };  
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                IsBodyHtml = true,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }

        public String createHTMLBody(List<Product> products)
        {
            string templateUrl = @AppDomain.CurrentDomain.BaseDirectory+ "/Views/ProductBrowse/WishListEmailTemplate.cshtml";
            string template;
            using (StreamReader reader = new StreamReader(new FileStream(templateUrl, FileMode.Open, FileAccess.Read,
                                      FileShare.ReadWrite | FileShare.Delete), System.Text.Encoding.Unicode))
            {
                template = reader.ReadToEnd();
            }
            // Install RazorEngine from Package Manager using "Install-Package RazorEngine"
            String mailBody = Razor.Parse(template, products);
            return mailBody;
        }

        
    }
}