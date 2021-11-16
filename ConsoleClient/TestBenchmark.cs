using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{

    public class TestBenchmark
    {
        private byte[] _data = Enumerable.Range(0, 10).Select(i => (byte)i).ToArray();
        private MemoryStream _destination = new MemoryStream();

        //[Benchmark]
        //public async Task Encode()
        //{
        //    _destination.Position = 0;
        //    using (var toBase64 = new ToBase64Transform())
        //    using (var stream = new CryptoStream(_destination, toBase64, CryptoStreamMode.Write, leaveOpen: true))
        //    {
        //        await stream.WriteAsync(_data, 0, _data.Length);
        //    }
        //}

        //[Benchmark]
        public string Encode()
        {
            _destination.Position = 0;
            using (var toBase64 = new ToBase64Transform())
            using (var stream = new CryptoStream(_destination, toBase64, CryptoStreamMode.Write, leaveOpen: true))
            {
                stream.Write(_data, 0, _data.Length);
            }

            return Convert.ToBase64String(_destination.ToArray());
        }
    }
}
