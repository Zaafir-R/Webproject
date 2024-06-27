using System;
using System.IO;
using System.Net.Mail;
using System.Web.Hosting;

namespace WebPortal.Common
{
    public class Helper
    {
        public static void SendEmail(string subject, string content, string destination) 
        {
            using (MailMessage mail = new MailMessage())
            {
                string template = GetEmailTemplate("Default");

                template = template.Replace("{{Content}}", content);

                MailAddress sendermailAddress = new MailAddress("ZaafirWebprojecttest@gmail.com");

                mail.From = sendermailAddress;
                mail.To.Add(destination);
                mail.Subject = subject;
                mail.Body = template;
                mail.IsBodyHtml = true;
                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential("ZaafirWebprojecttest@gmail.com", "wfro omst bzvv pycu");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }

        public static string GetEmailTemplate(string templateName)
        {
            string path = HostingEnvironment.ApplicationPhysicalPath +"\\Views\\Email\\"+ templateName+".html";

            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            else
            {
                return "";
            }
        }
    }
}