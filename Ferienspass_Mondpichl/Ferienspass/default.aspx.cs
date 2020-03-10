using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ferienspass
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        /// <summary>
        /// Programmer: Alexander Reiter
        /// Date: 03.12.2019
        /// Verified by: Josip Gabric
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.Name == "registration") Response.Redirect("~/registration.aspx");
            else if (User.Identity.Name == "pwforgotten") Response.Redirect("~/forgotpassword.aspx");
            else if (Check.IsAdmin(User.Identity.Name)) Response.Redirect("~/admin-courses.aspx");
            else if (Check.IsUser(User.Identity.Name)) Response.Redirect("~/user-home.aspx");
        }
    }
}