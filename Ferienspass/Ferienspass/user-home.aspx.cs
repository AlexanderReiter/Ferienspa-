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
        //List<KeyValuePair<DateTime, string>> DateList;
        DataTable GlobalDt;
        protected void Page_Load(object sender, EventArgs e)
        {
            ((user_master)this.Master).SetBasketNumber(GlobalMethods.BasketCount(User.Identity.Name));

            GlobalDt = GetDates();
            Calendar1.Caption = "Calender - Author: Mair Andreas";
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

        private DataTable GetDates()
        {
            DB db = new DB();

            string sqlGetDates = $"SELECT Kid.kidId, KidParticipation.courseId, Course.coursename, Course.description, Course.date " +
                $"FROM(" +
                $"  kids Kid " +
                $"  INNER JOIN kidparticipates KidParticipation " +
                $"  ON Kid.kidId = KidParticipation.kidId" +
                $")" +
                $"INNER JOIN courses Course " +
                $"ON KidParticipation.courseId = Course.courseId " +
                $"WHERE Kid.email = 'user'";

            DataTable dt = db.Query(sqlGetDates);
            string expression = "date = '2020-01-18'";
            int anzahlTermineAnTag = dt.Select(expression).Count();

            //List<List<DateTime, string>> holiday = new List<List<DateTime, string>>();
            //List<KeyValuePair<DateTime, string>> dates = new List<KeyValuePair<DateTime, string>>();
            
            //dates.Add( new KeyValuePair<DateTime, string>(new DateTime(2020,1,1), "New Year"));
            //dates.Add( new KeyValuePair<DateTime, string>(new DateTime(2020,1,5), "Guru Govind Singh Jayanti"));
            //dates.Add( new KeyValuePair<DateTime, string>(new DateTime(2020,1,8), "Muharam (Al Hijra)"));
            //dates.Add( new KeyValuePair<DateTime, string>(new DateTime(2020,1,14), "Pongal"));
            //dates.Add( new KeyValuePair<DateTime, string>(new DateTime(2020,1,26), "Republic Day"));
            //dates.Add( new KeyValuePair<DateTime, string>(new DateTime(2020,2,23), "Maha Shivaratri"));
            //dates.Add( new KeyValuePair<DateTime, string>(new DateTime(2020,3,10), "Milad un Nabi (Birthday of the Prophet"));
            //dates.Add( new KeyValuePair<DateTime, string>(new DateTime(2020,3,21), "Holi"));
            //dates.Add( new KeyValuePair<DateTime, string>(new DateTime(2020,3,21), "Telugu New Year"));
            //holiday["05.01.2020"] = "";
            //holiday["08.01.2020"] = "";
            //holiday["14.01.2020"] = "";
            //holiday["26.01.2020"] = "";
            //holiday["23.02.2020"] = "";
            //holiday["10.03.2020"] = "";
            //holiday["21.03.2020"] = "";
            //holiday["21.03.2020"] = "";
            //holiday["03.04.2020"] = "Ram Navmi";
            //holiday["07.04.2020"] = "Mahavir Jayanti";
            //holiday["10.04.2020"] = "Good Friday";
            //holiday["12.04.2020"] = "Easter";
            //holiday["14.04.2020"] = "Tamil New Year and Dr Ambedkar Birth Day";
            //holiday["01.05.2020"] = "May Day";
            //holiday["09.05.2020"] = "Buddha Jayanti and Buddha Purnima";
            //holiday["24.06.2020"] = "Rath yatra";
            //holiday["13.08.2020"] = "Krishna Jayanthi";
            //holiday["14.08.2020"] = "Janmashtami";
            //holiday["15.08.2020"] = "Independence Day";
            //holiday["19.08.2020"] = "Parsi New Year";
            //holiday["23.08.2020"] = "Vinayaka Chaturthi";
            //holiday["02.09.2020"] = "Onam";
            //holiday["05.09.2020"] = "Teachers Day";
            //holiday["21.09.2020"] = "Ramzan";
            //holiday["27.09.2020"] = "Ayutha Pooja";
            //holiday["28.09.2020"] = "Vijaya Dasami (Dusherra)";
            //holiday["02.10.2020"] = "Gandhi Jayanti";
            //holiday["17.10.2020"] = "Diwali & Govardhan Puja";
            //holiday["19.10.2020"] = "Bhaidooj";
            //holiday["02.11.2020"] = "Guru Nanak Jayanti";
            //holiday["14.11.2020"] = "Children's Day";
            //holiday["28.11.2020"] = "Bakrid";
            //holiday["25.12.2020"] = "Christmas";
            //holiday["28.12.2020"] = "Muharram";
            return dt;
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            LabelAction.Text = "Date changed to :" + Calendar1.SelectedDate.ToShortDateString();
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

        protected void Calendar1_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            LabelAction.Text = "Month changed to :" + e.NewDate.ToShortDateString();
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
                    label1.Text = "••";
                    label1.Font.Size = new FontUnit(FontSize.Medium);
                }

                
                e.Cell.Controls.Add(label1);
            }
        }
    }
}