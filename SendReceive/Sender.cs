﻿using GOST;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Numerics;

namespace GOST
{
    class Sender
    {
        public RSA rsa;
        public RSAParameters publicParameters;
        private RSAParameters privateParameters;
        private BigInteger signaturePrivateKey;
        public  BigInteger sessionKey;
        public int keySize;
        public GOSTSignatureGenerator signatureGenerator;
        
        public Sender(int keySize)
        {
            this.keySize = keySize;
         
            rsa = RSA.Create();
            signaturePrivateKey = BigIntegerExtentions.GenerateBigIntByBitLength(512);
            signatureGenerator = new GOSTSignatureGenerator(signaturePrivateKey);



            //Console.WriteLine(Encoding.UTF8.GetString(m));


        }
        public void GetOpenParams(RSAParameters oparams)
        {
            rsa.ImportParameters( oparams);
            publicParameters = rsa.ExportParameters(false);
            
        }

        public Message Send()
        {
            byte[] arr;
            BigInteger modulus = BigIntegerExtentions.FromByteArray(publicParameters.Modulus);
            do
            {
                arr = BigIntegerExtentions.GenerateRandomByteArray(128);
                sessionKey = BigIntegerExtentions.FromByteArray(arr);
            }
            while (sessionKey >= modulus);

            var r = sessionKey.ToByteArray();
            var sessinKeyEncrypted = rsa.Encrypt(arr, RSAEncryptionPadding.CreateOaep(HashAlgorithmName.MD5));


            var hasher = MD5.Create();
            var hashOfMBytes = hasher.ComputeHash(sessinKeyEncrypted);
            string hashOfMString = BitConverter.ToString(hashOfMBytes).Replace("-", "").ToLower();

            var hashOfMNumber = BigInteger.Parse("0" + hashOfMString, System.Globalization.NumberStyles.AllowHexSpecifier);
            var signature  = signatureGenerator.GenerateSignature(hashOfMNumber);
            return new Message { signature = signature, key = sessinKeyEncrypted };


        }



    }
}