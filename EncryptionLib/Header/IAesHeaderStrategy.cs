using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionLib.Header
{
    internal interface IAesHeaderStrategy
    {
        public byte[] GenerateHeader(AesHeaderProfile profile);
        public Task<AesHeaderProfile> ReadHeader(Stream stream);
    }
}
