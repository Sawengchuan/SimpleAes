using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionLib
{
    internal static class StreamHashingExtension
    {
        public static async Task<byte[]> HMACSHA512(this Stream inputStream, byte[] key, long StartPosition = 0)
        {
            byte[] vs;

            using (HMACSHA512 hmac = new HMACSHA512(key))
            {
                inputStream.Position = StartPosition;
                vs = await hmac.ComputeHashAsync(inputStream);
                inputStream.Position = 0;
            }

            return vs;
        }
    }
}
