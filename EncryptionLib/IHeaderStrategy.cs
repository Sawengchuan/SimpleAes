using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionLib
{
    internal interface IHeaderStrategy
    {
        public byte[][] GenerateHeader(params byte[][] vs);
        public byte[][] ReadHeader(Stream stream);
    }
}
