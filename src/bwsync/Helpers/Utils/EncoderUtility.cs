using System;
using System.Text;

namespace bwsync.Helpers
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
    }
}