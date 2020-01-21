using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ferienspass
{
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
            ((user_master)this.Master).SetBasketNumber(GlobalMethods.BasketCount(User.Identity.Name));
            if (!Page.IsPostBack)
            {
                Fill_gvUserCourses();
            }
        }

        private void Fill_gvUserCourses()
        {
            DB db = new DB();
            DataTable dtCompany = db.Query("SELECT *,courseID as current_id, " +
                "(SELECT COUNT(*) FROM kidparticipates WHERE kidparticipates.courseId=current_id) as cntparticipants FROM courses " +
                "LEFT JOIN organisation ON courses.organisationID = organisation.organisationID");
            DataView dvCompany = new DataView(dtCompany);
            dvCompany.Sort = SortExpresssion;

            gvUserCourses.DataSource = dvCompany;
            gvUserCourses.DataBind();
            gvUserCourses.HeaderRow.TableSection = TableRowSection.TableHeader;
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