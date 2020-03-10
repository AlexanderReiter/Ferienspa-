using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ferienspass
{
    public partial class user_home : System.Web.UI.Page
    {
        DataTable GlobalDt;
        protected void Page_Load(object sender, EventArgs e)
        {
            ((user_master)this.Master).SetBasketNumber(GlobalMethods.BasketCount(User.Identity.Name));

            GlobalDt = GetDates();
            Load_NextDates();
            Calendar1.FirstDayOfWeek = FirstDayOfWeek.Monday;
            Calendar1.NextPrevFormat = NextPrevFormat.FullMonth;
            Calendar1.TitleFormat = TitleFormat.MonthYear;
            Calendar1.ShowGridLines = true;
            Calendar1.DayStyle.Height = new Unit(50);
            Calendar1.DayStyle.Width = new Unit(150);
            Calendar1.DayStyle.HorizontalAlign = HorizontalAlign.Center;
            Calendar1.DayStyle.VerticalAlign = VerticalAlign.Top;
            Calendar1.OtherMonthDayStyle.BackColor = System.Drawing.Color.AliceBlue;
        }

        private void Load_NextDates()
        {
            DateTime dateNow = DateTime.Now;
            string expression = string.Format("date > #{0}/{1}/{2}#", dateNow.Year, dateNow.Month, dateNow.Day);
            DataRow[] rows = GlobalDt.Select(expression);

            if (rows.Count() > 0)
            {
                DataTable dt = rows.Take(4).CopyToDataTable();
                gvNextDates.DataSource = dt;
                gvNextDates.DataBind();
            }
        }

        private DataTable GetDates()
        {
            DB db = new DB();

            string sqlGetDates = $"SELECT Kid.kidId, KidParticipation.courseId, Course.coursename, Course.description, Course.date, " +
                $"Kid.givenname, Kid.surname, Course.coursename, Course.zipcode, Course.city, Course.streetname, Course.housenumber " +
                $"FROM(" +
                $"  kids Kid " +
                $"  INNER JOIN kidparticipates KidParticipation " +
                $"  ON Kid.kidId = KidParticipation.kidId" +
                $")" +
                $"INNER JOIN courses Course " +
                $"ON KidParticipation.courseId = Course.courseId " +
                $"WHERE Kid.email = ?";

            DataTable dt = db.Query(sqlGetDates, User.Identity.Name);
            return dt;
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            DateTime date = Calendar1.SelectedDate;
            string expression = string.Format("date = '{0}-{1}-{2}'", date.Year, date.Month, date.Day);
            DataRow[] rows = GlobalDt.Select(expression);
            if (rows.Count() > 0)
            {
                DataTable dt = (rows.AsEnumerable().CopyToDataTable());
                gvSelectedDate.DataSource = dt;
                gvSelectedDate.DataBind();
            }
            else
            {
                gvSelectedDate.DataSource = null;
                gvSelectedDate.DataBind();
            }
        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            DateTime date = e.Day.Date;
            string expression = string.Format("date = '{0}-{1}-{2}'", date.Year, date.Month, date.Day);
            int numberOfDates = GlobalDt.Select(expression).Count();
            if (numberOfDates>0)
            {
                Literal literal1 = new Literal();
                literal1.Text = "<br/>";
                e.Cell.Controls.Add(literal1);
                Label label1 = new Label();

                if (numberOfDates == 1)
                {
                    label1.Text = "•";
                    label1.Font.Size = new FontUnit(FontSize.Large);
                }
                else 
                { 
                    label1.Text = "•••";
                    label1.Font.Size = new FontUnit(FontSize.Small);
                }

                
                e.Cell.Controls.Add(label1);
            }
        }
    }
}