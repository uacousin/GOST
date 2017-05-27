using System;
using System.Text;
using System.Security.Cryptography;
using System.Numerics;
namespace GOST
{
    class Program
    {
        static void Main(string[] args)
        {
            
            GOSTPrimeNumberGenerator primeNumberGenerator = new GOSTPrimeNumberGenerator();            
            Console.WriteLine("Generating p and q...");
            
            
            primeNumberGenerator.Generate(512);
            Console.WriteLine(primeNumberGenerator.q.BitLength());
            Console.WriteLine(primeNumberGenerator.p.BitLength());
            BigInteger p =  primeNumberGenerator.p;
            BigInteger q =primeNumberGenerator.q;
            Console.WriteLine("Generating a...");
            BigInteger a = GOSTSignatureGenerator.GenerateA(p, q);
            Console.WriteLine("Generating signature...");
            int x = new BigInteger("");
            GOSTSignatureGenerator signatureGenerator = new GOSTSignatureGenerator(p, q, a, x);
            var signature = signatureGenerator.GenerateSignature("message");
            Console.WriteLine("Checking signature...");
            Console.WriteLine(GOSTSignatureChecker.Check(signature, p, q, a));

            signature.M = "message2";           
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Checking signature...");
            Console.WriteLine(GOSTSignatureChecker.Check(signature, p, q, a));


            Console.ReadKey();
        }
        
    }
}
