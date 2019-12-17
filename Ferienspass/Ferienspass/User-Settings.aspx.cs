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
/// Date: 23.11.2019
/// Verified by: N/A
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
            btnChangeName.Enabled = true;
            btnChangePassword.Enabled = true;
        }

        protected void gvKids_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvKids.EditIndex = -1;
            Fill_gvKids();
        }

        protected void gvKids_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvKids.EditIndex = e.NewEditIndex;
            Fill_gvKids();
        }

        protected void gvKids_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Add":
                    DB db = new DB();
                    DataTable dt = db.Query("SELECT * FROM kids");
                    DataRow newRow = dt.NewRow();
                    dt.Rows.Add(newRow);
                    gvKids.EditIndex = dt.Rows.Count;
                    gvKids.DataSource = dt;
                    gvKids.DataBind();
                    break;
            }
        }

        protected void gvKids_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow gvr = gvKids.Rows[e.RowIndex];
            DB db = new DB();
            int kidID = Convert.ToInt32(e.Keys[0]);

            if(kidID != -1)
            {
                db.Query("UPDATE kids SET givenname=?, surname=?, gender=?, birthday=? WHERE kidId=?", e.NewValues["givenname"], e.NewValues["surname"], e.NewValues["gender"], e.NewValues["birthday"], e.Keys[0]);
            }
            else
            {
                db.Query("INSERT INTO kids (givenname, surname, gender, birthday, email) VALUES(?,?,?,?,?)", e.NewValues["givenname"], e.NewValues["surname"], e.NewValues["gender"], e.NewValues["birthday"], User.Identity.Name);
            }

            gvKids.EditIndex = -1;
            Fill_gvKids();
        }

        private string DataKey
        {
            get
            {
                return Convert.ToString(ViewState["DataKey"]);
            }
            set
            {
                ViewState["DataKey"] = value;
            }
        }

        protected void gvKids_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DB db = new DB();
            DataKey = Convert.ToString(e.Keys[0]);

            db.Query("DELETE FROM kids WHERE kidId=?", DataKey);

            Fill_gvKids();
        }
    }
}