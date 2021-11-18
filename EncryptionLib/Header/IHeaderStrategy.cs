using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionLib.Header
{
    internal interface IHeaderStrategy
    {
        public byte[][] GenerateHeader(params byte[][] vs);
        public HeaderProfile ReadHeader(Stream stream);
    }
}
