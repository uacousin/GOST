using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Numerics;
using System.IO;


namespace GOST
{
    public class Sender
    {
        public string message { get; set; }
        public RSACryptoServiceProvider Rsa { get; set; }
        public byte[] sessionKey;
        public TripleDESCryptoServiceProvider tripleDES { get; set; }

        public Sender(string message, byte[] publicKey )
        {
            Rsa = new RSACryptoServiceProvider(512);
            tripleDES = new TripleDESCryptoServiceProvider();
            sessionKey = new byte[tripleDES.Key.Length];
            tripleDES.Key.CopyTo(sessionKey, 0);
            Rsa.ImportCspBlob(publicKey);
            this.message = message;

        }

        public List<object> SendParams()
        {
            //Objects we must send to receiver
            List<object> sendParams = new List<object>();

            string encryptedKeyPath = @"C:\Users\Alexander\Source\Repos\GOST-master\encryptedSessionKey.txt";
            string encryptedMessagePath = @"C:\Users\Alexander\Source\Repos\GOST-master\encryptedMessage.txt";
            string SignaturePath = @"C:\Users\Alexander\Source\Repos\GOST-master\Signature.txt";

            //generating rsa keys
           
           
            

            //encrypting session key
   
            sessionKey = tripleDES.Key;
            byte[] sendedKey = Rsa.Encrypt(tripleDES.Key, false);

            using (StreamWriter sw = new StreamWriter(encryptedKeyPath))
            {
                for (int i = 0; i < sendedKey.Length; i++) sw.WriteLine(sendedKey[i]);
            }



             BigInteger x;
            BigInteger p;
            BigInteger q;
            BigInteger a;
            BigInteger hashOfM;

            GOSTPrimeNumberGenerator primeNumberGenerator = new GOSTPrimeNumberGenerator();
            Console.WriteLine("Generating p and q...");
            primeNumberGenerator.Generate(512);
            Console.WriteLine(primeNumberGenerator.q.BitLength());
            Console.WriteLine(primeNumberGenerator.p.BitLength());

            p = primeNumberGenerator.p;
            q = primeNumberGenerator.q;
            x = 12345678;
            Console.WriteLine("\nGenerating a...");
            a = GOSTSignatureGenerator.GenerateA(p, q);

            hashOfM = Hash(Encoding.Default.GetString(sendedKey));

            //Generating Signature
            Console.WriteLine("\nGenerating signature...");
            GOSTSignatureGenerator signatureGenerator = new GOSTSignatureGenerator(p, q, a, x);

            var signature = signatureGenerator.GenerateSignature(hashOfM);

            using (StreamWriter sw = new StreamWriter(SignaturePath))
            {
                sw.WriteLine(signature.s);
                sw.WriteLine(signature.rs);
                sw.WriteLine(signature.y);
                sw.WriteLine(signature.hashOfM);
        
            }

            //Checking Signature
            Console.WriteLine("rs= {0}", signature.rs.ToString("X"));
            Console.WriteLine("s= {0}", signature.s.ToString("X"));
            Console.WriteLine("y= {0}", signature.y.ToString("X"));
            Console.WriteLine("hashOfM= {0}", signature.hashOfM.ToString("X"));

            byte[] encryptedMessage = TripleDESManaged.EncryptStringToBytes(message, tripleDES.Key, tripleDES.IV);

            using (StreamWriter sw = new StreamWriter(encryptedMessagePath))
            {
                for (int i = 0; i < encryptedMessage.Length; i++) sw.WriteLine(encryptedMessage[i]);
            }


            sendParams.Add(encryptedMessage);
            sendParams.Add(signature);
            sendParams.Add(sendedKey);
            sendParams.Add(signatureGenerator);


            return sendParams;

        }

        public static BigInteger Hash(string M)
        {
            var hasher = MD5.Create();
            var hashOfMBytes = hasher.ComputeHash(Encoding.ASCII.GetBytes(M));
            string hashOfMString = BitConverter.ToString(hashOfMBytes).Replace("-", "").ToLower();

            return BigInteger.Parse("0" + hashOfMString, System.Globalization.NumberStyles.AllowHexSpecifier);
        }



        static void Main(string[] args)
        {

        }
    }
}
