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
            BigInteger x;
            BigInteger p;
            BigInteger q;
            BigInteger a;
            BigInteger hashOfM;
            
            //Generating p, q, a
            GOSTPrimeNumberGenerator primeNumberGenerator = new GOSTPrimeNumberGenerator();
            Console.WriteLine("Generating p and q...");
            primeNumberGenerator.Generate(512);
            Console.WriteLine(primeNumberGenerator.q.BitLength());
            Console.WriteLine(primeNumberGenerator.p.BitLength());



            //Calculating hash of M
            String M = "message";
            Console.WriteLine("\nCalculating h({0})", M);
            hashOfM = Hash(M);


            //Input parameters 
            //Проверочные параметры (незнаю как это перевести). Чтобы использовать нужно раскомментировать соответствующие
            //строки присвоения параметров p, q, x, a вручную и закомментировать те что ниже.
            //x = BigInteger.Parse("0"+"3036314538303830343630454235324435324234314132373832433138443046", System.Globalization.NumberStyles.AllowHexSpecifier);
            //p = BigInteger.Parse("0" + "EE8172AE8996608FB69359B89EB82A69854510E2977A4D63BC97322CE5DC3386EA0A12B343E9190F23177539845839786BB0C345D165976EF2195EC9B1C379E3", System.Globalization.NumberStyles.AllowHexSpecifier);
            //q = BigInteger.Parse("0" + "98915E7EC8265EDFCDA31E88F24809DDB064BDC7285DD50D7289F0AC6F49DD2D", System.Globalization.NumberStyles.AllowHexSpecifier);
            //a = BigInteger.Parse("0" + "9E96031500C8774A869582D4AFDE2127AFAD2538B4B6270A6F7C8837B50D50F206755984A49E509304D648BE2AB5AAB18EBE2CD46AC3D8495B142AA6CE23E21C", System.Globalization.NumberStyles.AllowHexSpecifier);
            //hashOfM = BigInteger.Parse("0" + "3534454132454236443134453437313943363345374143423445413631454230", System.Globalization.NumberStyles.AllowHexSpecifier);
            //x = BigInteger.Parse("0" + "3036314538303830343630454235324435324234314132373832433138443046", System.Globalization.NumberStyles.AllowHexSpecifier);
            p = primeNumberGenerator.p;
            q = primeNumberGenerator.q;
            x = 12345678;          
            Console.WriteLine("\nGenerating a...");
            a  = GOSTSignatureGenerator.GenerateA(p, q);

            //Generating Signature
            Console.WriteLine("\nGenerating signature...");
            GOSTSignatureGenerator signatureGenerator = new GOSTSignatureGenerator(p, q, a, x);
            var signature = signatureGenerator.GenerateSignature(hashOfM);

            //Checking Signature
            Console.WriteLine("rs= {0}", signature.rs.ToString("X"));
            Console.WriteLine("s= {0}", signature.s.ToString("X"));
            Console.WriteLine("y= {0}", signature.y.ToString("X"));
            Console.WriteLine("hashOfM= {0}", signature.hashOfM.ToString("X"));

            Console.WriteLine("\nChecking signature...");
            Console.WriteLine("Valid: {0}", GOSTSignatureChecker.Check(signature, p, q, a));



            //Wrong Signature 
            signature.hashOfM = Hash("message2");
            Console.WriteLine("\n---------------------------------");
            Console.WriteLine("\nChecking signature...");
            Console.WriteLine("Valid: {0}", GOSTSignatureChecker.Check(signature, p, q, a));

            Console.ReadKey();


        }


        public static BigInteger Hash(string M)
        {
            var hasher = MD5.Create();
            var hashOfMBytes = hasher.ComputeHash(Encoding.ASCII.GetBytes(M));
            string hashOfMString = BitConverter.ToString(hashOfMBytes).Replace("-", "").ToLower();

            return BigInteger.Parse("0" + hashOfMString, System.Globalization.NumberStyles.AllowHexSpecifier);
        }


    
        
    }
}
