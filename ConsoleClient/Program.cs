



// See https://aka.ms/new-console-template for more information

using ConsoleClient;
using EncryptionLib;
using System.Security.Cryptography;


//await new TestBenchmark().Encode();

//var summary = new TestBenchmark().Encode();


string password = "password1234";


try
{
    //var file = @"E:\temp\temp1.txt";
    //var encryptor = new AesEncryptor();

    //Console.WriteLine("Starting to encrypt/decrypt!");

    //await encryptor.EncryptFile(file, password);

    //encryptor = new AesEncryptor();
    //await encryptor.DecryptFile(file, password);



    byte[] key = new byte[] { 1,2,3,4,5,6 }; // RandomNumberGenerator.GetBytes(99);


    HMACSHA256 hmac = new HMACSHA256(key);


    Dictionary<string, byte> dict = new Dictionary<string, byte>();

    for(byte b = byte.MinValue; b < byte.MaxValue; b++)
    {
        dict.Add(BitConverter.ToString(hmac.ComputeHash(new byte[] { b })), b);
    }

    byte b99 = 99;
    var b99Hash = BitConverter.ToString(hmac.ComputeHash(new byte[] { 99 }));

    var value = dict[b99Hash];


    var b1 = new byte[]{ 1, 2, 3 };
    var b2 = new byte[] { 11, 12, 13, 14 };
    var b3 = new byte[] { 21, 22, 23, 24, 25 };

    var resultt = HeaderHelper.GenerateHeader(10, b1, b2, b3);

    var totalXtra = resultt.Sum( b => b.Length );

    var merged = new byte[totalXtra];

    int runningIndex = 0;
    foreach(var x in resultt)
    {
        x.CopyTo(merged, runningIndex);
        runningIndex += x.Length;

    }



    using (var stream = File.Create(@"E:\temp\tempWrite.txt"))
        stream.Write(merged);

        Console.WriteLine("Hello, World!");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);

}


//Console.Read();
