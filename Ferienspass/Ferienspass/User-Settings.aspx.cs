using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ferienspass;

/// <summary>
/// Programmer: Kollross Marcel
/// </summary>
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
            DataRow dr = db.Query("SELECT * FROM user WHERE email=?", User.Identity.Name).Rows[0];

            txtEmail.Text = Convert.ToString(dr["email"]);
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
            gvKids.DataSource = db.Query("SELECT * FROM kids WHERE email=?", User.Identity.Name);
            gvKids.DataBind();
        }

        //protected void btnChangeEmail_Click(object sender, EventArgs e)
        //{
        //    txtEmail.Enabled = true;
        //    pnlChangeEmail.Visible = false;
        //    pnlSaveCancelEmail.Visible = true;
        //    btnChangeAdress.Enabled = false;
        //    btnChangeName.Enabled = false;
        //    btnChangePassword.Enabled = false;
        //}

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/user-changepassword.aspx");
        }

        protected void btnChangeName_Click(object sender, EventArgs e)
        {
            txtGivenname.Enabled = true;
            txtSurname.Enabled = true;
            pnlChangeName.Visible = false;
            pnlSaveName.Visible = true;
            pnlCancelName.Visible = true;
            //btnChangeEmail.Enabled = false;
            btnChangeAdress.Enabled = false;
            btnChangePassword.Enabled = false;
        }

        protected void btnChangeAdress_Click(object sender, EventArgs e)
        {
            txtCity.Enabled = true;
            txtStreet.Enabled = true;
            txtZIP.Enabled = true;
            txtNr.Enabled = true;
            pnlChangeAdress.Visible = false;
            pnlSaveAdress.Visible = true;
            pnlCancelAdress.Visible = true;
            //btnChangeEmail.Enabled = false;
            btnChangeName.Enabled = false;
            btnChangePassword.Enabled = false;
        }

        protected void btnSaveName_Click(object sender, EventArgs e)
        {
            string newGivenname = txtGivenname.Text;
            string newSurname = txtSurname.Text;

            DB db = new DB();
            db.Query("UPDATE user SET givenname=? , surname=? WHERE email=?", newGivenname, newSurname, User.Identity.Name);

            txtGivenname.Enabled = false;
            txtSurname.Enabled = false;
            pnlChangeName.Visible = true;
            pnlSaveName.Visible = false;
            pnlCancelName.Visible = false;
            //btnChangeEmail.Enabled = true;
            btnChangeAdress.Enabled = true;
            btnChangePassword.Enabled = true;
        }

        protected void btnCancelName_Click(object sender, EventArgs e)
        {
            txtGivenname.Enabled = false;
            txtSurname.Enabled = false;
            pnlChangeName.Visible = true;
            pnlSaveName.Visible = false;
            pnlCancelName.Visible = false;
            //btnChangeEmail.Enabled = true;
            btnChangeAdress.Enabled = true;
            btnChangePassword.Enabled = true;
        }

        protected void btnSaveAdress_Click(object sender, EventArgs e)
        {
            string newCity = txtCity.Text;
            string newStreet = txtStreet.Text;
            string newZIP = txtZIP.Text;
            string newNr = txtNr.Text;

            DB db = new DB();
            db.Query("UPDATE user SET city=?, street=?, zipcode=?, housenumber=? WHERE email=?", newCity, newStreet, newZIP, newNr, User.Identity.Name);

            txtCity.Enabled = false;
            txtStreet.Enabled = false;
            txtZIP.Enabled = false;
            txtNr.Enabled = false;
            pnlChangeAdress.Visible = true;
            pnlSaveAdress.Visible = false;
            pnlCancelAdress.Visible = false;
            //btnChangeEmail.Enabled = true;
            btnChangeName.Enabled = true;
            btnChangePassword.Enabled = true;
        }

        protected void btnCancelAdress_Click(object sender, EventArgs e)
        {
            txtCity.Enabled = false;
            txtStreet.Enabled = false;
            txtZIP.Enabled = false;
            txtNr.Enabled = false;
            pnlChangeAdress.Visible = true;
            pnlSaveAdress.Visible = false;
            pnlCancelAdress.Visible = false;
            //btnChangeEmail.Enabled = true;
            btnChangeName.Enabled = true;
            btnChangePassword.Enabled = true;
        }

        //protected void btnSaveEmail_Click(object sender, EventArgs e)
        //{
        //    string newEmail = txtEmail.Text;

        //    DB db = new DB();
        //    db.Query("UPDATE user SET email=? WHERE email=?", newEmail, User.Identity.Name);

        //    txtEmail.Enabled = false;
        //    pnlChangeEmail.Visible = true;
        //    pnlSaveCancelEmail.Visible = false;
        //    btnChangeAdress.Enabled = true;
        //    btnChangeName.Enabled = true;
        //    btnChangePassword.Enabled = true;
        //}

        //protected void btnCancelEmail_Click(object sender, EventArgs e)
        //{
        //    txtEmail.Enabled = false;
        //    pnlChangeEmail.Visible = true;
        //    pnlSaveCancelEmail.Visible = false;
        //    btnChangeAdress.Enabled = true;
        //    btnChangeName.Enabled = true;
        //    btnChangePassword.Enabled = true;
        //}
    }
}