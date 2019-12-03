using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Programmer: Kollross Marcel
/// </summary>
namespace Ferienspass
{
    public partial class user_changepassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSavePassword_Click(object sender, EventArgs e)
        {
            string oldPassword = txtOldPassword.Text;
            
        }
    }
}