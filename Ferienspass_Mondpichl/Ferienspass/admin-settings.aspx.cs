using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Ferienspass.GlobalMethods;

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
        // Task: Erlaubte Orte auflisten, löschen, hinzufügen
        // verified by Mair Andreas
        // 07.01.2020

        private void Fill_gvNeighbourcities()
        {
            DB db = new DB();
            gvNeighbourcities.DataSource = db.Query("SELECT * FROM neighbourcities");
            gvNeighbourcities.DataBind();
            gvNeighbourcities.HeaderRow.TableSection = TableRowSection.TableHeader;
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
            litAlertNeighbourcities.Text = "";
        }

        protected void gvNeighbourcities_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string[] currentZip = GetCurrentDatatable();

            DB db = new DB();
            if (currentZip.Length > e.RowIndex)
            {
                try
                {
                    int updatedRows = db.ExecuteNonQuery("UPDATE neighbourcities SET zipcode=?, city=? WHERE zipcode=?", e.NewValues["zipcode"], e.NewValues["city"], currentZip[Convert.ToInt32(e.RowIndex)]);
                    if (updatedRows >= 1) litAlertNeighbourcities.Text = "<div class='row'><div class='col'><div class='alert alert-success'>Erfolgreich geändert!</div></div></div>";
                    else litAlertNeighbourcities.Text = "<div class='row'><div class='col'><div class='alert alert-danger'>Änderung fehlgeschlagen!</div></div></div>";
                }
                catch
                {
                    litAlertNeighbourcities.Text= "<div class='row'><div class='col'><div class='alert alert-danger'>Änderung fehlgeschlagen!</div></div></div>";
                }
            }
            else
            {
                string sqlCheckZipExists = "SELECT COUNT(*) FROM neighbourcities WHERE zipcode=?";
                if (Convert.ToInt32(db.ExecuteScalar(sqlCheckZipExists, e.NewValues["zipcode"])) < 1)
                {
                    int insertedRows = db.ExecuteNonQuery("INSERT INTO neighbourcities VALUES(?,?)", e.NewValues["zipcode"], e.NewValues["city"]);
                    if (insertedRows >= 1) litAlertNeighbourcities.Text = "<div class='row'><div class='col'><div class='alert alert-success'>Neue Gemeinde erfolgreich hinzugefügt!</div></div></div>";
                    else litAlertNeighbourcities.Text = "<div class='row'><div class='col'><div class='alert alert-danger'>Gemeinde hinzufügen fehlgeschlagen!</div></div></div>";
                }
                else
                {
                    litAlertNeighbourcities.Text = "<div class='row'><div class='col'><div class='alert alert-danger'>Es existiert bereits eine Gemeinde mit diesem zipcode!</div></div></div>";
                }
            }
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


            int deletedRows = db.ExecuteNonQuery("DELETE FROM neighbourcities WHERE zipcode=?", zip);
            if(deletedRows>0) litAlertNeighbourcities.Text = "<div class='row'><div class='col'><div class='alert alert-success'>Gemeinde erfolgreich gelöscht!</div></div></div>";
            else litAlertNeighbourcities.Text = "<div class='row'><div class='col'><div class='alert alert-danger'>Gemeinde löschen fehlgeschlagen!</div></div></div>";
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
        // Email Sendedaten anzeigen, ändern
        // Verified by Mair Andreas
        // 07.01.2020
        private void LoadEmailSettings()
        {
            DataTable dt = GetDataTableFromSettings();

            txtEmail.Text = GetValueFromDataTable(dt, "email");
            txtHost.Text = GetValueFromDataTable(dt, "host");
            for (int i = 0; i < GetValueFromDataTable(dt,"password").Length; i++)    //Das Passwort wird nicht im Klartext angezeigt
            {
                txtPassword.Text += "•";
            }
            txtPort.Text = GetValueFromDataTable(dt, "port");
            txtResetDauer.Text = GetValueFromDataTable(dt, "resetpwdauer");
        }

        protected void btnChangeSettings_Click(object sender, EventArgs e)
        {
            DataTable dt = GetDataTableFromSettings();

            txtEmail.Enabled = true;
            txtHost.Enabled = true;
            txtPassword.Enabled = true;
            txtPassword.Text = GetValueFromDataTable(dt,"password");
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
            DataTable dt = GetDataTableFromSettings();

            txtEmail.Enabled = false;
            txtHost.Enabled = false;
            txtPassword.Enabled = false;
            txtPassword.Text = "";
            string pw = GetValueFromDataTable(dt, "password");
            for (int i = 0; i < pw.Length; i++)
            {
                txtPassword.Text += "•";
            }
            txtPort.Enabled = false;
            txtResetDauer.Enabled = false;
            pnlChangeSettings.Visible = true;
            pnlCancelSettings.Visible = false;
            pnlSaveSettings.Visible = false;
            LoadEmailSettings();
        }
        #endregion




        #region Other-Settings
        // Anmeldezeitraum anzeigen, ändern
        // Rabatt anzeigen, ändern
        // Verified by Mair Andreas
        // 07.01.2020
        private void LoadOtherSettings()
        {
            DataTable dt = GetDataTableFromSettings();

            txtStartRegistrationSpan.Text = GetValueFromDataTable(dt,"startregistration");
            txtStopRegistrationSpan.Text = GetValueFromDataTable(dt,"stopregistration");
            txtDiscount.Text = GetValueFromDataTable(dt,"discount");
            txtBasketExpiryTime.Text = GetValueFromDataTable(dt, "basketexpirytime");
        }

        protected void btnChangeOtherSettings_Click(object sender, EventArgs e)
        {
            txtStartRegistrationSpan.Enabled = true;
            txtStopRegistrationSpan.Enabled = true;
            txtDiscount.Enabled = true;
            txtBasketExpiryTime.Enabled = true;
            pnlChangeOtherSettings.Visible = false;
            pnlCancelOtherSettings.Visible = true;
            pnlSaveOtherSettings.Visible = true;
        }
        protected void btnSaveOtherSettings_Click(object sender, EventArgs e)
        {
            string newStartDate = txtStartRegistrationSpan.Text;
            string newEndDate = txtStopRegistrationSpan.Text;
            string newDiscount = txtDiscount.Text;
            string newBasketExpiryTime = txtBasketExpiryTime.Text;

            if (newDiscount.Contains("%"))
            {
                if (DateTime.TryParseExact(newStartDate, "dd/mm/yyyy", null, DateTimeStyles.None, out DateTime startdate) &&
                    DateTime.TryParseExact(newEndDate, "dd/mm/yyyy", null, DateTimeStyles.None, out DateTime enddate))
                {
                    litAlertOtherSettings.Text = "";
                    DB db = new DB();
                    db.ExecuteNonQuery("UPDATE settings SET VALUE=? WHERE settingId='startregistration'", newStartDate);
                    db.ExecuteNonQuery("UPDATE settings SET VALUE=? WHERE settingId='stopregistration'", newEndDate);
                    db.ExecuteNonQuery("UPDATE settings SET VALUE=? WHERE settingId='discount'", newDiscount);
                    db.ExecuteNonQuery("UPDATE settings SET VALUE=? WHERE settingId='basketexpirytime'", newBasketExpiryTime);

                    txtStartRegistrationSpan.Enabled = false;
                    txtStopRegistrationSpan.Enabled = false;
                    txtDiscount.Enabled = false;
                    txtBasketExpiryTime.Enabled = false;
                    pnlChangeOtherSettings.Visible = true;
                    pnlCancelOtherSettings.Visible = false;
                    pnlSaveOtherSettings.Visible = false;
                }
                else litAlertOtherSettings.Text = "<div class='row'><div class='col'><div class='alert alert-danger'>Das eingegebene Datum ist ungültig!</div></div></div>";
            }
            else litAlertOtherSettings.Text = "<div class='row'><div class='col'><div class='alert alert-danger'>Die Eingabe muss ein Prozentwert sein!</div></div></div>";
        }

        protected void btnCancelOtherSettings_Click(object sender, EventArgs e)
        {
            litAlertOtherSettings.Text = "";
            txtStartRegistrationSpan.Enabled = false;
            txtStopRegistrationSpan.Enabled = false;
            txtDiscount.Enabled = false;
            txtBasketExpiryTime.Enabled = false;
            pnlChangeOtherSettings.Visible = true;
            pnlCancelOtherSettings.Visible = false;
            pnlSaveOtherSettings.Visible = false;
            LoadOtherSettings();
        }


        #endregion
    }
}