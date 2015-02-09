using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using yops_lib.Constants;

namespace yops_lib.Crypto
{
    public static class SaltHash
    {
        /*
         * Get the hash of the input string.
         */
        public static string getHash(string text)
        {
            // generate salt byte
            RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[CryptoConstants.SALT_BYTE_SIZE];
            csprng.GetBytes(salt);

            // generate the hash bytes
            byte[] hash = PBKDF2(text, salt, CryptoConstants.PBKDF2_ITERATIONS, CryptoConstants.HASH_BYTE_SIZE);
            // encode for easy verification
            return CryptoConstants.PBKDF2_ITERATIONS + ":" + Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
        }

        /*
         * Generate the PBKDF2 for the input text and salt.
         */
        private static byte[] PBKDF2(string text, byte[] salt, int iterations, int outputBytes)
        {
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(text, salt);
            pbkdf2.IterationCount = iterations;
            return pbkdf2.GetBytes(outputBytes);
        }

        /*
         * Hash input text and compare with encoded hash.
         */
        public static bool validateHash(string text, string encodedHash)
        {
            // extract the parts from the encoded hash
            char[] delimiter = { ':' };
            string[] parts = encodedHash.Split(delimiter);
            int iterations = Int32.Parse(parts[CryptoConstants.ITERATION_INDEX]);
            byte[] salt = Convert.FromBase64String(parts[CryptoConstants.SALT_INDEX]);
            byte[] hash = Convert.FromBase64String(parts[CryptoConstants.PBKDF2_INDEX]);

            // generate a hash from the input text and return comparison result
            byte[] generated = PBKDF2(text, salt, iterations, hash.Length);
            return slowEqual(hash, generated);
        }

        /*
         * Compare the two byte arrays in length-constant time.
         * This will prevent password extraction via a timing attack.
         */
        private static bool slowEqual(byte[] a, byte[] b)
        {
            uint diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }
            return diff == 0;
        }
    }
}
