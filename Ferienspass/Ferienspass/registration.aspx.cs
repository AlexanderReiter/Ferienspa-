using Ferienspass.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Programmer: Alexander Reiter
/// Date: 26.11.2019
/// //Verified by Josip
/// </summary>

namespace Ferienspass
{

    public partial class registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.Name != "registration")
            {
                FormsAuthentication.SignOut();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/logout.aspx");
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (AllTxtsFilled())
            {
                if (EMailMeetsCriteria(txtEMail.Text))
                {
                    if (CheckRepeatPassword())
                    {
                        if (EMailNotInDB(txtEMail.Text))
                        {
                            if (PasswordMeetsCriterias(txtPassword.Text))
                            {
                                DB db = new DB();
                                string salt = Password.GenerateSalt();
                                db.ExecuteNonQuery("INSERT INTO user (userstatus, givenname, surname, zipcode, city, streetname, housenumber, " +
                                    "email, password, passwordsalt, failedlogins, blocked) VALUES (1, ?, ?, ?, ?, ?, ?, ?, ?, ?, 0, 0)", txtGivenname.Text,
                                    txtSurname.Text, txtZIP.Text, txtCity.Text, txtStreet.Text, txtNumber.Text, txtEMail.Text,
                                    Password.EncryptPassword(txtPassword.Text, salt), salt);
                                SendAuthenticationEmail(txtEMail.Text);
                                Response.Redirect("~/logout.aspx");
                            }
                            else litAlert.Text = "<div class='row'><div class='col'><div class='alert alert-danger'>Passwort muss ... enthalten!</div></div></div>";
                        }
                        else litAlert.Text = "<div class='row'><div class='col'><div class='alert alert-danger'>E-Mail ist bereits vorhanden!</div></div></div>";
                    }
                    else litAlert.Text = "<div class='row'><div class='col'><div class='alert alert-danger'>Beide Passörter müssen übereinstimmen!</div></div></div>";
                }
                else litAlert.Text = "<div class='row'><div class='col'><div class='alert alert-danger'>Keine gültige E-Mail!</div></div></div>";
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

        private bool EMailMeetsCriteria(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool CheckRepeatPassword()
        {
            return txtPassword.Text == txtRepeatPassword.Text;
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

            return (db.ExecuteScalar("SELECT * FROM user WHERE email=?", email) == null);
        }

        private bool PasswordMeetsCriterias(string text)
        {
            return true;
        }

        private void SendAuthenticationEmail(string toMail)
        {
            string subject = "Bestätigung Ferienspaß Mondpichl";
            string body = "Content";
            EmailMaker.Send(toMail, subject, body);
        }
    }
}