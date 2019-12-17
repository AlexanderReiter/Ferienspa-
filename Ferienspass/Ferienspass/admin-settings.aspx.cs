using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ferienspass
{
    public partial class admin_settings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Fill_gvNeighbourcities();
                LoadSettings();
            }
        }

        #region Settings
        private void LoadSettings()
        {
            string[] values = GetValuesFromSettings();

            txtEmail.Text = Convert.ToString(values[0]);
            txtHost.Text = Convert.ToString(values[1]);
            for (int i = 0; i < Convert.ToString(values[2]).Length; i++)    //Das Passwort wird nicht im Klartext angezeigt
            {
                txtPassword.Text += "•";
            }
            txtPort.Text = Convert.ToString(values[3]);
            txtResetDauer.Text = Convert.ToString(values[4]);
        }

        protected void btnChangeSettings_Click(object sender, EventArgs e)
        {
            string[] values = GetValuesFromSettings();

            txtEmail.Enabled = true;
            txtHost.Enabled = true;
            txtPassword.Enabled = true;
            txtPassword.Text = Convert.ToString(values[2]);
            txtPort.Enabled = true;
            txtResetDauer.Enabled = true;
            pnlChangeSettings.Visible = false;
            pnlCancelSettings.Visible = true;
            pnlSaveSettings.Visible = true;
        }

        protected void btnSaveSettings_Click(object sender, EventArgs e)
        {
            string newEmail = txtEmail.Text;
            string newHost = txtHost.Text;
            string newPassword = txtPassword.Text;
            string newPort = txtPort.Text;
            string newResetDauer = txtResetDauer.Text;

            DB db = new DB();
            db.Query("UPDATE settings SET VALUE=? WHERE settingId='email'", newEmail);
            db.Query("UPDATE settings SET VALUE=? WHERE settingId='host'", newHost);
            db.Query("UPDATE settings SET VALUE=? WHERE settingId='password'", newPassword);
            db.Query("UPDATE settings SET VALUE=? WHERE settingId='port'", newPort);
            db.Query("UPDATE settings SET VALUE=? WHERE settingId='resetpwdauer'", newResetDauer);

            txtEmail.Enabled = false;
            txtHost.Enabled = false;
            txtPassword.Enabled = false;
            txtPort.Enabled = false;
            txtResetDauer.Enabled = false;
            pnlChangeSettings.Visible = true;
            pnlCancelSettings.Visible = false;
            pnlSaveSettings.Visible = false;

            txtPassword.Text = "";
            LoadSettings();
        }

        protected void btnCancelSettings_Click(object sender, EventArgs e)
        {
            string[] values = GetValuesFromSettings();

            txtEmail.Enabled = false;
            txtHost.Enabled = false;
            txtPassword.Enabled = false;
            txtPassword.Text = "";
            string pw = Convert.ToString(values[2]);
            for (int i = 0; i < pw.Length; i++)
            {
                txtPassword.Text += "•";
            }
            txtPort.Enabled = false;
            txtResetDauer.Enabled = false;
            pnlChangeSettings.Visible = true;
            pnlCancelSettings.Visible = false;
            pnlSaveSettings.Visible = false;
        }

        private string[] GetValuesFromSettings()
        {
            DB db = new DB();
            DataTable dt = db.Query("SELECT value FROM settings");
            string[] values = dt.Rows.OfType<DataRow>().Select(k => k[0].ToString()).ToArray();
            return values;
        }
        #endregion

        #region Neighbourcities

        private void Fill_gvNeighbourcities()
        {
            DB db = new DB();
            gvNeighbourcities.DataSource = db.Query("SELECT * FROM neighbourcities");
            gvNeighbourcities.DataBind();
        }

        protected void gvNeighbourcities_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvNeighbourcities.EditIndex = e.NewEditIndex;
            Fill_gvNeighbourcities();
        }

        protected void gvNeighbourcities_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvNeighbourcities.EditIndex = -1;
            Fill_gvNeighbourcities();
        }

        protected void gvNeighbourcities_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string[] currentZip = GetCurrentDatatable();

            DB db = new DB();
            db.Query("UPDATE neighbourcities SET zipcode=?, city=? WHERE zipcode=?", e.NewValues["zipcode"], e.NewValues["city"], currentZip[Convert.ToInt32(e.RowIndex)]);

            gvNeighbourcities.EditIndex = -1;
            Fill_gvNeighbourcities();
        }

        private string[] GetCurrentDatatable()
        {
            DB db = new DB();
            DataTable dt = db.Query("SELECT zipcode FROM neighbourcities");
            string[] zipcodes = dt.Rows.OfType<DataRow>().Select(k => k[0].ToString()).ToArray();
            return zipcodes;
        }

        protected void gvNeighbourcities_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DB db = new DB();

            GridViewRow row = gvNeighbourcities.Rows[Convert.ToInt32(e.RowIndex)];
            string zip = ((Label)row.FindControl("lblZipCode")).Text;


            db.Query("DELETE FROM neighbourcities WHERE zipcode=?", zip);

            Fill_gvNeighbourcities();
        }

        protected void gvNeighbourcities_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Add":
                    DB db = new DB();
                    DataTable dt = db.Query("SELECT * FROM neighbourcities");
                    DataRow newRow = dt.NewRow();
                    dt.Rows.Add(newRow);
                    gvNeighbourcities.DataSource = dt;
                    gvNeighbourcities.EditIndex = dt.Rows.Count - 1;
                    gvNeighbourcities.DataBind();
                    break;
            }

        }
        #endregion
    }
}