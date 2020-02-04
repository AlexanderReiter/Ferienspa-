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
            if (!Check.IsAdmin(User.Identity.Name)) Response.Redirect("logout.aspx");
            if (!Page.IsPostBack)
            {
                Fill_gvUser(); 
            }
        }

        protected void btnSearchUser_Click(object sender, EventArgs e)
        {
            Fill_gvUser();
        }

        private void Fill_gvUser()
        {
            DB db = new DB();

            //Nur aktive bzw. nicht aus GV gelöschte User werden angezeigt
            //activeuser = 1 --> User ist aktiv 
            //actuveuser = 0 --> User ist gelöscht aus GV aber vorhanden in der Datenbank

            string queryString = "SELECT * from user WHERE activeuser = 1";

            if (!string.IsNullOrEmpty(txtSearchbar.Text))   //Suchabfrage
            {
                queryString += $" AND (user.email LIKE '{txtSearchbar.Text}%' OR user.givenname LIKE '{txtSearchbar.Text}%' OR user.surname LIKE '{txtSearchbar.Text}%')";
            }

            DataTable dtUser = db.Query(queryString);
            DataView dvUser = new DataView(dtUser);

            gvUser.DataSource = dtUser;
            dvUser.Sort = SortExpression;
            gvUser.DataSource = dvUser;
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


            DataTable userMail = db.Query("SELECT * FROM user WHERE email=?", e.Keys[0]);

            foreach (DataRow dr in userMail.Rows)
            {
                EmailMaker.Send((string)dr["email"], "Löschung des Benutzers!", UserDeleteMailText);
            }
        }

        public string SortExpression
        {
            get
            {
                return (ViewState["SortExpression"] ?? string.Empty).ToString();
            }
            set
            {
                if (SortExpression.StartsWith(value) && (!SortExpression.EndsWith("DESC")))
                {
                    ViewState["SortExpression"] = value + " DESC";
                }
                else
                {
                    ViewState["SortExpression"] = value;
                }
            }
        }

        protected void gvUser_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortExpression = e.SortExpression;
            Fill_gvUser();
        }

        protected void gvUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUser.PageIndex = e.NewPageIndex;
            Fill_gvUser();
        }

        protected void ClosePanel()
        {
            litAlert.Text = string.Empty;
            panUser.Visible = false;
            panBlockBackground.Visible = false;

            Fill_gvUser();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClosePanel();
        }

        private bool AllFilledOrSelected()
        {
            if (txtGivenname.Text == string.Empty) return false;
            if (txtSurname.Text == string.Empty) return false;
            if (Convert.ToInt32(txtUserstatus.Text) < 0 ) return false;
            if (Convert.ToInt32(txtFailedLogins.Text) < 0) return false;
            if (Convert.ToInt32(txtBlocked.Text) < 0) return false;
            if (txtZIP.Text == string.Empty) return false;
            if (txtCity.Text == string.Empty) return false;
            if (txtStreet.Text == string.Empty) return false;
            if (txtNr.Text == string.Empty) return false;


            return true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (AllFilledOrSelected())
            {
                DB db = new DB();
               db.ExecuteNonQuery("UPDATE user SET givenname=?, surname=?, userstatus=?, failedlogins=?, blocked=?, zipcode=?, city=?, streetname=?, housenumber=? WHERE email=?", 
                    txtGivenname.Text, txtSurname.Text, Convert.ToInt32(txtUserstatus.Text), Convert.ToInt32(txtFailedLogins.Text), Convert.ToInt32(txtBlocked.Text), txtZIP.Text, txtCity.Text, txtStreet.Text, txtNr.Text, txtEmail.Text);
            

                ClosePanel();
            }

            else
            {
                litAlert.Text = "<div class='alert aler-danger><strong>Fehler!<strong> Alle Felder müssen mit zulässigen Werten ausgefüllt werden.</div>";
            }
        }

        protected void gvUser_RowEditing(object sender, GridViewEditEventArgs e)
        {
            
            DB db = new DB();
            DataTable dt = db.Query("SELECT * FROM user WHERE email=?", gvUser.DataKeys[e.NewEditIndex].Value);
            DataRow dr = dt.Rows[0];

            txtEmail.Text = (string)dr["email"];
            txtGivenname.Text = (string)dr["givenname"];
            txtSurname.Text = (string)dr["surname"];
            txtUserstatus.Text = Convert.ToString((int)dr["userstatus"]);
            txtFailedLogins.Text = Convert.ToString((int)dr["failedlogins"]);
            txtBlocked.Text = Convert.ToString((int)dr["blocked"]);
            txtZIP.Text = (string)dr["zipcode"];
            txtCity.Text = (string)dr["city"];
            txtStreet.Text = (string)dr["streetname"];
            txtNr.Text = (string)dr["housenumber"];


            panUser.Visible = true;
            panBlockBackground.Visible = true;
            btnSave.Visible = true;
        }

       
    }
}