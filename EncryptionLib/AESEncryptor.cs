using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionLib
{
    //
    // https://tomrucki.com/posts/aes-encryption-in-csharp/
    // https://stackoverflow.com/questions/15125680/using-cryptostreams-to-encrypt-and-hmac-data
    // https://stackoverflow.com/questions/38623335/aes-encrypt-then-mac-a-large-file-with-net/38629596#38629596
    // https://stackoverflow.com/questions/38623335/aes-encrypt-then-mac-a-large-file-with-net/
    //

    public sealed class AesEncryptor
    {
        private const int AesBlockByteSize = 128 / 8;

        private const int PasswordSaltByteSize = 128 / 8;
        private const int PasswordByteSize = 256 / 8;
        private const int IterationNumberByteSize = 32 / 8;



        private const int DefaultPasswordIterationCount = 100_000;

        private const int SignatureByteSize = 512 / 8;



        private const int MinimumEncryptedMessageByteSize =
            PasswordSaltByteSize + // auth salt
            PasswordSaltByteSize + // key salt
            AesBlockByteSize + // IV
            IterationNumberByteSize + // Iteration Number
            AesBlockByteSize + // cipher text min length
            SignatureByteSize; // signature tag

        private static readonly Encoding StringEncoding = Encoding.UTF8;
        private static readonly RandomNumberGenerator Random = RandomNumberGenerator.Create();

        private byte[] keySalt { get; set; }
        private byte[] key { get; set; }
        private byte[] iv { get; set; }
        private byte[] iterationNumber { get; set; }

        private byte[] authKeySalt { get; set; }
        private byte[] authKey { get; set; }

        private readonly ICryptoTransform toBase64;
        private readonly ICryptoTransform fromBase64;

        private Aes aes;
        private ICryptoTransform encryptor;
        private ICryptoTransform decryptor;

        const string source_ext = ".source";
        const string source_with_sig_ext = ".source_with_sig";
        const string source_with_sig_enc_ext = ".source_with_sig_encrypted";
        const string source_with_sig_enc_ext_base64 = ".source_with_sig_encrypted_64";

        int PasswordIterationCount;


        /*
         * final pattern of the encrypted file

            SignatureByteSize + authKeySalt + keySalt + iv + IterationNumber + cipherText


            var result = MergeArrays(
                additionalCapacity: SignatureByteSize,
                authKeySalt, keySalt, iv, IterationNumber, cipherText);


        */

        public AesEncryptor()
        {
            toBase64 = new ToBase64Transform();
            fromBase64 = new FromBase64Transform();
            aes = CreateAes();

            //PasswordIterationCount = DefaultPasswordIterationCount;
            PasswordIterationCount = RandomNumberGenerator.GetInt32(10_000, 100_000);

        }


        /// <summary>
        /// MAC-Encrypt-MAC
        /// Source file rename to <FileName>.old
        /// Encrypted file name is source file name
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public async Task<Result> EncryptFile(string FilePath, string Password)
        {
            List<string> toBeCleanUpfiles = new List<string>();

            Result result = new Result();

            string fileNameWithoutExt;
            string workingFileNameWithoutExt;
            string OriginalSourceFile = null;
            string RenamedSourceFile = null;

            try
            {
                FileInfo originalFi = new FileInfo(FilePath);

                VerifyFilePath(originalFi);
                VerifyPassword(Password);

                PasswordIterationCount = RandomNumberGenerator.GetInt32(10_000, 100_000);
                SetupAes(Password);



                /*
                 * target final pattern of the encrypted file
                 


                var result = MergeArrays(
                    additionalCapacity: SignatureByteSize,
                    authKeySalt, keySalt, iv, IterationNumber, cipherText);


                */


                ///
                /// clone source file with signature on begin
                /// 


                fileNameWithoutExt = originalFi.Name.Replace(originalFi.Extension, string.Empty);
                workingFileNameWithoutExt = $"{ fileNameWithoutExt }_{ Guid.NewGuid() }";

                // rename source file
                OriginalSourceFile = originalFi.FullName;
                RenamedSourceFile = Path.Combine(originalFi.DirectoryName ?? string.Empty, workingFileNameWithoutExt + source_ext);

                try
                {
                    File.Move(FilePath, RenamedSourceFile);
                }
                catch (IOException io)
                {
                    throw new Exception($"IOException: Unexpected IO error: {io.Message}");
                }
                catch (UnauthorizedAccessException)
                {
                    throw new Exception("UnauthorizedAccessException: No permission to rename file");
                }
                catch (Exception ex)
                {
                    throw new Exception($"Unexpected Error: {ex.Message}");
                }

                var workingFi = new FileInfo(RenamedSourceFile);

                if (!workingFi.Exists)
                {
                    FileInfo oriFi = new FileInfo(OriginalSourceFile);

                    throw new Exception($"Unexpected Internal Error");
                }


                //var encFileName = workingFi.Name + ".encc";
                //var encFileFullPath = workingFi.FullName.Replace(workingFi.Name, encFileName);

                byte[] plainTextSignature;

                var sig_with_source_file_name = $"{ workingFileNameWithoutExt + source_with_sig_ext }";
                var sig_with_source_file_Path = workingFi.FullName.Replace(workingFi.Name, sig_with_source_file_name);

                toBeCleanUpfiles.Add(sig_with_source_file_Path);

                using (var sourceFile = File.OpenRead(workingFi.FullName))
                using (var sourceFileWithSig = File.Create(sig_with_source_file_Path))
                {

                    plainTextSignature = await sourceFile.HMACSHA512(authKey);
                    sourceFileWithSig.Write(plainTextSignature);
                    await sourceFile.CopyToAsync(sourceFileWithSig);
                }


                #region how to read back sig and compare
                // ----------------   how to read back sig and compare
                {
                    bool err;

                    using (HMACSHA512 hmac = new HMACSHA512(authKey))
                    {
                        // Create an array to hold the keyed hash value read from the file.
                        byte[] storedHash = new byte[hmac.HashSize / 8];
                        // Create a FileStream for the source file.
                        using (FileStream inStream = new FileStream(sig_with_source_file_Path, FileMode.Open))
                        {
                            // Read in the storedHash.
                            await inStream.ReadAsync(storedHash, 0, storedHash.Length);
                            // Compute the hash of the remaining contents of the file.
                            // The stream is properly positioned at the beginning of the content, 
                            // immediately after the stored hash value.
                            byte[] computedHash; // = hmac.ComputeHash(inStream);

                            computedHash = await inStream.HMACSHA512(authKey, inStream.Position);
                            // compare the computed hash with the stored value

                            for (int i = 0; i < storedHash.Length; i++)
                            {
                                if (computedHash[i] != storedHash[i])
                                {
                                    err = true;
                                }
                            }
                        }
                    }
                }
                // ----------------------------------------------------------------------------
                #endregion


                ///
                /// encrypt the new source_file_with_sig
                /// 


                var encrypted_file_name = $"{ workingFileNameWithoutExt + source_with_sig_enc_ext }";
                var encrypted_file_Path = workingFi.FullName.Replace(workingFi.Name, encrypted_file_name);

                toBeCleanUpfiles.Add(encrypted_file_Path);

                using (var sourceFileWithSig = File.OpenRead(sig_with_source_file_Path))
                using (var encryptedFile = File.Create(encrypted_file_Path))
                using (var encryptCryptoStream = new CryptoStream(encryptedFile, encryptor, CryptoStreamMode.Write))
                    await sourceFileWithSig.CopyToAsync(encryptCryptoStream);




                // ***************************************************************************************************************************
                //try
                //{
                //    var decrypted_File = "decrypted_" + fi.Name;
                //    var decrypted_FilePath = fi.FullName.Replace(fi.Name, decrypted_File);

                //    using (var originalEncryptedFileStream = File.OpenRead(encrypted_file_Path))
                //    using (var decryptedFileStream = File.Create(decrypted_FilePath))
                //    using (var decryptCryptoStream = new CryptoStream(originalEncryptedFileStream, decryptor, CryptoStreamMode.Read))
                //    {
                //        //inStream.Position = header.Length;
                //        await decryptCryptoStream.CopyToAsync(decryptedFileStream);
                //    }

                //}
                //catch (Exception ex)
                //{

                //}
                //*************************************************************************************************************************



                /*
                var result = MergeArrays(
                    cipherTextSignature,
                    authKeySalt, keySalt, iv, IterationNumber, cipherText);

                */



                /// create final encrypted file with header

                var base64_file_name = $"{ workingFileNameWithoutExt + source_with_sig_enc_ext_base64 }";
                var base64_file_Path = workingFi.FullName.Replace(workingFi.Name, base64_file_name);

                using (var encryptedFile = File.OpenRead(encrypted_file_Path))
                using (var finalEncryptedFileWithHeader = File.Create(base64_file_Path))
                using (var base64CryptoStream = new CryptoStream(finalEncryptedFileWithHeader, toBase64, CryptoStreamMode.Write))
                {
                    byte[] cipherTextSignature = await encryptedFile.HMACSHA512(authKey);

                    byte[] header = MergeArrays(cipherTextSignature, authKeySalt, keySalt, iv, iterationNumber);

                    //finalEncryptedFileWithHeader.Write(header);
                    //await encryptedFile.CopyToAsync(finalEncryptedFileWithHeader);

                    base64CryptoStream.Write(header);
                    await encryptedFile.CopyToAsync(base64CryptoStream);
                }



                #region how to read back  final encrypted file with header and compare
                // ----------------   how to read back  final encrypted file with header and compare

                //{
                //    /*
                //     * pattern
                //        var result = MergeArrays(
                //            cipherTextSignature,
                //            authKeySalt, keySalt, iv, cipherText);
                //    */
                //    bool err;

                //    var frombase64_File = "fromBase64_" + workingFi.Name;
                //    var frombase64_file_Path = workingFi.FullName.Replace(workingFi.Name, frombase64_File);

                //    using (HMACSHA512 hmac = new HMACSHA512(authKey))
                //    {


                //        using (FileStream inputFile = File.OpenRead(base64_file_Path))
                //        using (FileStream outputFile = File.OpenWrite(frombase64_file_Path))
                //        using (CryptoStream cryptoStream = new CryptoStream(inputFile, fromBase64, CryptoStreamMode.Read))
                //        {
                //            cryptoStream.CopyTo(outputFile, 4096);
                //        }



                //        // Create an array to hold the keyed hash value read from the file.
                //        byte[] storedHash = new byte[hmac.HashSize / 8];
                //        byte[] storedAuthKeyHash = new byte[PasswordSaltByteSize];
                //        byte[] storedKeyHash = new byte[PasswordSaltByteSize];
                //        byte[] storedIVHash = new byte[AesBlockByteSize];
                //        byte[] storedIterationNumberHash = new byte[IterationNumberByteSize];


                //        // Create a FileStream for the source file.
                //        //using (FileStream inStream = new FileStream(final_file_Path, FileMode.Open))
                //        using (FileStream inStream = new FileStream(frombase64_file_Path, FileMode.Open))
                //        {
                //            // Read in the storedHash.
                //            await inStream.ReadAsync(storedHash, 0, storedHash.Length);
                //            await inStream.ReadAsync(storedAuthKeyHash, 0, storedAuthKeyHash.Length);
                //            await inStream.ReadAsync(storedKeyHash, 0, storedKeyHash.Length);
                //            await inStream.ReadAsync(storedIVHash, 0, storedIVHash.Length);
                //            await inStream.ReadAsync(storedIterationNumberHash, 0, storedIterationNumberHash.Length);



                //            // Compute the hash of the remaining contents of the file.
                //            // The stream is properly positioned at the beginning of the content, 
                //            // immediately after the stored hash value.
                //            byte[] computedHash; // = hmac.ComputeHash(inStream);

                //            computedHash = await inStream.HMACSHA512(authKey, inStream.Position);
                //            // compare the computed hash with the stored value

                //            for (int i = 0; i < storedHash.Length; i++)
                //            {
                //                if (computedHash[i] != storedHash[i])
                //                {
                //                    err = true;
                //                }
                //            }
                //            for (int i = 0; i < storedAuthKeyHash.Length; i++)
                //            {
                //                if (authKeySalt[i] != storedAuthKeyHash[i])
                //                {
                //                    err = true;
                //                }
                //            }
                //            for (int i = 0; i < storedKeyHash.Length; i++)
                //            {
                //                if (keySalt[i] != storedKeyHash[i])
                //                {
                //                    err = true;
                //                }
                //            }
                //            for (int i = 0; i < storedIVHash.Length; i++)
                //            {
                //                if (iv[i] != storedIVHash[i])
                //                {
                //                    err = true;
                //                }
                //            }


                //        }





                //        // descryption
                //    }

                //    //await DecryptFile(frombase64_file_Path, Password);

                //}
                #endregion

                RenameToOriginalFile(base64_file_Path, OriginalSourceFile);

                result.Success = true;
                result.NewFIlePath = OriginalSourceFile;
                result.OldFilePath = RenamedSourceFile;

            }
            catch
            {
                RenameToOriginalFile(RenamedSourceFile, OriginalSourceFile);

                result.Success = false;
                result.NewFIlePath = OriginalSourceFile;
                result.OldFilePath = OriginalSourceFile;

                throw;
            }
            finally
            {
                CleanUp(toBeCleanUpfiles);
            }



            return result;
        }

        private void SetupAes(string Password, bool fromInternal = true)
        {
            try
            {
                iterationNumber = BitConverter.GetBytes(PasswordIterationCount);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(iterationNumber);

            }
            catch (Exception ex)
            {
                throw new Exception($"Internal setup error for iteration number: {ex.Message}");
            }

            try
            {
                keySalt = GenerateRandomBytes(PasswordSaltByteSize);
                key = GetKey(Password, keySalt);
            }
            catch (Exception ex)
            {
                if (fromInternal)
                    throw new Exception($"Internal setup error during key creation: {ex.Message}");
                else
                    throw new Exception($"Bad data for key creation: {ex.Message}");

            }

            try
            {
                iv = GenerateRandomBytes(AesBlockByteSize);
            }
            catch (Exception ex)
            {
                if (fromInternal)
                    throw new Exception($"Internal setup error in IV initialization: {ex.Message}");
                else
                    throw new Exception($"Bad data for IV initialization: {ex.Message}");
            }

            try
            {
                authKeySalt = GenerateRandomBytes(PasswordSaltByteSize); //GenerateRandomBytes(PasswordSaltByteSize);
                authKey = GetKey(Password, authKeySalt);
            }
            catch (Exception ex)
            {
                if (fromInternal)
                    throw new Exception($"Internal setup error during signature key creation: {ex.Message}");
                else
                    throw new Exception($"Bad data for signature key creation: {ex.Message}");

            }

            //Aes aes = CreateAes();
            try
            {
                encryptor = aes.CreateEncryptor(key, iv);
            }
            catch (Exception ex)
            {
                if (fromInternal)
                    throw new Exception($"Internal setup error during encryptor creation: {ex.Message}");
                else
                    throw new Exception($"Bad data for encryptor creation: {ex.Message}");
            }

            try
            {
                decryptor = aes.CreateDecryptor(key, iv);
            }
            catch (Exception ex)
            {
                if (fromInternal)
                    throw new Exception($"Internal setup error during decryptor creation: {ex.Message}");
                else
                    throw new Exception($"Bad data for decryptor creation: {ex.Message}");

            }
        }

        private static void VerifyFilePath(FileInfo originalFi)
        {
            if (!originalFi.Exists)
                throw new FileNotFoundException();
        }

        private static void VerifyPassword(string Password)
        {
            if (string.IsNullOrEmpty(Password?.Trim()))
                throw new ArgumentNullException(nameof(Password));
        }

        private void RenameToOriginalFile(string renamedSourceFile, string originalSourceFile)
        {
            FileInfo fi = new FileInfo(renamedSourceFile);

            if (fi.Exists)
            {
                File.Move(fi.FullName, originalSourceFile, true);
            }
        }

        private void CleanUp(List<string> toBeCleanUpfiles)
        {
            foreach (var file in toBeCleanUpfiles)
            {
                FileInfo fi = new FileInfo(file);

                try
                {
                    if (fi.Exists)
                        File.Delete(fi.FullName);
                }
                catch (Exception ex)
                {

                }
            }
        }

        public async Task<Result> DecryptFile(string FilePath, string Password)
        {

            Result result = new Result();

            FileInfo originalFi = new FileInfo(FilePath);

            VerifyFilePath(originalFi);
            VerifyPassword(Password);

            if (originalFi.Length < MinimumEncryptedMessageByteSize)
            {
                throw new ArgumentException("Invalid length of encrypted data");
            }

            string fileNameWithoutExt = originalFi.Name.Replace(originalFi.Extension, string.Empty);
            string workingFileNameWithoutExt = $"{ fileNameWithoutExt }_{ Guid.NewGuid() }";


            #region rename file
            // rename source file
            var OriginalSourceFile = originalFi.FullName;
            var RenamedSourceFile = Path.Combine(originalFi.DirectoryName ?? string.Empty, workingFileNameWithoutExt + source_with_sig_enc_ext_base64);


            try
            {
                File.Move(FilePath, RenamedSourceFile);
            }
            catch (IOException io)
            {
                throw new Exception($"IOException: Unexpected IO error: {io.Message}");
            }
            catch (UnauthorizedAccessException)
            {
                throw new Exception("UnauthorizedAccessException: No permission to rename file");
            }
            catch (Exception ex)
            {
                throw new Exception($"Unexpected Error: {ex.Message}");
            }
            #endregion


            List<string> toBeCleanUpfiles = new List<string>();

            var workingFi = new FileInfo(RenamedSourceFile);


            try
            {
                var encrypted_File = $"{ workingFileNameWithoutExt + source_with_sig_enc_ext}";
                var encrypted_file_Path = workingFi.FullName.Replace(workingFi.Name, encrypted_File);


                toBeCleanUpfiles.Add(encrypted_file_Path);

                try
                {
                    using (FileStream inputFile = File.OpenRead(RenamedSourceFile))
                    using (FileStream outputFile = File.OpenWrite(encrypted_file_Path))
                    using (CryptoStream cryptoStream = new CryptoStream(inputFile, fromBase64, CryptoStreamMode.Read))
                    {
                        cryptoStream.CopyTo(outputFile, 4096);
                    }
                }
                catch
                {
                    throw new Exception("Error during decoding base-64 file");
                }



                #region setup -- extract from file

                byte[] storedSig = new byte[new HMACSHA512().HashSize / 8];

                authKeySalt = new byte[PasswordSaltByteSize];
                keySalt = new byte[PasswordSaltByteSize];
                iv = new byte[AesBlockByteSize];
                iterationNumber = new byte[IterationNumberByteSize];

                #endregion

                //
                // pattern: SignatureByteSize + authKeySalt + keySalt + iv + cipherText
                // read from encrypted file: SignatureByteSize + authKeySalt + keySalt + iv
                // compute the signature of the cipherText
                //

                using (FileStream inStream = File.OpenRead(encrypted_file_Path))
                {
                    // Read in the storedHash.
                    await inStream.ReadAsync(storedSig, 0, storedSig.Length);
                    await inStream.ReadAsync(authKeySalt, 0, authKeySalt.Length);
                    await inStream.ReadAsync(keySalt, 0, keySalt.Length);
                    await inStream.ReadAsync(iv, 0, iv.Length);
                    await inStream.ReadAsync(iterationNumber, 0, iterationNumber.Length);

                    try
                    {
                        PasswordIterationCount = BinaryPrimitives.ReadInt32BigEndian(iterationNumber);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Bad data for iteration number: {ex.Message}");
                    }


                    try
                    {
                        key = GetKey(Password, keySalt);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Bad data for key creation: {ex.Message}");
                    }

                    try
                    {
                        authKey = GetKey(Password, authKeySalt);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Bad data for signature key creation: {ex.Message}");
                    }

                    try
                    {
                        decryptor = aes.CreateDecryptor(key, iv);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Bad data for decryptor creation: {ex.Message}");
                    }

                    //SetupAes(Password, false);

                    byte[] computedHash;

                    computedHash = await inStream.HMACSHA512(authKey, inStream.Position);

                    for (int i = 0; i < storedSig.Length; i++)
                    {
                        if (computedHash[i] != storedSig[i])
                        {
                            throw new Exception("Encrypted file integrity failed: Signature not matched");
                        }
                    }


                    byte[] header = MergeArrays(storedSig, authKeySalt, keySalt, iv, iterationNumber);

                    var decrypted_File = $"{ workingFileNameWithoutExt + source_with_sig_ext}";
                    var decrypted_FilePath = workingFi.FullName.Replace(workingFi.Name, decrypted_File);

                    toBeCleanUpfiles.Add(decrypted_FilePath);

                    using (var decryptedFileStream = File.Create(decrypted_FilePath))
                    using (var decryptCryptoStream = new CryptoStream(inStream, decryptor, CryptoStreamMode.Read))
                    {
                        inStream.Position = header.Length;
                        await decryptCryptoStream.CopyToAsync(decryptedFileStream);
                    }

                    using (HMACSHA512 hmac = new HMACSHA512(authKey))
                    {
                        // Create an array to hold the keyed hash value read from the file.
                        byte[] storedPlainHash = new byte[hmac.HashSize / 8];
                        // Create a FileStream for the source file.
                        using (FileStream sourceFileWithSig = File.OpenRead(decrypted_FilePath))
                        {
                            // Read in the storedHash.
                            await sourceFileWithSig.ReadAsync(storedPlainHash, 0, storedPlainHash.Length);
                            // Compute the hash of the remaining contents of the file.
                            // The stream is properly positioned at the beginning of the content, 
                            // immediately after the stored hash value.
                            byte[] computedPlainHash; // = hmac.ComputeHash(inStream);

                            computedPlainHash = await sourceFileWithSig.HMACSHA512(authKey, sourceFileWithSig.Position);
                            // compare the computed hash with the stored value

                            for (int i = 0; i < storedPlainHash.Length; i++)
                            {
                                if (computedPlainHash[i] != storedPlainHash[i])
                                {
                                    throw new Exception("File integrity failed: Signature not matched");

                                }
                            }

                            var final_decrypted_File = $"{ workingFileNameWithoutExt + source_ext}";
                            var final_decrypted_FilePath = workingFi.FullName.Replace(workingFi.Name, final_decrypted_File);


                            using (var finalDecryptedFileStream = File.Create(final_decrypted_FilePath))
                            {
                                sourceFileWithSig.Position = storedPlainHash.Length;
                                await sourceFileWithSig.CopyToAsync(finalDecryptedFileStream);

                            }

                            RenameToOriginalFile(final_decrypted_FilePath, FilePath);

                            result.Success = true;
                            result.OldFilePath = RenamedSourceFile;
                            result.NewFIlePath = FilePath;

                        }
                    }


                }

            }
            catch
            {
                RenameToOriginalFile(RenamedSourceFile, FilePath);

                result.Success = false;
                result.OldFilePath = FilePath;
                result.NewFIlePath = FilePath;

                throw;
            }
            finally
            {
                CleanUp(toBeCleanUpfiles);
            }

            return result;
        }

        byte[] GenerateRandomBytes(int numberOfBytes)
        {
            var randomBytes = new byte[numberOfBytes];
            Random.GetBytes(randomBytes);
            return randomBytes;
        }


        byte[] GetKey(string password, byte[] passwordSalt)
        {
            var keyBytes = StringEncoding.GetBytes(password);

            using (var derivator = new Rfc2898DeriveBytes(
                keyBytes, passwordSalt,
                PasswordIterationCount, HashAlgorithmName.SHA256))
            {
                return derivator.GetBytes(PasswordByteSize);
            }
        }

        //byte[] GenerateFixBytes(int numberOfBytes)
        //{
        //    var randomBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };

        //    return randomBytes;
        //}

        Aes CreateAes()
        {
            var aes = Aes.Create();
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            return aes;
        }

        private static byte[] MergeArrays(params byte[][] arrays)
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
