using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ferienspass
{
    public partial class edit_users : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Fill_gvUser();
            }
        }

        private void Fill_gvUser()
        {
            DB db = new DB();

            DataTable dtUser = db.Query("SELECT * from user");

            gvUser.DataSource = dtUser;
            gvUser.DataBind();
            gvUser.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void gvUser_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //Kurs löschen
            DB db = new DB();

            db.Query("DELETE FROM user WHERE email=?", e.Keys[0]);
            gvUser.EditIndex = -1;

            Fill_gvUser();

            //Falls bestätigt, dann E-Mail versenden

        }
    }
}