using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ferienspass
{
    public class GlobalMethods
    {
        public static int BasketCount(string userId)
        {
            DB db = new DB();
            int cntItemsInBasket = Convert.ToInt32(db.ExecuteScalar("SELECT COUNT(*) FROM basket WHERE userId=?", userId));

            return cntItemsInBasket;
        }
    }
}