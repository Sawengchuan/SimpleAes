using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionLib.Header
{
    public class HeaderProfile
    {
        public byte[] AuthKeySalt { get; set; }
        public byte[] KeySalt { get; set; }
        public byte[] IV { get; set; }
        public byte[] Iteration { get; set; }
        public byte[] StoredSig { get; set; }
        public long Length { get; set; }
    }
}
