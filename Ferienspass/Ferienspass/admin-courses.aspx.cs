using Ferienspass.Classes;
using System;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ferienspass
{
    public partial class admin_courses : System.Web.UI.Page
    {
        public int CourseID
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

        private int KidID
        {
            get
            {
                return Convert.ToInt32(ViewState["DataKey"]);
            }
            set
            {
                ViewState["DataKey"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Fill_gvcourses();
                //txtSearchbar.Attributes.Add("onkeypress", "return handleKeyDown('" + txtSearchbar.ClientID + "', event)");
            }
        }

        protected void btnSearchCourse_Click(object sender, EventArgs e)
        {
            Fill_gvcourses();
        }

        private void Fill_gvcourses()
        {
            DB db = new DB();
            string queryString = "SELECT *,courseID as current_id, " +
                "(SELECT COUNT(*) FROM kidparticipates WHERE kidparticipates.courseId=current_id) as cntparticipants FROM courses " +
                "LEFT JOIN organisation ON courses.organisationID = organisation.organisationID";

            if (!string.IsNullOrEmpty(txtSearchbar.Text))   //Suchabfrage
            {
                queryString += $" WHERE courses.coursename LIKE '{txtSearchbar.Text}%' OR organisation.organisationname LIKE '{txtSearchbar.Text}%'";
            }

            DataTable dtCompany = db.Query(queryString);
            DataView dvCompany = new DataView(dtCompany);
            dvCompany.Sort = SortExpression;

            gvCourses.DataSource = dvCompany;
            gvCourses.DataBind();
            gvCourses.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void gvCourses_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortExpression = e.SortExpression;
            Fill_gvcourses();
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

        protected void gvCourses_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCourses.PageIndex = e.NewPageIndex;
            Fill_gvcourses();
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
            txtPrice.Text = "0.00€";

            btnAdd.Visible = true;
            btnSave.Visible = false;

            panBlockBackground.Visible = true;
            panCourse.Visible = true;
        }

        protected void gvCourses_RowEditing(object sender, GridViewEditEventArgs e)
        {
            CourseID = Convert.ToInt32(gvCourses.DataKeys[e.NewEditIndex].Value);
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
            txtPrice.Text = "€ " + Convert.ToString((decimal)dr["price"]);

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
                        double price =
                        db.ExecuteNonQuery("UPDATE courses SET coursename=?, description=?, zipcode=?, city=?, streetname=?, housenumber=?, date=?, timefrom=?, " +
                            "timeto=?, managername=?, organisationId=?, contactemail=?, minparticipants=?, maxparticipants=?, price=? WHERE courseId=?",
                            txtCourseName.Text, txtDesciption.InnerText, txtZIP.Text, txtCity.Text, txtStreet.Text,
                            txtNr.Text, calendar.SelectedDate, txtFrom.Text, txtTo.Text, txtManagerName.Text, ddlOrganisation.SelectedIndex, txtContactMail.Text,
                            Convert.ToInt32(txtMinParticipants.Text), Convert.ToInt32(txtMaxParticipants.Text), Convert.ToDecimal(txtPrice.Text.Replace("€", "").Trim(' ')), CourseID);

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


            //Falls bestätigt, dann E-Mail versenden


            string CourseDeleteMailTExt =
              $"Sehr geehrte Damen und Herren, <br><br>der Kurs wurde leider abgesagt. Um den Grund der Absage zu erfahren, " +
              $"melden Sie sich bitte bei dem Kursmanager. Die Nummer bzw. Email-Adresse können Sie auf unserer Webseite finden." +
              $"Bitte entschuldigen Sie die Unnanehmlichkeiten." + $"" +
              $"<br><br> Mit freundlichen Grüßen,<br>Gemeinde Mondpichl";

            DataTable dtEmails = db.Query("SELECT DISTINCT email FROM kidparticipates LEFT JOIN kids ON kidparticipates.kidId = kids.kidId WHERE courseId=?", e.Keys[0]);

            foreach (DataRow dr in dtEmails.Rows)
            {
                EmailMaker.Send((string)dr["email"], "Kursabsage", CourseDeleteMailTExt);
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            string MailText = txtContent.Value;

            DataTable dtEmails = db.Query("SELECT DISTINCT email FROM kidparticipates LEFT JOIN kids ON kidparticipates.kidId = kids.kidId WHERE courseId=?", CourseID);

            foreach (DataRow dr in dtEmails.Rows)
            {
                EmailMaker.Send((string)dr["email"], txtSubject.Text, MailText);
            }

            litEmail.Text = "<div class='alert alert-info'>Email wurde an Benutzer gesendet. <button type='button' name='btnShowUserWhoGotMail' runat='server' onclick='ShowUserWhoGotMail()'>Benutzer anzeigen</button></div>";
            Fill_gvUserWhoGotMail();

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

            switch (command)
            {
                case "Mail":
                    panBlockBackground.Visible = true;
                    panSendMail.Visible = true;
                    CourseID = Convert.ToInt32(e.CommandArgument.ToString());
                    break;
                case "Participants":
                    CourseID = Convert.ToInt32(e.CommandArgument.ToString());
                    panBlockBackground.Visible = true;
                    if(GetParticipantsNumber() == 0)
                    {
                        panNoParticipants.Visible = true;
                    }
                    else
                    {
                        panParticipants.Visible = true;
                    }
                    Fill_gvParticipants();
                    break;
            }
        }

        protected void txtPrice_TextChanged(object sender, EventArgs e)
        {
            //Remove previous formatting, or the decimal check will fail including leading zeros
            string value = txtPrice.Text.Replace(",", "")
                .Replace("€", "").Replace(".", "").TrimStart('0');
            decimal ul;
            //Check we are indeed handling a number
            if (decimal.TryParse(value, out ul))
            {
                ul /= 100;
                //Unsub the event so we don't enter a loop
                txtPrice.TextChanged -= txtPrice_TextChanged;
                //Format the text as currency
                txtPrice.Text = string.Format(CultureInfo.CreateSpecificCulture("de-AT"), "{0:C2}", ul);
                txtPrice.TextChanged += txtPrice_TextChanged;
            }
            bool goodToGo = TextisValid(txtPrice.Text);
            if (!goodToGo)
            {
                txtPrice.Text = "0.00€";
            }
        }

        private bool TextisValid(string text)
        {
            Regex money = new Regex(@"\€\ ([0-9]+[\,]*[0-9]*)");
            return money.IsMatch(text);
        }


        private void Fill_gvParticipants()
        {
            DB db = new DB();
            gvParticipants.DataSource = db.Query("SELECT *, gender.name AS gendername FROM kids LEFT JOIN kidparticipates ON kids.kidId = kidparticipates.kidId LEFT JOIN gender ON gender.id = kids.gender WHERE courseId=?", CourseID);
            gvParticipants.DataBind();
            gvParticipants.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        private void GetUserOfParticipant()
        {
            DB db = new DB();
            DataRow dr = db.Query("SELECT * FROM user LEFT JOIN kids ON kids.email = user.email WHERE kids.kidId=?", KidID).Rows[0];

            txtEmail.Text = Convert.ToString(dr["email"]);
            txtGivenname.Text = Convert.ToString(dr["givenname"]);
            txtSurname.Text = Convert.ToString(dr["surname"]);
            txtUserZIP.Text = Convert.ToString(dr["zipcode"]);
            txtUserCity.Text = Convert.ToString(dr["city"]);
            txtUserStreet.Text = Convert.ToString(dr["streetname"]);
            txtUserHousenumber.Text = Convert.ToString(dr["housenumber"]);
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            panParticipants.Visible = false;
            panBlockBackground.Visible = false;
        }

        protected void btnNoParticipantsClose_Click(object sender, EventArgs e)
        {
            panNoParticipants.Visible = false;
            panBlockBackground.Visible = false;
        }

        private int GetParticipantsNumber()
        {
            DB db = new DB();
            int cntParticipants = Convert.ToInt32(db.ExecuteScalar("SELECT COUNT(*) FROM kids LEFT JOIN kidparticipates ON kids.kidId = kidparticipates.kidId WHERE courseId=?", CourseID));
            return cntParticipants;
        }
        
        private string GetEmail()
        {
            DB db = new DB();
            string email = Convert.ToString(db.Query("SELECT email FROM kids LEFT JOIN kidparticipates ON kids.kidId = kidparticipates.kidId WHERE courseId=?", CourseID));
            return email;
        }

        protected void btnBackToParticipants_Click(object sender, EventArgs e)
        {
            panUser.Visible = false;
            panParticipants.Visible = true;
        }

        protected void gvParticipants_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string command = e.CommandName;
            switch(command)
            {
                case "User":
                    KidID = Convert.ToInt32(e.CommandArgument.ToString());
                    panUser.Visible = true;
                    panParticipants.Visible = false;
                    GetUserOfParticipant();
                    break;
            }
        }

        protected void gvParticipants_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DB db = new DB();
            KidID = Convert.ToInt32(e.Keys[0]);

            DataTable dtParticipate = db.Query("SELECT surname, givenname FROM kids WHERE kidId=?", KidID);
            DataRow drParticipate = dtParticipate.Rows[0];

            DataTable dtCourse = db.Query("SELECT coursename FROM courses WHERE courseId=?", CourseID);
            DataRow drCourse = dtCourse.Rows[0];

            db.Query("DELETE FROM kidparticipates WHERE kidId=? AND courseID=?", KidID, CourseID);

            Fill_gvParticipants();
            Fill_gvcourses();

            string ParticipateDeleteMailText =
              $"Sehr geehrte Damen und Herren, <br><br>Ihr Kind {drParticipate["givenname"]} {drParticipate["surname"]} wurde leider aus dem Kurs {drCourse["coursename"]} entfernt. Um den Grund des Ausschlusses zu erfahren, " +
              $"melden Sie sich bitte bei dem Kursmanager. Die Nummer bzw. Email-Adresse können Sie auf unserer Webseite finden." +
              $" Bitte entschuldigen Sie die Unannehmlichkeiten." + $"" +
              $"<br><br> Mit freundlichen Grüßen,<br>Gemeinde Mondpichl";

            DataTable dtEmails = db.Query("SELECT user.email FROM user LEFT JOIN kids ON kids.email = user.email WHERE kids.kidId=?", KidID);

            foreach (DataRow dr in dtEmails.Rows)
            {
                EmailMaker.Send((string)dr["email"], "Ausschluss Kind", ParticipateDeleteMailText);
            }
        }

        private void Fill_gvUserWhoGotMail()
        {
            DB db = new DB();
            gvUserWhoGotMail.DataSource = db.Query("SELECT * FROM user LEFT JOIN kids ON kids.email = user.email LEFT JOIN kidparticipates ON kidparticipates.kidId = kids.kidId WHERE courseId=?", CourseID);
            gvUserWhoGotMail.DataBind();
            gvUserWhoGotMail.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
}