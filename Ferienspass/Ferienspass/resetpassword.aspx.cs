using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

// Programmer: Mair Andreas
// Date: 03.12.19
// Tasks: Pw vergessen Pw zurücksetzen



namespace Ferienspass
{
    public partial class resetpassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack) FormsAuthentication.SetAuthCookie("resetpassword", true);
            if (!IsPostBack)
            {
                string mail = Convert.ToString(Request.QueryString["email"]);
                string code = Convert.ToString(Request.QueryString["code"]);
                DB db = new DB();
                string sqlCheckCode = "SELECT * FROM resetpwcodes WHERE email=? AND code=?";
                DataTable dt = db.Query(sqlCheckCode, mail, code);
                if (dt.Rows.Count == 0)
                {
                    Response.Redirect("logout.aspx");
                }
            }
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