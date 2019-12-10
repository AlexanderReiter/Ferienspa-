using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ferienspass
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) Fill_gvNAPeople();
        }

        private void Fill_gvNAPeople()
        {
            DB db = new DB();
            string sqlGetNAPeople = $"SELECT t1.givenname, t1.surname, t1.zipcode, t1.city, t1.streetname, t1.housenumber, t1.email " +
                    "FROM user As t1 " +
                    "WHERE t1.zipcode NOT IN " +
                    "(SELECT t2.zipcode " +
                    "FROM neighbourcities AS t2 " +
                    "WHERE t2.zipcode IS NOT NULL)";
            DataTable dt = db.Query(sqlGetNAPeople);
            gvNAPeople.DataSource = dt;
            gvNAPeople.DataBind();
        }
    }
}