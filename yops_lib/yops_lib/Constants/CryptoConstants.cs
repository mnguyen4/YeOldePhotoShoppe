using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yops_lib.Constants
{
    public static class CryptoConstants
    {
        // byte size for salt and hash
        public const int SALT_BYTE_SIZE = 24;
        public const int HASH_BYTE_SIZE = 24;
        // number of iterations
        public const int PBKDF2_ITERATIONS = 1000;
        // index of hash parts
        public const int ITERATION_INDEX = 0;
        public const int SALT_INDEX = 1;
        public const int PBKDF2_INDEX = 2;
    }
}
