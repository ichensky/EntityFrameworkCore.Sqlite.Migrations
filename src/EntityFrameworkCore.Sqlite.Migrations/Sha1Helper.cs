using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace EntityFrameworkCore.Sqlite.Migrations
{
    public static class Sha1Helper
    {
        public static string Sha1(this string inputString)
        {
            var algorithm = SHA1.Create();  
            var bytes= algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
            var sb = new StringBuilder();
            foreach (var b in bytes)
            {
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
