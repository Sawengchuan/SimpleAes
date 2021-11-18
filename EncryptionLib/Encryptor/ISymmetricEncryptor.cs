using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionLib.Encryptor
{
    public interface ISymmetricEncryptor
    {
        Task<Result> EncryptFile(string FilePath, string Password);
        Task<Result> DecryptFile(string FilePath, string Password);
    }
}
