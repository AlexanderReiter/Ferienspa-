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
        protected void Page_Load(object sender, EventArgs e)
        {
            //Alexander Reiter
            if (User.Identity.Name == "registration") Response.Redirect("~/registration.aspx");
            else if (User.Identity.Name == "admin") Response.Redirect("~/admin");
            else if (User.Identity.Name == "user") Response.Redirect("~/user-settings.aspx");
        }
    }
}