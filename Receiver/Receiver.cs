using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

using Newtonsoft.Json;

namespace GOST
{
    class Receiver
    {
        public readonly RSA rsa;
        public  RSAParameters publicParameters;
        private  RSAParameters privateParameters;
        public string path;
        public Receiver(int keySize, string path)
        {
            this.path = path;
            rsa = RSA.Create();            
            rsa.KeySize = keySize;
            privateParameters = rsa.ExportParameters(false);
            publicParameters = rsa.ExportParameters(true);
           
        }
        public void PublicOpenKey()
        {
            using (StreamWriter file = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                //serialize object directly into file stream
                serializer.Serialize(file, publicParameters);
            }           
            
        }
        




    }
}
