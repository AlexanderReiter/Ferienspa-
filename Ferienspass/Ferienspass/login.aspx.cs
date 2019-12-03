using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

//Mair Andreas
//26.11.19
//Tasks: Login Formular erstellen + pw abgleichen

namespace Ferienspass
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (AllTxtsFilled())
            {
                string user = txtEmailaddress.Text;
                string pw = txtPassword.Text;

                DB db = new DB();
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
                        FormsAuthentication.RedirectFromLoginPage(user, false);
                    }
                    else
                    {
                        
                        litLoginFailed.Text = "<div class='row'><div class='col'><div class='alert alert-danger'>Login fehlgeschlagen!</div></div></div>";
                    }
                }
            }
            else
            {
                
                litLoginFailed.Text = "<div class='row'><div class='col'><div class='alert alert-danger'>Nicht alle Felder sind ausgefüllt!</div></div></div>";
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