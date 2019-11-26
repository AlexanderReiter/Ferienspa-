using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Ferienspass.Classes
{
    public class EmailMaker
    {
        /* Aufruf der Klasse:

        var mail = new MailMaker()
        {
            Sender = "ttt@web.de",
            Receiver = new List<string>() { "bbb@web.de" },
            Copy = new List<string>() { "hallo@web.de" },
            Attachments = new List<Attachment> { new Attachment("C:\\ttt.txt") },
            Subject = "Hallo",
            Message = "Na wie gehts?",
            Servername = "smtp.web.de",
            Port = "25",
            Username = "Username",
            Password = "Password"
        };

        mail.Send();
        */

        #region Konstruktor
        public EmailMaker()
        {
        }
        #endregion

        #region Properties
        static public string Sender { get; set; }  //Email-Adresse des Absenders

        static public List<string> Receiver { get; set; }  //Auflistung aller Emails der Empfänger

        static public List<string> Copy { get; set; }  //Auflistung aller Empfänger, die die Email als Kopie erhalten

        static public List<Attachment> Attachments { get; set; }   //Auflistung der Dateinamen der Anhänge (zb: "C:\\test.txt")

        static public string Subject { get; set; } //Betreff der Mail

        static public string Message { get; set; } //Nachricht der Mail

        static public string Username { get; set; }

        static public string Password { get; set; }

        //Servername und Port auf http://www.patshaping.de/hilfen_ta/pop3_smtp.htm
        static public string Servername { get; set; }  //Name des SMTP-Servers

        static public string Port { get; set; }    //Port für die Email-Übermittlung
        #endregion

        static public void Send()
        {
            MailMessage email = new MailMessage();

            MailAddress mailSender = new MailAddress(Sender);
            email.From = mailSender;
            
            //Empfänger hinzufügen
            foreach (string rec in Receiver)
                email.To.Add(rec);
            
            //Kopie-Empfänger hinzufügen (wenn vorhanden)
            if(Copy.Count != 0)
            {
                foreach (string cc in Copy)
                    email.CC.Add(cc);
            }

            //Anhänge hinzufügen (wenn vorhanden)
            if(Attachments.Count != 0)
            {
                foreach (Attachment atmt in Attachments)
                    email.Attachments.Add(atmt);
            }

            email.Subject = Subject;

            email.Body = Message;

            SmtpClient mailClient = new SmtpClient(Servername, int.Parse(Port));    //Postausgangserver

            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(Username, Password);
            mailClient.Credentials = credentials;   //Anmeldeinfos setzen

            mailClient.Send(email); //Email senden
        }
    }
}