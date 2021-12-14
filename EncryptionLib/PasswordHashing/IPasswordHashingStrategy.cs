using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionLib.PasswordHashing
{
    internal interface IPasswordHashingStrategy
    {
        byte[] Get(string password, byte[] salt);
    }
}
