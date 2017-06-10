using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

using System.Xml.Serialization;
using Newtonsoft.Json;

namespace GOST
{
    class Sender
    {
        public readonly RSA rsa;
        public RSAParameters publicParameters;
        private RSAParameters privateParameters;
        public string receiverPath;
        public string senderPath;
        public Sender (int keySize, string receiverPath,  string senderPath)
        {
            this.receiverPath = receiverPath;
            this.senderPath = senderPath;
            rsa = RSA.Create();
          
            
            
            //Console.WriteLine(Encoding.UTF8.GetString(m));
            
            
        }
        public void GetOpenKey()
        {
            using (StreamReader file = File.OpenText(receiverPath))
            {
                JsonSerializer serializer = new JsonSerializer();
                //serialize object directly into file stream
                publicParameters = (RSAParameters)serializer.Deserialize(file, privateParameters.GetType());
                rsa.ImportParameters(publicParameters);
            }
        }
        
        public void Send ()
        {
            var sessinKey = BigIntegerExtentions.GenerateBigIntByBitLength(512);

            var m = rsa.Encrypt(Encoding.UTF8.GetBytes(sessinKey.ToString()), RSAEncryptionPadding.CreateOaep(HashAlgorithmName.MD5));

            using (StreamWriter file = File.CreateText(senderPath))
            {
                JsonSerializer serializer = new JsonSerializer();
                //serialize object directly into file stream
                serializer.Serialize(file, publicParameters);
            }

        }
       
        
    

    }
}
