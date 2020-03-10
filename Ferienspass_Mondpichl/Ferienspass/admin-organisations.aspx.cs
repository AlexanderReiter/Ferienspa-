using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ferienspass
{
    public partial class admin_organisations : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                Fill_gvOrganisation();
            }
        }

        private string OrganisationID
        {
            get
            {
                return Convert.ToString(ViewState["DataKey"]);
            }
            set
            {
                ViewState["DataKey"] = value;
            }
        }

        private void Fill_gvOrganisation()
        {
            DB db = new DB();
            gvOrganisations.DataSource = db.Query("SELECT * FROM organisation");
            gvOrganisations.DataBind();
        }

        protected void gvOrganisations_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvOrganisations.EditIndex = e.NewEditIndex;
            Fill_gvOrganisation();
        }

        protected void gvOrganisations_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvOrganisations.EditIndex = -1;
            Fill_gvOrganisation();
        }

        protected void gvOrganisations_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow gvr = gvOrganisations.Rows[e.RowIndex];
            DB db = new DB();
            int organisationID;
            if (Convert.ToString(e.Keys[0]) == "")
            {
                organisationID = -1;
            }
            else
            {
                organisationID = Convert.ToInt32(e.Keys[0]);
            }

            if (organisationID != -1)
            {
                Control ctrlOrganisationName = gvOrganisations.Rows[e.RowIndex].FindControl("txtOrganisationName");
                TextBox txtOrganisationName = ctrlOrganisationName as TextBox;
                Control ctrlEmail = gvOrganisations.Rows[e.RowIndex].FindControl("txtEmail");
                TextBox txtEmail = ctrlEmail as TextBox;

                if (txtEmail.Text == "" || txtOrganisationName.Text == "")
                {
                    litError.Text = "<div class='row'><div class='col'><div class='alert alert-danger'>Alle Felder müssen ausgefühlt werden!</div></div></div>";
                }
                else
                {
                    db.Query("UPDATE organisation SET organisationname=?, email=? WHERE organisationId=?", e.NewValues["organisationname"], e.NewValues["email"], organisationID);
                }
            }
            else
            {
                Control ctrlOrganisationName = gvOrganisations.Rows[e.RowIndex].FindControl("txtOrganisationName");
                TextBox txtOrganisationName = ctrlOrganisationName as TextBox;
                Control ctrlEmail = gvOrganisations.Rows[e.RowIndex].FindControl("txtEmail");
                TextBox txtEmail = ctrlEmail as TextBox;
                if (txtOrganisationName.Text == "" || txtEmail.Text == "")
                {
                    litError.Text = "<div class='row'><div class='col'><div class='alert alert-danger'>Alle Felder müssen ausgefühlt werden!</div></div></div>";
                }
                else
                {
                    db.ExecuteNonQuery($"INSERT INTO organisation (organisationname, email) VALUES(?,?)", e.NewValues["organisationname"], e.NewValues["email"]);
                }
            }

            gvOrganisations.EditIndex = -1;
            Fill_gvOrganisation();
        }

        protected void gvOrganisations_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch(e.CommandName)
            {
                case "Add":
                    DB db = new DB();
                    DataTable dt = db.Query("SELECT * FROM organisation");
                    DataRow newRow = dt.NewRow();
                    dt.Rows.Add(newRow);
                    gvOrganisations.EditIndex = dt.Rows.Count - 1;
                    gvOrganisations.DataSource = dt;
                    gvOrganisations.DataBind();
                    break;
            }
        }
    }
}