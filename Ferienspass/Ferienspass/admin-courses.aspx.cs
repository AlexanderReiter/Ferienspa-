using Ferienspass.Classes;
using System;
using System.Data;
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
            DataTable dtCompany = db.Query("SELECT *,courseID as current_id, " +
                "(SELECT COUNT(*) FROM kidparticipates WHERE kidparticipates.courseId=current_id) as cntparticipants FROM courses " +
                "LEFT JOIN organisation ON courses.organisationID = organisation.organisationID");
            DataView dvCompany = new DataView(dtCompany);
            dvCompany.Sort = SortExpresssion;

            gvCourses.DataSource = dvCompany;
            gvCourses.DataBind();
        }

        protected void gvCourses_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvCourses_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void btnNewCourse_Click(object sender, EventArgs e)
        {
            txtCourseName.Text = string.Empty;
            txtDesciption.InnerText = string.Empty;
            txtFrom.Text = string.Empty;
            txtTo.Text = string.Empty;
            txtZIP.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtStreet.Text = string.Empty;
            txtNr.Text = string.Empty;
            litPanHeadline.Text = "Neuer Kurs";
            calendar.SelectedDate = DateTime.Now;
            Fill_ddlOrganisation();

            btnAdd.Visible = true;
            btnSave.Visible = false;

            panBlockBackground.Visible = true;
            panCourse.Visible = true;
        }

        protected void gvCourses_RowEditing(object sender, GridViewEditEventArgs e)
        {
            DB db = new DB();
            DataTable dt = db.Query("SELECT * FROM courses WHERE courseId=?", gvCourses.DataKeys[e.NewEditIndex].Value);
            DataRow dr = dt.Rows[0];

            txtCourseName.Text = (string)dr["coursename"];
            txtDesciption.InnerText = (string)dr["description"];
            TimeSpan timeFrom = (TimeSpan)dr["timefrom"];
            TimeSpan timeTo = (TimeSpan)dr["timeto"];
            txtFrom.Text = timeFrom.ToString();
            txtTo.Text = timeTo.ToString();
            txtZIP.Text = (string)dr["zipcode"];
            txtCity.Text = (string)dr["city"];
            txtStreet.Text = (string)dr["streetname"];
            txtNr.Text = (string)dr["housenumber"];
            DateTime date = Convert.ToDateTime(dr["date"]);
            calendar.SelectedDate = date;
            txtManagerName.Text = (string)dr["managername"];
            txtContactMail.Text = (string)dr["contactemail"];

            Fill_ddlOrganisation();
            ddlOrganisation.SelectedIndex = (int)dr["organisationId"];

            litPanHeadline.Text = "Kurs ändern";

            btnAdd.Visible = false;
            btnSave.Visible = true;

            panBlockBackground.Visible = true;
            panCourse.Visible = true;
        }

       

        private void Fill_ddlOrganisation()
        {
            DB db = new DB();
            DataTable dtOrganisations = db.Query("SELECT * FROM organisation");
            dtOrganisations.Rows.Add(0, "Nicht ausgewählt");
            dtOrganisations.DefaultView.Sort = "organisationId ASC";
            ddlOrganisation.DataSource = dtOrganisations;
            ddlOrganisation.DataTextField = "organisationname";
            ddlOrganisation.DataBind();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            panCourse.Visible = false;
            panBlockBackground.Visible = false;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (TXTsFilled())
            {
                if (Convert.ToDateTime(txtFrom).TimeOfDay < Convert.ToDateTime(txtTo).TimeOfDay)
                {
                    if (calendar.SelectedDate > DateTime.Now)
                    {
                        DB db = new DB();
                        db.ExecuteNonQuery("INSERT INTO courses (coursename, description, zipcode, city, streetname, housenumber, date, timefrom, timeto, " +
                            "managername, organisationId, contactemail, minparticipants, maxparticipants) " +
                            "VALUES (?,?,?,?,?,?,?,?,?)", txtCourseName.Text, txtDesciption.InnerText, txtZIP.Text, txtCity.Text, txtStreet.Text, txtNr.Text, 
                            calendar.SelectedDate, txtFrom.Text, txtTo);
                    }
                }
            }
            else
            {

            }
        }

        private bool TXTsFilled()
        {
            throw new NotImplementedException();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }

        protected void btnMail_Click(object sender, EventArgs e)
        {
           
              
            
        }

        protected void gvCourses_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //Kurs löschen

            DB db = new DB();          

            db.Query("DELETE FROM courses WHERE courseId=?", e.Keys[0]);
            gvCourses.EditIndex = -1;

            Fill_gvcourses();     

            //Panel für die Bestätigung bzw. Abbruch erstellen
            


            //Falls bestätigt, dann E-Mail versenden

            
            string MailText =
              $"Sehr geehrte Damen und Herren, <br><br>Der Kurst wurde leider abgesagt. Um den Grund der Absage zu erfahren, " +
              $"melden Sie sich bitte bei dem Kursmanager. Die Nummer bzw. Email-Adresse können Sie auf unserer Webseite finden." +
              $"Bitte entschuldigen Sie die Unnanehmlichkeiten." + $"" +
              $"<br><br> Mit freundlichen Grüßen,<br>Gemeinde Mondpichl";

            db.Query("SELECT * FROM kidparticipates LEFT JOIN kids ON kidparticipates.kidId = kids.Id WHERE courseId=? " +
                "LEFT JOIN users ON users.id=kids.parentid");

            try
            {

                EmailMaker.Send(user, "Kursabsage", MailText);

                litEmailStatus.Text = "<div class='row'><div class='col'><div class='alert alert-success'>E-Mail erfolgreich gesendet!</div></div></div>";
            }
            catch
            {
                litEmailStatus.Text = "<div class='row'><div class='col'><div class='alert alert-danger'>E-Mail senden fehlgeschlagen!</div></div></div>";
            }

            
        }
    }
}