using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ferienspass
{
    // Auswählen der Kinder + Anmelden + Liste Kurse (user kind anmelden)
    // verified by Andi
    // 28.01.2020

    public partial class user_courses : System.Web.UI.Page
    {
        public int CourseId
        {
            set
            {
                ViewState["courseid"] = value;
            }
            get 
            { 
                return Convert.ToInt32(ViewState["courseid"]); 
            }
        }

        private string SortExpression
        {
            get
            {
                return (ViewState["SortExpression"] ?? string.Empty).ToString();
            }
            set
            {
                if (SortExpression.StartsWith(value) && (!SortExpression.EndsWith("DESC")))
                {
                    ViewState["SortExpression"] = value + " DESC";
                }
                else
                {
                    ViewState["SortExpression"] = value;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ((user_master)this.Master).SetBasketNumber(GlobalMethods.BasketCount(User.Identity.Name));
            if (!Page.IsPostBack)
            {
                Fill_gvUserCourses();
            }
        }

        protected void btnUserSearchCourse_Click(object sender, EventArgs e)
        {
            Fill_gvUserCourses();
        }

        private void Fill_gvUserCourses()
        {
            DB db = new DB();

            string queryString = "SELECT *,courseID as current_id, " +
                "(SELECT COUNT(*) FROM kidparticipates WHERE kidparticipates.courseId=current_id) as cntparticipants FROM courses " +
                "LEFT JOIN organisation ON courses.organisationID = organisation.organisationID";

            if (!string.IsNullOrEmpty(txtSearchbar.Text))   //Suchabfrage
            {
                queryString += $" WHERE courses.coursename LIKE '%{txtSearchbar.Text}%' OR organisation.organisationname LIKE '%{txtSearchbar.Text}%'";
            }

            DataTable dtCompany = db.Query(queryString);
            DataView dvCompany = new DataView(dtCompany);
            dvCompany.Sort = SortExpression;

            gvUserCourses.DataSource = dvCompany;
            gvUserCourses.DataBind();
            gvUserCourses.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void gvUserCourses_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortExpression = e.SortExpression;
            Fill_gvUserCourses();
        }

        protected void gvUserCourses_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUserCourses.PageIndex = e.NewPageIndex;
            Fill_gvUserCourses();
        }

        protected void gvUserCourses_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string command = e.CommandName;

            switch (command)
            {
                case "ShowDetails":
                    int courseID = Convert.ToInt32(e.CommandArgument.ToString());

                    CourseId = courseID;

                    DB db = new DB();
                    DataTable dt = db.Query("SELECT * FROM courses LEFT JOIN organisation ON courses.organisationId=organisation.organisationId WHERE courseId=?", courseID);
                    DataRow dr = dt.Rows[0];

                    txtCourseName.Text = (string)dr["coursename"];
                    txtDesciption.InnerText = (string)dr["description"];
                    TimeSpan timeFrom = (TimeSpan)dr["timefrom"];
                    TimeSpan timeTo = (TimeSpan)dr["timeto"];
                    txtFrom.Text = timeFrom.ToString();
                    txtTo.Text = timeTo.ToString();
                    txtMinParticipants.Text = Convert.ToString((int)dr["minparticipants"]);
                    txtMaxParticipants.Text = Convert.ToString((int)dr["maxparticipants"]);
                    txtZIP.Text = (string)dr["zipcode"];
                    txtCity.Text = (string)dr["city"];
                    txtStreet.Text = (string)dr["streetname"];
                    txtNr.Text = (string)dr["housenumber"];
                    DateTime date = Convert.ToDateTime(dr["date"]);
                    calendar.SelectedDate = date;
                    calendar.VisibleDate = date;
                    txtManagerName.Text = (string)dr["managername"];
                    txtContactMail.Text = (string)dr["contactemail"];
                    txtPrice.Text = "€ " + Convert.ToString((decimal)dr["price"]);
                    txtOrganisation.Text = (string)dr["organisationname"];

                    panBlockBackground.Visible = true;
                    panCourse.Visible = true;
                    break;
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            DataTable dtKids = db.Query("Select * FROM kids WHERE email=?", User.Identity.Name);
            DataView dvKids = new DataView(dtKids);

            gvKids.DataSource = dvKids;
            gvKids.DataBind();
            gvKids.HeaderRow.TableSection = TableRowSection.TableHeader;

            panCourse.Visible = false;
            panSelectKids.Visible = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            panCourse.Visible = false;
            panBlockBackground.Visible = false;
        }

        protected void btnKidsCancel_Click(object sender, EventArgs e)
        {
            panCourse.Visible = true;
            panSelectKids.Visible = false;
        }

        protected void btnKidsAddToBasket_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            List<int> kidsAllreadyRegistered = new List<int>();

            foreach (GridViewRow row in gvKids.Rows)
            {
                CheckBox cbx = row.FindControl("cbxKid") as CheckBox;
                if (cbx.Checked)
                {
                    int cnt = Convert.ToInt32(db.ExecuteScalar("SELECT COUNT(*) FROM basket WHERE courseId=? AND kidId=?", CourseId, gvKids.DataKeys[row.RowIndex].Value));
                    cnt += Convert.ToInt32(db.ExecuteScalar("SELECT COUNT(*) FROM kidparticipates WHERE courseId=? AND kidId=?", CourseId, gvKids.DataKeys[row.RowIndex].Value));
                    if (cnt == 0)
                    {
                        db.ExecuteNonQuery("INSERT INTO basket (userId, kidId, courseId, date) VALUES (?, ?, ?, ?)", User.Identity.Name, gvKids.DataKeys[row.RowIndex].Value, CourseId, DateTime.Now);
                        ((user_master)this.Master).SetBasketNumber(GlobalMethods.BasketCount(User.Identity.Name));
                    }
                    else kidsAllreadyRegistered.Add(Convert.ToInt32(gvKids.DataKeys[row.RowIndex].Value));
                }
            }

            panSelectKids.Visible = false;
            panBlockBackground.Visible = false;

            if (kidsAllreadyRegistered.Count != 0)
            {
                litAlert.Text = "<div class='alert alert-danger'><strong>Achtung!</strong> Ein/mehrere Anmeldungen wurden nicht zum Warenkorb hinzugefügt da sie sich entweder bereits dort befinden oder schon angemeldet sind.</div>";
            }
        }

    }
}