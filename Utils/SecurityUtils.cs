using System;
using System.Security.Cryptography;
using System.Text;

namespace FoodOrderSystem.Utils
{
    public static class SecurityUtils
    {
        public static string HashMd5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2")); // Convert byte to hex string
                }
                return sb.ToString();
            }
        }

        public static string GenerateRandomPassword(int length)
        {
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@";
            StringBuilder password = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                password.Append(characters[random.Next(characters.Length)]);
            }

            return password.ToString();
        }
    }
}
