using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ferienspass;

/// <summary>
/// Programmer: Kollross Marcel
/// Date: 23.11.2019
/// Verified by: Reiter Alexander
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
            gvKids.DataSource = db.Query("SELECT *, gender.name AS gendername FROM kids LEFT JOIN gender ON gender.id=kids.gender WHERE email=?", User.Identity.Name);
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
            db.Query("UPDATE user SET city=?, streetname=?, zipcode=?, housenumber=? WHERE email=?", newCity, newStreet, newZIP, newNr, User.Identity.Name);

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
                    DataTable dt = db.Query("SELECT *, gender.name AS gendername FROM kids LEFT JOIN gender ON gender.id=kids.gender WHERE email=?", User.Identity.Name);
                    DataRow newRow = dt.NewRow();
                    dt.Rows.Add(newRow);
                    gvKids.EditIndex = dt.Rows.Count - 1;
                    gvKids.DataSource = dt;
                    gvKids.DataBind();
                    break;
            }
        }

        protected void gvKids_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow gvr = gvKids.Rows[e.RowIndex];
            DB db = new DB();
            int kidID;
            if(Convert.ToString(e.Keys[0]) == "")
            {
                kidID = -1;
            }
            else
            {
                kidID = Convert.ToInt32(e.Keys[0]);
            }
            DateTime date = Convert.ToDateTime(e.NewValues["birthday"]);

            if (kidID != -1)
            {
                Control ctrlSurname = gvKids.Rows[e.RowIndex].FindControl("txtSurnameChild");
                TextBox txtSurname = ctrlSurname as TextBox;
                Control ctrlGivenname = gvKids.Rows[e.RowIndex].FindControl("txtGivennameChild");
                TextBox txtGivenname = ctrlGivenname as TextBox;
                Control ctrlBirthday = gvKids.Rows[e.RowIndex].FindControl("txtBirthday");
                TextBox txtBirthday = ctrlBirthday as TextBox;
                Control ctrlGender = gvKids.Rows[e.RowIndex].FindControl("ddlGender");
                DropDownList ddl = ctrlGender as DropDownList;
                if (ddl.SelectedItem.Text == "" || ddl.SelectedItem.Text == "Nicht ausgewählt" || txtGivenname.Text == "" || txtSurname.Text == "" || txtBirthday.Text == "")
                {
                    litGenderError.Text = "<div class='row'><div class='col'><div class='alert alert-danger'>Alle Felder müssen ausgefühlt werden!</div></div></div>";
                }
                else
                {
                    db.Query("UPDATE kids SET givenname=?, surname=?, gender=?, birthday=? WHERE kidId=?", e.NewValues["givenname"], e.NewValues["surname"], ddl.SelectedIndex, date, e.Keys[0]);
                }
            }
            else
            {
                Control ctrlSurname = gvKids.Rows[e.RowIndex].FindControl("txtSurnameChild");
                TextBox txtSurname = ctrlSurname as TextBox;
                Control ctrlGivenname = gvKids.Rows[e.RowIndex].FindControl("txtGivennameChild");
                TextBox txtGivenname = ctrlGivenname as TextBox;
                Control ctrlBirthday = gvKids.Rows[e.RowIndex].FindControl("txtBirthday");
                TextBox txtBirthday = ctrlBirthday as TextBox;
                Control ctrlGender = gvKids.Rows[e.RowIndex].FindControl("ddlGender");
                DropDownList ddl = ctrlGender as DropDownList;
                if (ddl.SelectedItem.Text == "" || txtGivenname.Text == "" || txtSurname.Text == "" || txtBirthday.Text == "")
                {
                    litGenderError.Text = "<div class='row'><div class='col'><div class='alert alert-danger'>Alle Felder müssen ausgefühlt werden!</div></div></div>";
                }
                else
                {
                    db.ExecuteNonQuery($"INSERT INTO kids (givenname, surname, gender, birthday, email) VALUES(?,?,?,?,?)", e.NewValues["givenname"], e.NewValues["surname"], ddl.SelectedIndex, date, User.Identity.Name);
                }
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

        protected void gvKids_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.DataRow && gvKids.EditIndex == e.Row.RowIndex)
            {
                Control ctrl = e.Row.FindControl("ddlGender");
                DropDownList ddl = ctrl as DropDownList;
                DataTable dt = GetGender();

                for(int i = 0;i<dt.Rows.Count;i++)
                {
                    ddl.Items.Add(new ListItem(dt.Rows[i]["name"].ToString(), dt.Rows[i]["id"].ToString()));
                }
                DataRowView drv = e.Row.DataItem as DataRowView;
                ddl.SelectedValue = drv["id"].ToString();
                ddl.SelectedItem.Text = drv["name"].ToString();
            }           
        }

        public DataTable GetGender()
        {
            DB db = new DB();
            DataTable dt = db.Query("SELECT * FROM gender");
            return dt;
        }
    }
}