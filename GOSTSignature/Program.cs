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
            var hasher = MD5.Create();
            var hashOfM = hasher.ComputeHash(Encoding.ASCII.GetBytes("oleh"));
            //string hashOfMString = Encoding.Unicode.GetString(hashOfM);
            string hashOfMString = BitConverter.ToString(hashOfM).Replace("-", "").ToLower();
           // Console.WriteLine(hashOfMString);
           // Console.WriteLine(BigInteger.Parse(hashOfMString, System.Globalization.NumberStyles.AllowHexSpecifier));


            GOSTPrimeNumberGenerator gpnGenerator = new GOSTPrimeNumberGenerator();
            gpnGenerator.Generate(1024);
            GOSTSignatureGenerator sGenerator = new GOSTSignatureGenerator(gpnGenerator.p, gpnGenerator.q, 137);
            Console.WriteLine("p = {0}\nq = {1}", gpnGenerator.p, gpnGenerator.q);

            Signature signature = sGenerator.GenerateSignature("12344444324324234234324654642424234");
            Console.WriteLine("s = {0}\nrs = {1}", signature.s, signature.rs);
            GOSTSignatureChecker sChecker = new GOSTSignatureChecker(sGenerator, 137);
            sChecker.CheckSignature(signature.s, signature.rs, sGenerator, signature.M);



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
