using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using EncryptionLib.Encryptor;

namespace EncryptionLib.Header
{
    internal class AesRegularHeader : IAesHeaderStrategy
    {
        //
        // pattern: HeaderID + SignatureByteSize + authKeySalt + keySalt + iv + cipherText
        // read from encrypted file: SignatureByteSize + authKeySalt + keySalt + iv + iteration number
        // compute the signature of the cipherText
        //
        private int AesBlockByteSize = EncryptionLib.Encryptor.AesEncryptor.AesBlockByteSize;
        private int PasswordSaltByteSize = EncryptionLib.Encryptor.AesEncryptor.PasswordSaltByteSize;
        private int IterationNumberByteSize = EncryptionLib.Encryptor.AesEncryptor.IterationNumberByteSize;

        internal const byte ID = 200;
        internal const int HeaderIDByteSize = 1; // size in byte

        HMACSHA512 hmac = new HMACSHA512();
        byte[] GenerateHeader(params byte[][] vs)
        {
            if (vs.Length < 5)
                return null;

            return MergeArrays(vs);
        }

        public byte[] GenerateHeader(AesHeaderProfile profile)
        {
            if (profile == null)
                throw new ArgumentNullException(nameof(profile));

            if(profile.IV == null)
                throw new ArgumentNullException(nameof(profile.IV));
            if (profile.StoredSig == null)
                throw new ArgumentNullException(nameof(profile.StoredSig));
            if (profile.KeySalt == null)
                throw new ArgumentNullException(nameof(profile.KeySalt));
            if (profile.AuthKeySalt == null)
                throw new ArgumentNullException(nameof(profile.AuthKeySalt));
            if (profile.Iteration == null)
                throw new ArgumentNullException(nameof(profile.Iteration));

            if (profile.IV.Length != AesBlockByteSize)
                throw new Exception("IV: Invalid length");
            if (profile.StoredSig.Length != hmac.HashSize / 8)
                throw new Exception("Signature: Invalid length");
            if (profile.KeySalt.Length != PasswordSaltByteSize)
                throw new Exception("Key Salt: Invalid length");
            if (profile.AuthKeySalt.Length != PasswordSaltByteSize)
                throw new Exception("Auth Key Salt: Invalid length");
            if (profile.Iteration.Length != IterationNumberByteSize)
                throw new Exception("Iteration: Invalid length");


            return GenerateHeader(new byte[] { ID }, profile.StoredSig, profile.AuthKeySalt, profile.KeySalt, profile.IV, profile.Iteration);
        }

        public async Task<AesHeaderProfile> ReadHeader(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            if (!stream.CanSeek || !stream.CanRead)
                throw new Exception("Read header: Invalid stream - cannot read/seek");

            int MinimumEncryptedMessageByteSize =
                        HeaderIDByteSize +    // ID size
                        hmac.HashSize / 8 + // signature tag
                        PasswordSaltByteSize + // auth salt
                        PasswordSaltByteSize + // key salt
                        AesBlockByteSize + // IV
                        IterationNumberByteSize; // Iteration Number


            if (stream.Length < MinimumEncryptedMessageByteSize)
            {
                throw new ArgumentException("Invalid length of header data");
            }


            var profile = new AesHeaderProfile();

            byte[] storedSig = new byte[hmac.HashSize / 8];
            byte[] authKeySalt = new byte[PasswordSaltByteSize];
            byte[] keySalt = new byte[PasswordSaltByteSize];
            byte[] iv = new byte[AesBlockByteSize];
            byte[] iterationNumber = new byte[IterationNumberByteSize];


            stream.Position = HeaderIDByteSize;
            await stream.ReadAsync(storedSig, 0, storedSig.Length);
            await stream.ReadAsync(authKeySalt, 0, authKeySalt.Length);
            await stream.ReadAsync(keySalt, 0, keySalt.Length);
            await stream.ReadAsync(iv, 0, iv.Length);
            await stream.ReadAsync(iterationNumber, 0, iterationNumber.Length);

            profile.AuthKeySalt = authKeySalt;
            profile.IV = iv;
            profile.KeySalt = keySalt;
            profile.Iteration = iterationNumber;
            profile.StoredSig = storedSig;

            profile.Length = stream.Position;

            return profile;
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
