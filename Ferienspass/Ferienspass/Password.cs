using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Ferienspass
{
    public class Password
    {
        public static string EncryptPassword(string planePassword, string salt)
        {
            string password = planePassword + salt;

            throw new NotImplementedException();
        }
    }
}