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
            Console.WriteLine(hashOfMString);
            Console.WriteLine(BigInteger.Parse(hashOfMString, System.Globalization.NumberStyles.AllowHexSpecifier));
            Console.WriteLine("Hello World!");
            Console.WriteLine("Sanya Connected!!");
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
