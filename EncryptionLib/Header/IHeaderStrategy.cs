using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionLib.Header
{
    internal interface IHeaderStrategy
    {
        public byte[] GenerateHeader(HeaderProfile profile);
        public Task<HeaderProfile> ReadHeader(Stream stream);
    }
}
