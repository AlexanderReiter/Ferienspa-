using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Ferienspass
{
    public class GlobalMethods
    {
        public static int BasketCount(string userId)
        {
            DB db = new DB();

            DeleteExpiredBasket();
            int cntItemsInBasket = Convert.ToInt32(db.ExecuteScalar("SELECT COUNT(*) FROM basket WHERE userId=?", userId));

            return cntItemsInBasket;
        }

        private static void DeleteExpiredBasket()
        {
            DB db = new DB();

            int timeUntilExpired = Convert.ToInt32(db.ExecuteScalar("SELECT value FROM settings WHERE settingId='basketexpirytime'"));
            DateTime maxTime = DateTime.Now.AddDays(-timeUntilExpired);

            db.ExecuteNonQuery("DELETE FROM basket WHERE date < ?", maxTime);
        }

        //admin-setting
        public string GetValueFromDataTable(DataTable dt, string v)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0].ToString() == v) return dt.Rows[i]["value"].ToString();
            }
            return "";
        }
        //admin-setting
        public DataTable GetDataTableFromSettings()
        {
            DB db = new DB();
            DataTable dt = db.Query("SELECT * FROM settings");
            return dt;
        }
    }
}