using System;
using System.Security.Cryptography;
using System.Text;

namespace TP3_NET.Models.Service
{
    public static class EncryptionService
    {
        public static string EncryptSha256(string source)
        {
            var sha256 = SHA256.Create();
            var objUtf8 = new UTF8Encoding();
            return Convert.ToBase64String(sha256.ComputeHash(objUtf8.GetBytes(source)));
        }

        public static string EncryptMd5(string source)
        {
            var md5 = MD5.Create();
            var objUtf8 = new UTF8Encoding();
            return Convert.ToBase64String(md5.ComputeHash(objUtf8.GetBytes(source)));
        }
    }
}