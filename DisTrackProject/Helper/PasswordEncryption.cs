using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DisTrack.Helper
{
    public class PasswordEncryption
    {
        public static string SHA512ComputeHash(string password)
        {
            // Create a salt
            var saltString = password.Replace(password.ElementAt(0), password.ElementAt(1));

            // Build the hashed value
            var hash = new StringBuilder();
            byte[] secretkeyBytes = Encoding.UTF8.GetBytes(saltString);
            byte[] inputBytes = Encoding.UTF8.GetBytes(password);
            using (var hmac = new HMACSHA512(secretkeyBytes))
            {
                byte[] hashValue = hmac.ComputeHash(inputBytes);

                foreach (var theByte in hashValue)
                {
                    hash.Append(theByte.ToString("x2"));
                }
            }

            return hash.ToString();
        }
    }
}
