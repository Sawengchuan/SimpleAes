using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionLib
{
    public class Result
    {
        public CryptoOp CryptoOp { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public string OldFilePath { get; set; }
        public string NewFIlePath { get; set; }
    }
}
