using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

// Programmer: Mair Andreas
// Date: 26.11.19
// Tasks: Login Formular erstellen + pw abgleichen

/// <summary>
/// Verified by Josip
/// </summary>

namespace Ferienspass
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string mail = Convert.ToString(Request.QueryString["email"]);
                    if (!string.IsNullOrEmpty(mail))
                    {
                        FormsAuthentication.RedirectFromLoginPage("getnewpw", false);
                        Response.Redirect("logout", false);
                    }
                }
                catch { }
                
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (AllTxtsFilled())
            {
                string user = txtEmailaddress.Text;
                string pw = txtPassword.Text;
                DB db = new DB();

                string sqlFailedLoginAttempts = "SELECT DISTINCT(failedlogins) FROM user WHERE email=?";
                int failedLoginAttempts = Convert.ToInt32(db.ExecuteScalar(sqlFailedLoginAttempts, user));

                if (failedLoginAttempts < 3)
                {
                    string sql = "SELECT password, passwordsalt FROM user WHERE email=?";
                    DataTable sqlreturn = db.Query(sql, user);
                    if (sqlreturn.Rows.Count == 0)
                    {
                        litLoginFailed.Text = "<div class='row'><div class='col'><div class='alert alert-danger'>Login fehlgeschlagen!</div></div></div>";
                    }
                    else
                    {
                        string pwSalt;
                        string pwHash;
                        try
                        {
                            pwSalt = Convert.ToString(sqlreturn.Rows[0]["passwordsalt"]);
                            pwHash = Convert.ToString(sqlreturn.Rows[0]["password"]);
                        }
                        catch { throw new ApplicationException("Internal Error! Salt not found"); }

                        if (pwHash == Password.EncryptPassword(pw, pwSalt))
                        {
                            //Login erfolgreich
                            FormsAuthentication.RedirectFromLoginPage(user, false);
                        }
                        else
                        {
                            // Anzahl der fehlgeschlagenen Logins erhöhen
                            string sqlIncreaseFailedLogins = "UPDATE user SET failedlogins = failedlogins + 1 WHERE email=?";
                            try { db.ExecuteNonQuery(sqlIncreaseFailedLogins, user); } catch { }

                            // Nach dem ungültigem 3. Versuch soll direkt angezeit werden, 
                            // dass der user gesperrt ist, nicht erst bei erneutem login.
                            // Ansonsten wird angezeigt, dass der login fehlgeschlagen ist
                            // und ein Hinweis gegeben, dass der user nach 3 fehlschlägen gesperrt wird.
                            string sqlGetNumFailedLoginAttempts = "SELECT DISTINCT(failedlogins) FROM user WHERE email=?";
                            int numOfFailedLoginAttempts = Convert.ToInt32(db.ExecuteScalar(sqlGetNumFailedLoginAttempts, user));

                            if (numOfFailedLoginAttempts < 3)
                            {
                                litLoginFailed.Text = "<div class='row'><div class='col'><div class='alert alert-danger'>Login fehlgeschlagen!</div></div></div>";
                                litLoginInfo.Text = "<div class='row'><div class='col'><div class='alert alert-info'>Benutzer werden nach 3 fehlgeschlagenen Loginversuchen gesperrt.</div></div></div>";
                            }
                            else
                            {
                                litLoginFailed.Text = "<div class='row'><div class='col'><div class='alert alert-danger'>Nutzer gesperrt! Zu viele ungültige Loginversuche!</div></div></div>";
                                litLoginInfo.Text = "";
                            }
                        }
                    }
                }
                else
                {
                    litLoginFailed.Text = "<div class='row'><div class='col'><div class='alert alert-danger'>Nutzer gesperrt! Zu viele ungültige Loginversuche!</div></div></div>";
                    litLoginInfo.Text = "";
                }
            }
            else
            {
                litLoginFailed.Text = "<div class='row'><div class='col'><div class='alert alert-danger'>Nicht alle Felder sind ausgefüllt!</div></div></div>";
                litLoginInfo.Text = "";
            }
        }

        private bool AllTxtsFilled()
        {
            if (string.IsNullOrEmpty(txtEmailaddress.Text)) return false;
            if (string.IsNullOrEmpty(txtPassword.Text)) return false;
            return true;
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            FormsAuthentication.RedirectFromLoginPage("registration", false);
        }

        protected void btnPasswortVergessen_Click(object sender, EventArgs e)
        {
            FormsAuthentication.RedirectFromLoginPage("pwforgotten", false);
        }
    }
}