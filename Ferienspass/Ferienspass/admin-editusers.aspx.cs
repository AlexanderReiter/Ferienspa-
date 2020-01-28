using Ferienspass.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ferienspass
{
    public partial class edit_users : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Fill_gvUser();
            }
        }

        private void Fill_gvUser()
        {
            DB db = new DB();

            //Nur aktive bzw. nicht aus GV gelöschte User werden angezeigt
            //activeuser = 1 --> User ist aktiv 
            //actuveuser = 0 --> User ist gelöscht aus GV aber vorhanden in der Datenbank
            DataTable dtUser = db.Query("SELECT * from user WHERE activeuser = 1");

            gvUser.DataSource = dtUser;
            gvUser.DataBind();
            gvUser.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void gvUser_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //User wird mittgeteit, dass er gelöscht wird 
            //User wird aus GV gelöscht
            //User bleibt aber in der Datenbank erhalten --> Statistik

            //User löschen
            DB db = new DB();

            db.Query("UPDATE user SET activeuser = 0 WHERE email=?", e.Keys[0]);
            gvUser.EditIndex = -1;

            Fill_gvUser();

            //Falls bestätigt, dann E - Mail versenden

            string UserDeleteMailText =
              $"Sehr geehrter Ferienspaß-Benutzer, <br><br> Ihr Benutzer wurde aus unserem System entfernt . Um den Grund der Löschung zu erfahren, " +
              $"melden Sie sich bitte beim Systemmanager. Die Email-Adresse/Kontaktdaten können Sie auf unserer Webseite finden." +
              $"<br><br> Mit freundlichen Grüßen<br>Gemeinde Mondpichl";

            DataTable userMail = db.Query("SELECT * FROM user WHERE activeuser = 0");

            EmailMaker.Send("email", "Kursabsage", UserDeleteMailText);


        }
    }
}