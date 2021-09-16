using System;
using System.Security.Cryptography;
using System.Text;

namespace ThirdTask
{
    public class Hmac
    {
        public Hmac()
        {
            SecureKey = SecureKeyGenerator.GetNewKey();
        }

        private byte[] SecureKey { get; }

        // Get HMAC secure key in HEX
        public string GetHexSecureKey()
        {
            return BitConverter.ToString(SecureKey).Replace("-", string.Empty);
        }

        // Encrypt received string using secure key
        public string Encrypt(string toEncrypt)
        {
            var hash = new HMACSHA256(SecureKey);
            var bytesToEncrypt = Encoding.ASCII.GetBytes(toEncrypt);
            return BitConverter.ToString(hash.ComputeHash(bytesToEncrypt)).Replace("-", string.Empty);
        }
    }
}