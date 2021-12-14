using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionLib.Header
{
    internal class AesCustomHeader : IAesHeaderStrategy
    {
        internal const byte ID = 201;
        internal const int HeaderIDByteSize = 1; // size in byte
        struct SlotData
        {
            public byte Size;
            public byte[] Data;
            public byte[] FullData
            {
                get
                {
                    var merged = new byte[Data.Length + 1];
                    merged[0] = Size;

                    Data.CopyTo(merged, 1);

                    return merged;
                }
            }
        }
        byte[] GenerateHeader(params byte[][] secrets)
        {
            int MaxSlot = RandomNumberGenerator.GetInt32(5, 10);

            if (secrets.Length < 1)
                return null;

            if (secrets.Length > byte.MaxValue)
                return null;

            if (MaxSlot < secrets.Length)
                MaxSlot = secrets.Length * 10;

            List<int> luckyNumber = new List<int>();

            for (int i = luckyNumber.Count; i < secrets.Length; i = luckyNumber.Count)
            {
                var number = RandomNumberGenerator.GetInt32(0, MaxSlot - 1);

                if (!luckyNumber.Contains(number))
                    luckyNumber.Add(number);
            }

            luckyNumber.Sort();


            byte[] hashKey = RandomNumberGenerator.GetBytes(37);

            HMACSHA256 hmac = new HMACSHA256(hashKey);

            List<byte[]> header = new List<byte[]>();
            List<SlotData> headerSlot = new List<SlotData>();


            //byte[] totalSecret = new byte[] { (byte)(vs.Length - 1) };

            // pattern : HeaderID + haskKey (37) + totalsecret (32) + lucky number(s) + slot

            header.Add(new byte[] { ID }); // 1 byte
            header.Add(hashKey); // 37 byte
            header.Add(hmac.ComputeHash(new byte[] { (byte)(secrets.Length) })); // 32 byte
            luckyNumber.ForEach(n => header.Add(hmac.ComputeHash(new byte[] { (byte)n }))); // 32byte foreach


            //test mapping
            var dict = GetNumberDict(hashKey);

            var s = BitConverter.ToString(header[4]);


            var n = dict[s];
            // end testing


            for (var i = 0; i < MaxSlot; i++)
            {

                if (luckyNumber.Contains(i))
                {
                    var index = luckyNumber.IndexOf(i);

                    var size = (byte)secrets[index].Length;

                    headerSlot.Add(new SlotData { Size = size, Data = secrets[index] });
                    header.Add(headerSlot[i].FullData);

                }
                else
                {
                    var size = (byte)RandomNumberGenerator.GetInt32(4, 8); // (byte)RandomNumberGenerator.GetInt32(vs.Length, Byte.MaxValue);

                    headerSlot.Add(new SlotData { Size = size, Data = RandomNumberGenerator.GetBytes(size) });
                    header.Add(headerSlot[i].FullData);
                }
            }

            var headerByte = header.ToArray().Aggregate((x, y) =>
            {
                var agg = new byte[x.Length + y.Length];
                x.CopyTo(agg, 0);
                y.CopyTo(agg, x.Length);

                return agg;
            });

            return headerByte;
            //return header.ToArray();
        }

        public async Task<AesHeaderProfile> ReadHeader(Stream stream)
        {
            List<byte[]> list = new List<byte[]>();


            byte[] hashKey = RandomNumberGenerator.GetBytes(37); // get from stream
            HMACSHA256 hmac = new HMACSHA256(hashKey);


            Dictionary<string, byte> dict = new Dictionary<string, byte>();

            for (byte b = byte.MinValue; b < byte.MaxValue; b++)
            {
                dict.Add(BitConverter.ToString(hmac.ComputeHash(new byte[] { b })), b);
            }


            return new AesHeaderProfile();
        }

        static Dictionary<string, byte> GetNumberDict(byte[] hashKey)
        {

            HMACSHA256 hmac = new HMACSHA256(hashKey);


            Dictionary<string, byte> dict = new Dictionary<string, byte>();

            for (byte b = byte.MinValue; b < byte.MaxValue; b++)
            {
                dict.Add(BitConverter.ToString(hmac.ComputeHash(new byte[] { b })), b);
            }

            return dict;

        }

        public byte[] GenerateHeader(AesHeaderProfile profile)
        {
            throw new NotImplementedException();
        }
    }
}
