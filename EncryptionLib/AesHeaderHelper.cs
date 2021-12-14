using EncryptionLib.Header;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionLib
{
    internal class AesHeaderHelper
    {
        //readonly static IHeaderStrategy regular; // 200
        //readonly static IHeaderStrategy custom;  // 201

        IAesHeaderStrategy strategy;
        readonly static AesHeaderHelper defaultHelper;

        AesHeaderHelper()
        {
        }

        static AesHeaderHelper()
        {
            defaultHelper = new AesHeaderHelper() { strategy = new AesRegularHeader() };
        }

        // to do: ssss
        public static byte[] GenerateHeader(AesHeaderProfile profile)
        {
            return defaultHelper.strategy.GenerateHeader(profile);
        }
        public static async Task<AesHeaderProfile> ReadHeader(Stream stream)
        {
            byte[] info = new byte[1];


            stream.Position = 0;
            await stream.ReadAsync(info, 0, info.Length);

            AesHeaderHelper helper;
            switch (info[0])
            {
                case AesRegularHeader.ID:
                    helper = new AesHeaderHelper() { strategy = new AesRegularHeader() };
                    break;

                case AesCustomHeader.ID:
                    helper = new AesHeaderHelper() { strategy = new AesCustomHeader() };
                    break;

                default: 
                    throw new Exception("Invalid file");
            }

            return await helper.strategy.ReadHeader(stream);
        }
    }
}
