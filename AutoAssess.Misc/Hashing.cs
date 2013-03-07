using System;
using System.Text;
using System.Security.Cryptography;

namespace AutoAssess.Misc
{
	public static class Hashing
	{
		
		public static string GetMd5Hash(string input, string salt)
        {
			MD5 hasher = new MD5CryptoServiceProvider();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = hasher.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
	}
}

