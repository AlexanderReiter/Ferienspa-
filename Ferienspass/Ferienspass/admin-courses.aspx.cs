using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ferienspass
{
    public partial class admin_courses : System.Web.UI.Page
    {
        public string SortExpresssion 
        {
            set
            {
                if (SortExpresssion.StartsWith(value) && !SortExpresssion.EndsWith("DESC"))
                {
                    ViewState["sortexpression"] = value + " DESC";
                }
                else
                {
                    ViewState["sortexpression"] = value;
                }
            }

            get
            {
                return (ViewState["sortexpression"] ?? string.Empty).ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Fill_gvcourses();
            }
        }

        private void Fill_gvcourses()
        {
            DB db = new DB();
            DataTable dtCompany = db.Query("SELECT *,coursID as current_id, " +
                "(SELECT COUNT(*) FROM kidparticipates WHERE kidparticipates.courseId=current_id) as cntparticipants FROM courses " +
                "LEFT JOIN organisation ON courses.organisationID = organisation.organisationID");
            DataView dvCompany = new DataView(dtCompany);
            dvCompany.Sort = SortExpresssion;

            gvCourses.DataSource = dvCompany;
            gvCourses.DataBind();
        }

        protected void btnNewCours_Click(object sender, EventArgs e)
        {
            panBlockBackground.Visible = true;
            panNewCours.Visible = true;
        }

        protected void btnMail_Click(object sender, EventArgs e)
        {

        }

        protected void gvCourses_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gvCourses_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void gvCourses_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void gvCourses_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvCourses_Sorting(object sender, GridViewSortEventArgs e)
        {

        }
    }
}