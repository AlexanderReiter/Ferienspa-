using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
                LoadEmailSettings();
                LoadOtherSettings();
            }
        }


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

        #region Email-Settings
        private void LoadEmailSettings()
        {
            string[] values = GetValuesFromSettings();

            txtEmail.Text = Convert.ToString(values[1]);
            txtHost.Text = Convert.ToString(values[2]);
            for (int i = 0; i < Convert.ToString(values[3]).Length; i++)    //Das Passwort wird nicht im Klartext angezeigt
            {
                txtPassword.Text += "•";
            }
            txtPort.Text = Convert.ToString(values[4]);
            txtResetDauer.Text = Convert.ToString(values[5]);
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
            LoadEmailSettings();
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

        #endregion

        private string[] GetValuesFromSettings()
        {
            DB db = new DB();
            DataTable dt = db.Query("SELECT value FROM settings");
            string[] values = dt.Rows.OfType<DataRow>().Select(k => k[0].ToString()).ToArray();
            return values;
        }


        #region Other-Settings
        private void LoadOtherSettings()
        {
            string[] values = GetValuesFromSettings();

            txtStartRegistrationSpan.Text = values[6];
            txtStopRegistrationSpan.Text = values[7];
            txtDiscount.Text = values[0];
        }

        protected void btnChangeOtherSettings_Click(object sender, EventArgs e)
        {
            txtStartRegistrationSpan.Enabled = true;
            txtStopRegistrationSpan.Enabled = true;
            txtDiscount.Enabled = true;
            pnlChangeOtherSettings.Visible = false;
            pnlCancelOtherSettings.Visible = true;
            pnlSaveOtherSettings.Visible = true;
        }
        protected void btnSaveOtherSettings_Click(object sender, EventArgs e)
        {
            string newStartDate = txtStartRegistrationSpan.Text;
            string newEndDate = txtStopRegistrationSpan.Text;
            string newDiscount = txtDiscount.Text;

            if (newDiscount.Contains("%"))
            {
                if (DateTime.TryParseExact(newStartDate, "dd/mm/yyyy", null, DateTimeStyles.None, out DateTime startdate) &&
                    DateTime.TryParseExact(newEndDate, "dd/mm/yyyy", null, DateTimeStyles.None, out DateTime enddate))
                {
                    DB db = new DB();
                    db.Query("UPDATE settings SET VALUE=? WHERE settingId='startregistration'", newStartDate);
                    db.Query("UPDATE settings SET VALUE=? WHERE settingId='stopregistration'", newEndDate);
                    db.Query("UPDATE settings SET VALUE=? WHERE settingId='discount'", newDiscount);

                    txtStartRegistrationSpan.Enabled = false;
                    txtStopRegistrationSpan.Enabled = false;
                    txtDiscount.Enabled = false;
                    pnlChangeOtherSettings.Visible = true;
                    pnlCancelOtherSettings.Visible = false;
                    pnlSaveOtherSettings.Visible = false;
                }
                else throw new ApplicationException("Das eingegebene Datum ist ungültig!");
            }
            else throw new ApplicationException("Die Eingabe muss ein Prozentwert sein!");
        }

        protected void btnCancelOtherSettings_Click(object sender, EventArgs e)
        {
            txtStartRegistrationSpan.Enabled = false;
            txtStopRegistrationSpan.Enabled = false;
            txtDiscount.Enabled = false;
            pnlChangeOtherSettings.Visible = true;
            pnlCancelOtherSettings.Visible = false;
            pnlSaveOtherSettings.Visible = false;
        }


        #endregion
    }
}