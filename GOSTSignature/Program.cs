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
            var n = new BigInteger(16);
            Console.WriteLine(n.ToBinaryString());
            /*Console.WriteLine("Generating p and q...");
            Console.WriteLine(primeNumberGenerator.q.ToBinaryString().Length);
            Console.WriteLine(primeNumberGenerator.p.ToBinaryString().Length);
            primeNumberGenerator.Generate(1024);
            
            BigInteger p = primeNumberGenerator.p;
            BigInteger q = primeNumberGenerator.q;
            Console.WriteLine("Generating a...");
            BigInteger a = GOSTSignatureGenerator.GenerateA(p, q);
            Console.WriteLine("Generating signature...");
            int x = 124;
            GOSTSignatureGenerator signatureGenerator = new GOSTSignatureGenerator(p, q, a, x);
            var signature = signatureGenerator.GenerateSignature("oleh");
            Console.WriteLine("Checking signature...");
            Console.WriteLine(GOSTSignatureChecker.Check(signature, p, q, a));*/
            Console.ReadKey();
        }
        public static string MD5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
                return Encoding.ASCII.GetString(result);
            }
        }
    }
}
