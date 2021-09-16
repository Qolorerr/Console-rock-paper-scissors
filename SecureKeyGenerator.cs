using System.Security.Cryptography;

namespace ThirdTask
{
    public class SecureKeyGenerator
    {
        // Generating random byte sequence of specified length
        public static byte[] GetRandomByteSequence(int countOfBytes)
        {
            var bytes = new byte[countOfBytes];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);
            }

            return bytes;
        }

        // Generating secure key
        public static byte[] GetNewKey()
        {
            return GetRandomByteSequence(16);
        }
    }
}