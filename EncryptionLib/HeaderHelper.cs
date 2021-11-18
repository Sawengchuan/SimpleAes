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
        static IHeaderStrategy regular; // 200
        static IHeaderStrategy custom;  // 201

        static IHeaderStrategy use;  

        static HeaderHelper()
        {
            custom = new AesCustomHeader();
            regular = new AesRegularHeader();
            use = regular;
        }

        public static byte[] GenerateHeader(HeaderProfile profile)
        {
            return regular.GenerateHeader(profile);
        }
        public static async Task<HeaderProfile> ReadHeader(Stream stream)
        {
            byte[] info = new byte[1];


            stream.Position = 0;
            await stream.ReadAsync(info, 0, info.Length);

            switch (info[0])
            {
                case AesRegularHeader.ID:
                    use = regular;
                    break;

                case AesCustomHeader.ID:
                    use = custom;
                    break;

                default: 
                    use = regular;
                    break;
            }



            return await use.ReadHeader(stream);
        }
    }
}
