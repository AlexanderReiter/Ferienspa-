using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ferienspass
{
    public partial class resetpassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnResetPw_Click(object sender, EventArgs e)
        {
            if (AllTxtsFilled())
            {
                if (PwsMatch())
                {
                    string mail = Convert.ToString(Request.QueryString["email"]);
                    string sql = "Update user SET password=?, passwordsalt=? WHERE email=?";
                    string salt = Password.GenerateSalt();
                    string pwHash = Password.EncryptPassword(txtNewPw1.Text, salt);
                    DB db = new DB();
                    if (db.ExecuteNonQuery(sql, pwHash, salt, mail) > 0) litResetFailed.Text = "<div class='row'><div class='col'><div class='alert alert-success'>Passworter erfolgreich zurückgesetzt!</div></div></div>";
                    else litResetFailed.Text = "<div class='row'><div class='col'><div class='alert alert-danger'>Passwort ändern fehlgeschlagen!</div></div></div>";
                }
                else
                {
                    litResetFailed.Text = "<div class='row'><div class='col'><div class='alert alert-danger'>Passworter stimmen nicht überein!</div></div></div>";
                }

            }
            else litResetFailed.Text = "<div class='row'><div class='col'><div class='alert alert-danger'>Bitte geben Sie ein Passwort ein!</div></div></div>";
        }

        private bool PwsMatch()
        {
            if (string.Compare(txtNewPw1.Text, txtNewPw2.Text) == 0) return true;
            return false;
        }

        private bool AllTxtsFilled()
        {
            if (string.IsNullOrEmpty(txtNewPw1.Text)) return false;
            if (string.IsNullOrEmpty(txtNewPw2.Text)) return false;
            return true;
        }
    }
}