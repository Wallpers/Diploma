using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.BLL.Services
{
    public enum HashMode
    {
        MD5,
        SHA256
    }

    public static class SecurityService
    {
        public static HashMode HashMode { get; set; }

        public static string GetHash(string input)
        {
            switch (HashMode)
            {
                case HashMode.MD5:
                    return GetHashMD5(input);
                case HashMode.SHA256:
                    return GetHashSHA256(input);               
            }

            throw new InvalidOperationException("Somethings in security service.");
        }

        private static string GetHashMD5(string input)
        {
            var hasher = MD5.Create();
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = hasher.ComputeHash(bytes);


            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                // To HEX.
                builder.Append(hash[i].ToString("X2"));
            }

            return builder.ToString();
        }

        private static string GetHashSHA256(string input)
        {
            if (input == null)
                return null;

            var hasher = new SHA256Managed();
            var builder = new StringBuilder();
            var bytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(input), 0, Encoding.UTF8.GetByteCount(input));
            foreach (var @byte in bytes)
            {
                builder.Append(@byte.ToString("X2"));
            }
            return builder.ToString();
        }

        public static string GetPasswordHash(string password)
        {
            return GetHashSHA256(password);
        }
    }
}
