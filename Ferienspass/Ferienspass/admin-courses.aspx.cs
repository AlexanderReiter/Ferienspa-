using Ferienspass.Classes;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ferienspass
{
    public partial class admin_courses : System.Web.UI.Page
    {
        public int CustomerID
        {
            set
            {
                ViewState["editingcustomerid"] = value;
            }
            get 
            { 
                return Convert.ToInt32(ViewState["editingcustomerid"]); 
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
            Fill_ddlOrganisation();
            txtCourseName.Text = string.Empty;
            txtDesciption.InnerText = string.Empty;
            txtFrom.Text = string.Empty;
            txtTo.Text = string.Empty;
            txtMinParticipants.Text = string.Empty;
            txtMaxParticipants.Text = string.Empty;
            txtZIP.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtStreet.Text = string.Empty;
            txtNr.Text = string.Empty;
            txtManagerName.Text = string.Empty;
            txtContactMail.Text = string.Empty;
            ddlOrganisation.SelectedIndex = 0;
            litPanHeadline.Text = "Neuer Kurs";
            calendar.SelectedDate = DateTime.Now;

            btnAdd.Visible = true;
            btnSave.Visible = false;

            panBlockBackground.Visible = true;
            panCourse.Visible = true;
        }

        protected void gvCourses_RowEditing(object sender, GridViewEditEventArgs e)
        {
            CustomerID = Convert.ToInt32(gvCourses.DataKeys[e.NewEditIndex].Value);
            DB db = new DB();
            DataTable dt = db.Query("SELECT * FROM courses WHERE courseId=?", gvCourses.DataKeys[e.NewEditIndex].Value);
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
            ddlOrganisation.DataValueField = "organisationId";
            ddlOrganisation.DataTextField = "organisationname";
            ddlOrganisation.DataBind();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClosePanel();
        }

        protected void ClosePanel()
        {
            litAlert.Text = string.Empty;
            panCourse.Visible = false;
            panBlockBackground.Visible = false;

            Fill_gvcourses();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (AllFilldOrSelected())
            {
                if (Convert.ToDateTime(txtFrom.Text).TimeOfDay < Convert.ToDateTime(txtTo.Text).TimeOfDay)
                {
                    if (calendar.SelectedDate > DateTime.Now)
                    {
                        DB db = new DB();
                        db.ExecuteNonQuery("INSERT INTO courses (coursename, description, zipcode, city, streetname, housenumber, date, timefrom, timeto, " +
                            "managername, organisationId, contactemail, minparticipants, maxparticipants) " +
                            "VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?)", txtCourseName.Text, txtDesciption.InnerText, txtZIP.Text, txtCity.Text, txtStreet.Text, 
                            txtNr.Text, calendar.SelectedDate, txtFrom.Text, txtTo.Text, txtManagerName.Text, ddlOrganisation.SelectedIndex, txtContactMail.Text, 
                            Convert.ToInt32(txtMinParticipants.Text), Convert.ToInt32(txtMaxParticipants.Text));

                        ClosePanel();
                    }
                    else litAlert.Text = "<div class='alert alert-danger'><strong>Fehler!</strong> Ausgewähltes Datum ist nicht zulässig.</div>";
                }
                else litAlert.Text = "<div class='alert alert-danger'><strong>Fehler!</strong> Die Zeit muss richtig eingegeben werden.</div>";
            }
            else
            {
                litAlert.Text = "<div class='alert alert-danger'><strong>Fehler!</strong> Alle Felder müssen mit zulässigen Werten ausgefüllt werden.</div>";
            }
        }

        private bool AllFilldOrSelected()
        {
            if (txtCourseName.Text == string.Empty) return false;
            if (txtDesciption.Value == string.Empty || txtDesciption.Value.Length < 20) return false;
            if (txtFrom.Text == string.Empty) return false;
            if (txtTo.Text == string.Empty) return false;
            if (txtZIP.Text == string.Empty) return false;
            if (txtCity.Text == string.Empty) return false;
            if (txtStreet.Text == string.Empty) return false;
            if (txtNr.Text == string.Empty) return false;
            if (txtManagerName.Text == string.Empty) return false;
            if (txtContactMail.Text == string.Empty) return false;
            if (ddlOrganisation.SelectedIndex == 0) return false;
            if (Convert.ToInt32(txtMinParticipants.Text) < 0) return false;
            if (Convert.ToInt32(txtMaxParticipants.Text) < 0) return false;
            if (Convert.ToInt32(txtMinParticipants.Text) > Convert.ToInt32(txtMaxParticipants.Text)) return false;
            return true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (AllFilldOrSelected())
            {
                if (Convert.ToDateTime(txtFrom.Text).TimeOfDay < Convert.ToDateTime(txtTo.Text).TimeOfDay)
                {
                    if (calendar.SelectedDate > DateTime.Now)
                    {
                        DB db = new DB();
                        db.ExecuteNonQuery("UPDATE courses SET coursename=?, description=?, zipcode=?, city=?, streetname=?, housenumber=?, date=?, timefrom=?, " +
                            "timeto=?, managername=?, organisationId=?, contactemail=?, minparticipants=?, maxparticipants=? WHERE courseId=?", 
                            txtCourseName.Text, txtDesciption.InnerText, txtZIP.Text, txtCity.Text, txtStreet.Text,
                            txtNr.Text, calendar.SelectedDate, txtFrom.Text, txtTo.Text, txtManagerName.Text, ddlOrganisation.SelectedIndex, txtContactMail.Text,
                            Convert.ToInt32(txtMinParticipants.Text), Convert.ToInt32(txtMaxParticipants.Text), CustomerID);

                        ClosePanel();
                    }
                    else litAlert.Text = "<div class='alert alert-danger'><strong>Fehler!</strong> Ausgewähltes Datum ist nicht zulässig.</div>";
                }
                else litAlert.Text = "<div class='alert alert-danger'><strong>Fehler!</strong> Die Zeit muss richtig eingegeben werden.</div>";
            }
            else
            {
                litAlert.Text = "<div class='alert alert-danger'><strong>Fehler!</strong> Alle Felder müssen mit zulässigen Werten ausgefüllt werden.</div>";
            }
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
              $"Sehr geehrte Damen und Herren, <br><br>der Kurs wurde leider abgesagt. Um den Grund der Absage zu erfahren, " +
              $"melden Sie sich bitte bei dem Kursmanager. Die Nummer bzw. Email-Adresse können Sie auf unserer Webseite finden." +
              $"Bitte entschuldigen Sie die Unnanehmlichkeiten." + $"" +
              $"<br><br> Mit freundlichen Grüßen,<br>Gemeinde Mondpichl";

            DataTable dtEmails = db.Query("SELECT DISTINCT email FROM kidparticipates LEFT JOIN kids ON kidparticipates.kidId = kids.kidId WHERE courseId=?", e.Keys[0]);

            foreach (DataRow dr in dtEmails.Rows)
            {
                EmailMaker.Send((string)dr["email"], "Kursabsage", MailText);
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            string MailText = txtContent.Value;

            DataTable dtEmails = db.Query("SELECT DISTINCT email FROM kidparticipates LEFT JOIN kids ON kidparticipates.kidId = kids.kidId WHERE courseId=?", CustomerID);

            foreach (DataRow dr in dtEmails.Rows)
            {
                EmailMaker.Send((string)dr["email"], txtSubject.Text, MailText);
            }

            panSendMail.Visible = false;
            panBlockBackground.Visible = false;
        }

        protected void btnCancelSendMail_Click(object sender, EventArgs e)
        {
            panSendMail.Visible = false;
            panBlockBackground.Visible = false;
            txtSubject.Text = string.Empty;
            txtContent.Value = string.Empty;
        }

        protected void gvCourses_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string command = e.CommandName;

            switch (command) {
                case "Mail":
                    panBlockBackground.Visible = true;
                    panSendMail.Visible = true;
                    CustomerID = Convert.ToInt32(e.CommandArgument.ToString());
                    break;
            }
        }
    }
}