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
        //const int ENTROPY_LENGTH = 32;
        //private readonly byte[] m_Entropy;
        //private byte[] m_SecretEncrypted;
        public byte[] AuthKey { get; set; }
        //{
        //    get => ProtectedData.Unprotect(m_SecretEncrypted, m_Entropy, DataProtectionScope.CurrentUser);
        //    set
        //    {
        //        m_SecretEncrypted = ProtectedData.Protect(value, m_Entropy, DataProtectionScope.CurrentUser);
        //        AuthKey = value;
        //    }
        //}
        public byte[] Key { get; set; }
        public byte[] IV { get; set; }
        public byte[] Iteration { get; set; }

        public HeaderProfile()
        {
            //m_Entropy = RandomNumberGenerator.GetBytes(ENTROPY_LENGTH);
        }
    }
}
