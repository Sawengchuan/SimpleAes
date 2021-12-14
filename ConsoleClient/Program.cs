



// See https://aka.ms/new-console-template for more information

using ConsoleClient;
using EncryptionLib;
using System.Buffers.Binary;
using System.Security.Cryptography;


//await new TestBenchmark().Encode();

//var summary = new TestBenchmark().Encode();


string password = "password1234";


try
{
    //var file = @"E:\temp\temp1.txt";
    //var encryptor = new AesEncryptor();

    byte[] dd = new byte[117];
    using (var stream = File.OpenRead(@"E:\temp\temp1_ac836977-dffc-4f59-9f24-75fabe942da6.source_with_sig_encrypted"))
    {
        stream.Read(dd);
    }

    byte[] ee = new byte[4];

    ee[0] = dd[dd.Length - 4];
    ee[1] = dd[dd.Length - 3];
    ee[2] = dd[dd.Length - 2];
    ee[3] = dd[dd.Length - 1];

    var fff = BinaryPrimitives.ReadInt32BigEndian(ee);

    //    stream.Write(merged);

    Console.WriteLine("Hello, World!");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);

}


//Console.Read();
