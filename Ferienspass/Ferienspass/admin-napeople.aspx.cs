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
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) Fill_gvNAPeople();
        }

        private void Fill_gvNAPeople()
        {
            DB db = new DB();
            string sqlGetNAPeople = $"SELECT t1.givenname, t1.surname, t1.zipcode, t1.city, t1.streetname, t1.housenumber, t1.email " +
                    "FROM user As t1 " +
                    "WHERE t1.zipcode NOT IN " +
                    "(SELECT t2.zipcode " +
                    "FROM neighbourcities AS t2 " +
                    "WHERE t2.zipcode IS NOT NULL)" +
                    "AND t1.userstatus = 1";
            DataTable dt = db.Query(sqlGetNAPeople);
            gvNAPeople.DataSource = dt;
            gvNAPeople.DataKeyNames.Append("email");    // DataKeyNames wird gesetzt um später einfacher die email herauszufinden
            gvNAPeople.DataBind();
        }

        protected void btnSendMail_Click(object sender, EventArgs e)
        {
            string Text = $"Guten Tag, <br><br>Sie dürfen leider nicht mehr am Ferienpass der Gemeinde Mondpichl teilnehmen, " +
                $"da ihre Gemeinde keine Partnerschaft mehr mit Mondpichel hat. <br><br> Mit freundlichen Grüßen,<br>Gemeinde Mondpichl";

            // Get email from gridview an der Postion, an der der email senden Button betätigt wurde.
            // Sender object als control besitzt als parent DataControlFieldCell, welches sich in GridviewRow befindet.
            try
            {
                GridViewRow gvr = (GridViewRow)((Control)sender).Parent.Parent;
                string user = gvNAPeople.DataKeys[gvr.RowIndex].Value.ToString();   //Get email
                EmailMaker.Send(user, "Ausschluss von Teilnahme an Ferienspass Mondpichl", Text);
                
                litEmailStatus.Text= "<div class='row'><div class='col'><div class='alert alert-success'>E-mail erfolgreich gesendet!</div></div></div>";
            }
            catch(Exception ex)
            {
                litEmailStatus.Text = "<div class='row'><div class='col'><div class='alert alert-danger'>E-mail senden fehlgeschlagen!</div></div></div>";
            }
        }
    }
}