using System;
using System.Net.Mail;

namespace WebPortal.Common
{
    public class Helper
    {
        public static void SendEmail(string subject, string content, string destination) 
        {
            using (MailMessage mail = new MailMessage())
            {
                
                MailAddress sendermailAddress = new MailAddress("ZaafirWebprojecttest@gmail.com");

                mail.From = sendermailAddress;
                mail.To.Add(destination);
                mail.Subject = subject;
                mail.Body = content;
                mail.IsBodyHtml = true;
                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential("ZaafirWebprojecttest@gmail.com", "sxcd gvdl bnhm glty");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }
    }
}