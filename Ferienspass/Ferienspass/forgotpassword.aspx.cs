using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ferienspass.Classes;

//Mair Andreas
//03.12.19
//Passwort vergessen + Passwort zurücksetzen

namespace Ferienspass
{
    public partial class forgotpassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGetNewPw_Click(object sender, EventArgs e)
        {
            if (AllTxtsFilled())
            {
                string sql = "SELECT * FROM user WHERE email=?";
                string user = txtEmail.Text;

                DB db = new DB();
                DataTable sqlreturn = db.Query(sql, user);
                if (sqlreturn.Rows.Count != 1)
                {
                    litEmailFailed.Text = "<div class='row'><div class='col'><div class='alert alert-danger'>PW zurücksetzen fehlgeschlagen!</div></div></div>";
                }
                else
                {
                    //delete old reset link
                    string sqlDeleteCode = "DELETE FROM resetpwcodes WHERE email=?";
                    try { db.ExecuteNonQuery(sqlDeleteCode, user); } catch { }

                    //create reset link
                    DateTime rightnow = DateTime.Now;
                    string VerificationCode = Password.GenerateSalt();
                    VerificationCode.Replace('+', 'o');
                    string sqlVerificationCode = "INSERT INTO resetpwcodes (email, code, date, time) VALUES (?,?,?,?)";
                    db.ExecuteNonQuery(sqlVerificationCode, user, VerificationCode, rightnow.Date, rightnow.TimeOfDay);
                    string time = rightnow.TimeOfDay.ToString();
                    string link = string.Format("https:" + "//localhost:44383/resetpassword.aspx?email={0}&code={1}&date={2}&time={3}", user, VerificationCode, rightnow.Date.ToString("dd-MM-yyyy"), time.Split('.').First().Replace(':','-'));
                    EmailMaker.Send(user, "Reset Passwort für Ferienspass", string.Format("Resetlink: "+link));
                    litEmailFailed.Text = "<div class='row'><div class='col'><div class='alert alert-success'>Erfolgreich! Überprüfen Sie Ihr Postfach!</div></div></div>";
                }
            }
            else
            {
                litEmailFailed.Text = "<div class='row'><div class='col'><div class='alert alert-danger'>Bitte geben Sie eine EmailAdresse ein!</div></div></div>";
            }

        }

        private bool AllTxtsFilled()
        {
            if (string.IsNullOrEmpty(txtEmail.Text)) return false;
            return true;
        }
    }
}