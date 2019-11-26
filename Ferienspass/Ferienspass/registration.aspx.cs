using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ferienspass
{
    public partial class registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/logout.aspx");
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (AllTxtsFilled())
            {
                if (EMailNotInDB(txtEMail.Text))
                {

                }
                else litAlert.Text = "<div class='row'><div class='col'><div class='alert alert-danger'>E-Mail ist bereits vorhanden!</div></div></div>";
            }
            else litAlert.Text = "<div class='row'><div class='col'><div class='alert alert-danger'>Alle Felder müssen ausgefüllt werden!</div></div></div>";
        }

        private bool AllTxtsFilled()
        {
            if (string.IsNullOrEmpty(txtGivenname.Text)) return false;
            if (string.IsNullOrEmpty(txtSurname.Text)) return false;
            if (string.IsNullOrEmpty(txtZIP.Text)) return false;
            if (string.IsNullOrEmpty(txtCity.Text)) return false;
            if (string.IsNullOrEmpty(txtStreet.Text)) return false;
            if (string.IsNullOrEmpty(txtNumber.Text)) return false;
            if (string.IsNullOrEmpty(txtEMail.Text)) return false;
            if (string.IsNullOrEmpty(txtPassword.Text)) return false;
            return true;
        }

        private bool CityIsAllowed(int zip)
        {
            DB db = new DB();

            List<int> zips = new List<int>();

            foreach (DataRow row in db.Query("SELECT * FROM neighbourcities").Rows)
            {
                zips.Add(Convert.ToInt32(row["zipcode"]));
            }

            return zips.Contains(zip);
        }

        private bool EMailNotInDB(string email)
        {
            DB db = new DB();

            if (db.ExecuteScalar("SELECT * FROM user WHERE email=?", email) == null) return true;
            return false;
        }
    }
}