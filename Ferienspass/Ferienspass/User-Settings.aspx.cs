using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ferienspass;

namespace Ferienspass
{
    public partial class User_Settings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                LoadUser();
                Fill_gvKids();
            }
        }

        public void LoadUser()
        {
            DB db = new DB();
            DataRow dr = db.Query("SELECT * FROM user WHERE userId=?", 2).Rows[0];

            txtEmail.Text = Convert.ToString(dr["email"]);
            txtPassword.Text = Convert.ToString(dr["password"]);
            txtGivenname.Text = Convert.ToString(dr["givenname"]);
            txtSurname.Text = Convert.ToString(dr["surname"]);
            txtZIP.Text = Convert.ToString(dr["zipcode"]);
            txtCity.Text = Convert.ToString(dr["city"]);
            txtStreet.Text = Convert.ToString(dr["streetname"]);
            txtNr.Text = Convert.ToString(dr["housenumber"]);
        }

        public void Fill_gvKids()
        {
            DB db = new DB();
            gvKids.DataSource = db.Query("SELECT * FROM kids WHERE userId=?", User.Identity.Name);
            gvKids.DataBind();
        }

        protected void btnChangeEmail_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {

        }

        protected void btnChangeName_Click(object sender, EventArgs e)
        {
            txtGivenname.Enabled = true;
            txtSurname.Enabled = true;
            pnlChangeName.Visible = false;
            pnlSaveName.Visible = true;
            pnlCancelName.Visible = true;
        }

        protected void btnChangeAdress_Click(object sender, EventArgs e)
        {

        }

        protected void btnSaveName_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelName_Click(object sender, EventArgs e)
        {
            txtGivenname.Enabled = false;
            txtSurname.Enabled = false;
            pnlChangeName.Visible = true;
            pnlSaveName.Visible = false;
            pnlCancelName.Visible = false;
        }
    }
}