using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ferienspass
{
    public partial class user_master : System.Web.UI.MasterPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void SetBasketNumber(int number)
        {
            if (number != 0)
            {
                litBasketNumber.Text = number.ToString();
            }
        }
    }
}