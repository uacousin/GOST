using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace GOST
{
    class Receiver
    {
        public readonly RSA rsa;
        public RSAParameters publicParameters;
        private RSAParameters privateParameters;
        public string path;
        public BigInteger sessionKey;
        public Receiver(int keySize)
        {
            
            
            rsa = RSA.Create();
            rsa.KeySize = keySize;
            privateParameters = rsa.ExportParameters(true);
            publicParameters = rsa.ExportParameters(false);

        }   

        public bool GetKey(Message message)
        {

            if (!GOSTSignatureChecker.Check(message.signature))
                return false;
            else
            {
                sessionKey = new BigInteger(rsa.Decrypt(message.key, RSAEncryptionPadding.CreateOaep(HashAlgorithmName.MD5)));
                return true;
            }

        }




    }
}
