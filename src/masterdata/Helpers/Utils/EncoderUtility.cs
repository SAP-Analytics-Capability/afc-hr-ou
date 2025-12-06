using System;
using System.Text;

namespace masterdata.Helpers
{
    public class EncoderUtility
    {
        public static string Base64Encode(string plainText)
        {
            if (!string.IsNullOrEmpty(plainText))
            {
                var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                return Convert.ToBase64String(plainTextBytes);
            }
            else
            {
                return plainText;
            }
        }

        public static string Base64Encode(string username, string password)
        {
            string plainText = string.Format("{0}:{1}", username, password);

            if (!string.IsNullOrEmpty(plainText))
            {
                var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                return Convert.ToBase64String(plainTextBytes);
            }
            else
            {
                return plainText;
            }
        }
    }
}