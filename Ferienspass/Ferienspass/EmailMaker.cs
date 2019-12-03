using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;

/// <summary>
/// Programmer: ALexander Reiter
/// Date: 03.12.2019
/// Verfied by Josip
/// </summary>

namespace Ferienspass.Classes
{
    public class EmailMaker
    {
        /// <summary>
        /// Programmer: Alexander Reiter
        /// Date: 03.12.2019
        /// Verified by: Josip Gabric
        /// </summary>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        public static void Send(string email, string subject, string body)
        {
            DB db = new DB();
            string port = (string)db.ExecuteScalar("SELECT value FROM settings WHERE settingID = 'port'");
            string host = (string)db.ExecuteScalar("SELECT value FROM settings WHERE settingID = 'host'");
            string from_mail = (string)db.ExecuteScalar("SELECT value FROM settings WHERE settingID = 'email'");
            string pwd = (string)db.ExecuteScalar("SELECT value FROM settings WHERE settingID = 'password'");

            SmtpClient client = new SmtpClient();
            client.Port = Convert.ToInt32(port);
            client.Host = host;
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(from_mail, pwd);

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from_mail);
            mail.To.Add(email);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            client.Send(mail);
        }
    }
}