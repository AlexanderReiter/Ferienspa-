using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ferienspass
{
    public partial class admin_settings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Fill_gvNeighbourcities();
            }
        }

        private void Fill_gvNeighbourcities()
        {
            DB db = new DB();
            gvNeighbourcities.DataSource = db.Query("SELECT * FROM neighbourcities");
            gvNeighbourcities.DataBind();
        }

        protected void gvNeighbourcities_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvNeighbourcities.EditIndex = e.NewEditIndex;
            Fill_gvNeighbourcities();
        }

        protected void gvNeighbourcities_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvNeighbourcities.EditIndex = -1;
            Fill_gvNeighbourcities();
        }

        protected void gvNeighbourcities_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DB db = new DB();

            GridViewRow row = gvNeighbourcities.Rows[Convert.ToInt32(e.RowIndex)];
            string zip = ((Label)row.FindControl("lblZipCode")).Text;


            db.Query("DELETE FROM neighbourcities WHERE zipcode=?", zip);

            Fill_gvNeighbourcities();
        }

        protected void gvNeighbourcities_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Add":
                    DB db = new DB();
                    DataTable dt = db.Query("SELECT * FROM neighbourcities LIMIT 1");
                    dt.Clear();
                    DataRow newRow = dt.NewRow();
                    newRow["zipcode"] = -1;
                    dt.Rows.Add(newRow);
                    gvNeighbourcities.DataSource = dt;
                    gvNeighbourcities.EditIndex = 0;
                    gvNeighbourcities.DataBind();
                    break;
            }

        }
    }
}