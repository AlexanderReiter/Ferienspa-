using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Verwaltung_Themenfahrten;

namespace Ferienspass
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtEmailaddress.Text;
            string pw = txtPassword.Text;

            DB db = new DB();
            string sql = "SELECT password, passwordsalt WHERE email=?";
            DataTable sqlreturn = db.Query(sql, user);
            if (sqlreturn==null)
            {

            }
            else
            {
                string pwSalt;
                try
                {
                    pwSalt = Convert.ToString(sqlreturn.Rows[0]["passwordsalt"]);
                }
                catch { throw new ApplicationException("Internal Error! Salt not found"); }
                
                
            }


        }
    }
}