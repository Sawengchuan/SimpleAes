



// See https://aka.ms/new-console-template for more information

using ConsoleClient;
using EncryptionLib;


//await new TestBenchmark().Encode();

//var summary = new TestBenchmark().Encode();


string password = "password1234";


try
{
    var file = @"E:\temp\temp1.txt";
    var encryptor = new AesEncryptor();

    Console.WriteLine("Starting to encrypt/decrypt!");

    await encryptor.EncryptFile(file, password);

    encryptor = new AesEncryptor();
    await encryptor.DecryptFile(file, password);

    Console.WriteLine("Hello, World!");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);

}


//Console.Read();
