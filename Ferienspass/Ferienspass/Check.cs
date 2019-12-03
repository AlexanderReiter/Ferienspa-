using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Programmer: Josip
/// Date: 03.12.2019
/// Verified by: Alexander Reiter
/// </summary>

namespace Ferienspass
{
    public class Check
    {
        public static bool IsAdmin(string email)
        {
            DB db = new DB();
            string sql = "SELECT userstatus FROM user WHERE email=?";
            DataTable ret = db.Query(sql, email);


            // When userstatus == 1 then user is an admin --> return true
            if(ret.Rows.Count > 0)
            {
                if(ret.Columns.IndexOf("userstatus") == 0)
                {
                    return true; //User is a admin
                }

                else
                {
                    return false; //User is a ordinary user
                }            
            }

            else
            {
                return false;
            }
        }



        public static bool IsUser(string email)
        {
            DB db = new DB();
            string sql = "SELECT userstatus FROM user WHERE email=?";
            DataTable ret = db.Query(sql, email);

            //When userstatus == 0 then user is an ordinary user --> return true
            if(ret.Rows.Count > 0)
            {
                if(ret.Columns.IndexOf("userstatus") == 1)
                {
                    return true; //User is a ordinary user
                } 

                else
                {
                    return false; //User is a admin
                }
            }

            else
            {
                return false;
            }

        }
    }
}