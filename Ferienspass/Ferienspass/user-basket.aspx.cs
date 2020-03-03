using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ferienspass
{
    // Alle ausgewählten Kurse
    // Rabatt und Endpreis berechnen und anzeigen
    // Bestätigen (hinfällig, da bei paypal inbegriffen)
    // Verified by Andi
    // 28.01.2020


    public partial class user_basket : System.Web.UI.Page
    {
        public string Total
        {
            set
            {
                hiddenFieldTotal.Value = value;
            }
            get
            {
                return hiddenFieldTotal.Value;
            }
        }

        public string UserName
        {
            set
            {
                hiddenFieldUser.Value = value;
            }
            get
            {
                return hiddenFieldUser.Value;
            }
        }

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

        protected void Page_Load(object sender, EventArgs e)
        {
            ((user_master)this.Master).SetBasketNumber(GlobalMethods.BasketCount(User.Identity.Name));
            if (!Page.IsPostBack)
            {
                Fill_GvBasket();
            }
        }

        public void Fill_GvBasket()
        {
            DB db = new DB();
            DataTable dt = db.Query("SELECT *, basket.courseId as current_id, " +
                "(SELECT COUNT(*) FROM kidparticipates WHERE kidparticipates.courseId=current_id) as cntparticipants " +
                "FROM basket " +
                "LEFT JOIN kids ON basket.kidId=kids.kidId " +
                "LEFT JOIN courses ON basket.courseId=courses.courseId " +
                "LEFT JOIN organisation ON courses.organisationId=organisation.organisationId " +
                "WHERE userId=?", User.Identity.Name);
            gvBasket.DataSource = dt;
            gvBasket.DataBind();
            gvBasket.HeaderRow.TableSection = TableRowSection.TableHeader;

            CalculatePrice(dt);
            CheckSomethingInBasket();
            ((user_master)this.Master).SetBasketNumber(GlobalMethods.BasketCount(User.Identity.Name));
        }

        private void CheckSomethingInBasket()
        {
            DB db = new DB();
            int cntCouses = Convert.ToInt32(db.ExecuteScalar("SELECT COUNT(*) FROM basket WHERE userId=?", User.Identity.Name));

            if (cntCouses > 0)
            {
                //Disable PayPal-Button
            }
            else { /*Disable PayPal-Button*/ }
        }

        private void CalculatePrice(DataTable dt)
        {
            DB db = new DB();

            float subtotal = 0;
            foreach(DataRow r in dt.Rows)
            {
                subtotal += Convert.ToSingle((decimal)r["price"]);
            }
            lblSubtotal.Text = subtotal.ToString("F2");

            float discount = 0;
            DataTable distinctCourses = db.Query("SELECT DISTINCT basket.courseId, price FROM basket LEFT JOIN courses ON basket.courseId=courses.courseId WHERE userId=?", User.Identity.Name);

            DataTable settings = GlobalMethods.GetDataTableFromSettings();
            float percentage = Convert.ToSingle(GlobalMethods.GetValueFromDataTable(settings, "discount").Trim('%')) / 100;

            foreach (DataRow r in distinctCourses.Rows)
            {
                int cntCouses = Convert.ToInt32(db.ExecuteScalar("SELECT COUNT(*) FROM basket WHERE userId=? AND courseId=?", User.Identity.Name, r["courseId"]));
                if (cntCouses >= 2)
                {
                    discount += subtotal * percentage;
                }
            }
            lblDiscount.Text = discount.ToString("F2");

            float total = subtotal - discount;
            lblTotal.Text = total.ToString("F2");
            Total = String.Format("{0:0.00}", total).Replace(',', '.');
            UserName = User.Identity.Name;
        }

        protected void gvBasket_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string command = e.CommandName;
            DB db = new DB();

            switch (command)
            {
                case "Remove":
                    db.ExecuteNonQuery("DELETE FROM basket WHERE kidId=? AND courseId=?", Convert.ToInt32(e.CommandArgument.ToString().Split(',')[1]), Convert.ToInt32(e.CommandArgument.ToString().Split(',')[0]));
                    Fill_GvBasket();
                    break;

                case "ShowDetails":
                    int courseID = Convert.ToInt32(e.CommandArgument.ToString());

                    CourseId = courseID;

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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            panCourse.Visible = false;
            panBlockBackground.Visible = false;
        }

        [WebMethod]
        public static string Checkout(string sendData)// string sendData)
        {
            DB db = new DB();

            db.ExecuteNonQuery("INSERT INTO kidparticipates (kidparticipates.kidId, kidparticipates.courseId) SELECT basket.kidId, basket.courseId FROM basket WHERE basket.userId=?", sendData);
            //foreach (GridViewRow r in gvBasket.Rows)
            //{
            //    db.ExecuteNonQuery("INSERT INTO kidparticipates (kidId, courseId) VALUES(?, ?)", Convert.ToInt32(gvBasket.DataKeys[r.RowIndex].Values["kidId"]), Convert.ToInt32(gvBasket.DataKeys[r.RowIndex].Values["courseId"]));
            //}

            db.ExecuteNonQuery("DELETE FROM basket WHERE userId=?", sendData);
            return sendData;
        }
    }
}