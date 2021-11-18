using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionLib
{
    internal class RegularHeader : IHeaderStrategy
    {
        public byte[][] GenerateHeader(params byte[][] vs)
        {
            throw new NotImplementedException();
        }

        public byte[][] ReadHeader(Stream stream)
        {
            throw new NotImplementedException();
        }
        private byte[] MergeArrays(params byte[][] arrays)
        {
            var merged = new byte[arrays.Sum(a => a.Length)];
            var mergeIndex = 0;
            for (int i = 0; i < arrays.GetLength(0); i++)
            {
                arrays[i].CopyTo(merged, mergeIndex);
                mergeIndex += arrays[i].Length;
            }

            return merged;
        }
    }
}
