using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Numerics;


namespace GOST
{
    public class Receiver
    {
        public IList<byte> encryptedKey { get; private set; }
        public IList<byte> message { get; set; }
        public Signature signature { get; set; }
        public byte[] sessionKey;
        public RSACryptoServiceProvider rsa { get; set; }

        public Receiver() { rsa = new RSACryptoServiceProvider(); }

        public byte[] GenerateSend()
        {
            var publicKey = rsa.ExportCspBlob(false);
            return publicKey;
        }

        static void Main(string[] args)
        {
           
        }

       

        public void ReadFileSender()
        {
            List<object> readedParams = new List<object>();

            string encryptedKeyPath = @"C:\Users\Alexander\Source\Repos\GOST-master\encryptedSessionKey.txt";
            string encryptedMessagePath = @"C:\Users\Alexander\Source\Repos\GOST-master\encryptedMessage.txt";
            string SignaturePath = @"C:\Users\Alexander\Source\Repos\GOST-master\Signature.txt";

            encryptedKey = new List<byte>();
            BigInteger[] signatureAttributes = new BigInteger[4];

            using (StreamReader sr = new StreamReader(encryptedKeyPath))
            {
                while(!sr.EndOfStream)
                encryptedKey.Add(Byte.Parse(sr.ReadLine()));
            }


           message = new List<byte>();

            using (StreamReader sr = new StreamReader(encryptedMessagePath))
            {
                while(!sr.EndOfStream)
                    message.Add(Byte.Parse(sr.ReadLine()));
            }

            using (StreamReader sr = new StreamReader(SignaturePath))
            {
                for (int i = 0; i < signatureAttributes.Length; i++) signatureAttributes[i] = BigInteger.Parse(0 + "" + sr.ReadLine());
            }

            signature = new Signature(signatureAttributes[0], signatureAttributes[1], signatureAttributes[2], signatureAttributes[3]);

        }


        public bool Check(GOSTSignatureGenerator g, Signature s)
        {
            bool isUnchanged = GOSTSignatureChecker.Check(s, g.p, g.q, g.a);
            if(isUnchanged)
            {
                Console.WriteLine("Checking signature result : session key was not changed");
                return true;
            }
            else
            {
                Console.WriteLine("Checking signature result : Key was changed. Something wrong...");
                return false;
            }

        }

        public byte[] DecryptSessionKey(byte[] encryptedKey)
        {
            var privateKey = rsa.ExportCspBlob(true);

            sessionKey = rsa.Decrypt(encryptedKey, false);

            return sessionKey;
        }

        public string DecryptMessage(byte[] encryptedMessage, TripleDESCryptoServiceProvider tripleDes)
        {
            string decryptedMessage = TripleDESManaged.DecryptStringFromBytes(encryptedMessage, tripleDes.Key, tripleDes.IV);

            return decryptedMessage;
        }

    }
}
