using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Ferienspass
{
    public class Password
    {
        public static string EncryptPassword(string planePassword, string salt)
        {
            string password = planePassword + salt + ConfigurationManager.AppSettings.GetValues("Pepper");

            SHA512 sha512 = SHA512Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] hash = sha512.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }

        public static string GenerateSalt()
        {
            var random = new RNGCryptoServiceProvider();
            byte[] salt = new byte[100];

            random.GetNonZeroBytes(salt);

            return Convert.ToBase64String(salt).Substring(0, 20);
        }
    }
}