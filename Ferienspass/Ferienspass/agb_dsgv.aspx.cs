using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ferienspass
{
    public partial class agb_dsgv : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.ContentType = "pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=SailBig.jpg");
            Response.TransmitFile(Server.MapPath("~/App_Data/AGB_Vorlage1.pdf"));
            Response.End();
        }
    }
}