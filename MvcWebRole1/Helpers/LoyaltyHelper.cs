using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;

namespace LoyaltyAdministration
{
    public class LoyaltyHelper
    {
        public static void sendEmail(String To, String Cc, String Subject, String Body, String From, String FromName)
        {
            string SMTP_HOST = "smtp.gmail.com";
            string SMTP_USER = "harsha@indavest.com";
            string SMTP_PASS = "info7865";
            try
            {
                MailMessage msg = new MailMessage();
                msg.Subject = Subject;
                msg.Body = Body + "<p><img src='http://dev.rapidloyalty.com/Images/Rapid-Logo.png' style='width:300px;margin-left:-52px;'/></p>";
                msg.IsBodyHtml = true;
                msg.From = new MailAddress("svc@rapidloyalty.com", FromName);
                String[] toArray = To.Split(new string[] { ";" }, StringSplitOptions.None);
                String[] ccArray = Cc.Split(new string[] { ";" }, StringSplitOptions.None);
                for (int toLength = 0; toLength < toArray.Length; toLength++)
                {
                    msg.To.Add(new MailAddress(toArray[toLength]));
                }

                for (int ccLength = 0; ccLength < ccArray.Length; ccLength++)
                {
                    if (!String.IsNullOrEmpty(ccArray[ccLength]))
                    {
                        msg.CC.Add(new MailAddress(ccArray[ccLength]));
                    }
                }

                SmtpClient smtp = new SmtpClient();
                smtp.Host = SMTP_HOST;

                NetworkCredential authinfo = new NetworkCredential(SMTP_USER, SMTP_PASS);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = authinfo;
                smtp.EnableSsl = true;
                smtp.Send(msg);
            }
            catch (Exception ex)
            {

            }
        }
    }
}