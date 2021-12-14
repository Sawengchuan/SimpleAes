using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionLib.PasswordHashing
{
    internal class Rfc2898Hashing : IPasswordHashingStrategy
    {
        int PasswordByteSize = 256 / 8;
        int iteration;
        HashAlgorithmName algorithmName;
        public Rfc2898Hashing(int PasswordIterationCount, HashAlgorithmName hashAlgorithm, int PasswordByteSize)
        {
            if(PasswordIterationCount < 0)
                throw new ArgumentOutOfRangeException(nameof(PasswordIterationCount));

            if (PasswordByteSize < 0)
                throw new ArgumentOutOfRangeException(nameof(PasswordByteSize));

            this.iteration = PasswordIterationCount;
            this.algorithmName = hashAlgorithm;
            this.PasswordByteSize = PasswordByteSize;
        }

        public byte[] Get(string password, byte[] salt)
        {
            if(string.IsNullOrEmpty(password?.Trim()))
                throw new ArgumentNullException(nameof(password));

            if(salt == null)
                throw new ArgumentNullException(nameof(salt));

            var keyBytes = Encoding.UTF8.GetBytes(password);

            using (var derivator = new Rfc2898DeriveBytes(
                keyBytes, salt,
                iteration, algorithmName))
            {
                return derivator.GetBytes(PasswordByteSize);
            }
        }
    }
}
