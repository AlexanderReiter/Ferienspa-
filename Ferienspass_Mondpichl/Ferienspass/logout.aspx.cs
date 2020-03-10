using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ferienspass
{
    /// <summary>
    /// Programmer: Alexander Reiter
    /// Date: 03.12.2019
    /// Verified by: Josip Gabric
    /// </summary>
    public partial class logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Logout();
        }

        private void Logout()
        {
            FormsAuthentication.SignOut();
            Response.Redirect("default.aspx");
        }
    }
}