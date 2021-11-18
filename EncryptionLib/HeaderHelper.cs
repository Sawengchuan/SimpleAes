using EncryptionLib.Header;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionLib
{
    public class HeaderHelper
    {
        static IHeaderStrategy headerStrategy;

        static HeaderHelper()
        {
            headerStrategy = new CustomHeader();
        }

        public static byte[][] GenerateHeader(int MaxSlot, params byte[][] vs)
        {
            return headerStrategy.GenerateHeader(vs);
        }
        public static HeaderProfile ReadHeader(Stream stream)
        {
            return headerStrategy.ReadHeader(stream);
        }
    }
}
