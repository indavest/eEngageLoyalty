using System;
using System.Collections;
using System.Text;
using System.Security.Cryptography;


namespace BP_UniqueCodeGen
{
    /// <summary>
    /// Summary description for UniqueKeys.
    /// </summary>
    public class UniqueKeyGenerator
    {
        /// <summary>
        /// Generates Unique Keys
        /// </summary>
        /// <returns></returns>
        public string[] GenerateUniqueKeys(int numberOfKeys)
        {
            string[] keys = new string[numberOfKeys];

            for (int loop = 0; loop < numberOfKeys; loop++)
            {
                keys[loop] = this.RNGCharacterMask();
            }

            return keys;
        }


        /// <summary>
        /// RNGCharacterMask function returns a unique key
        /// </summary>
        /// <returns></returns>
        private string RNGCharacterMask()
        {
            int maxSize = 8;
            int minSize = 5;

            char[] chars = new char[62];
            string a;

            a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            chars = a.ToCharArray();

            int size = maxSize;
            byte[] data = new byte[1];

            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);

            size = maxSize;
            data = new byte[size];
            crypto.GetNonZeroBytes(data);

            StringBuilder result = new StringBuilder(size);

            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length - 1)]);
            }

            return result.ToString();
        }
    }
}
