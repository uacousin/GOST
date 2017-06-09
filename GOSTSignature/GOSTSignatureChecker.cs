using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Security.Cryptography;

namespace GOSTSignature
{
     class GOSTSignatureChecker
    {
        private BigInteger x; //Secret key
        public BigInteger y;  //Public key

        public GOSTSignatureChecker(GOSTSignatureGenerator sg, BigInteger x)
        {
            y = BigInteger.ModPow(sg.a, x, sg.p);
        }
           

        public bool CheckSignature(BigInteger s, BigInteger rs, GOSTSignatureGenerator sgen, string M1)   // Must be: 0<s<q and 0<rs<q
         {
            if ((s > 0 && s < sgen.q) && (rs > 0 && rs < sgen.q)) //1
            {
                HashAlgorithm hasher = SHA256.Create();

                var hashOfMBytes = hasher.ComputeHash(Encoding.ASCII.GetBytes(M1));
                string hashOfMString = BitConverter.ToString(hashOfMBytes).Replace("-", "").ToLower();
                
                BigInteger hashOfMNumber = BigInteger.Parse("0" + hashOfMString, System.Globalization.NumberStyles.AllowHexSpecifier);

                BigInteger v = BigInteger.ModPow(hashOfMNumber, sgen.q - 2, sgen.q);
                BigInteger z1 = s * v % sgen.q;
                BigInteger z2 = (sgen.q - rs) * v % sgen.q;

                Console.WriteLine("v = {0}\nz1 = {1}\nz2 = {2}\nhashofnumber = {3}", v, z1, z2, hashOfMString);

                BigInteger u = (BigInteger.Multiply(BigInteger.ModPow(sgen.a, z1, sgen.p), BigInteger.ModPow(y, z2, sgen.p)) % sgen.p) % sgen.q;
                Console.WriteLine("u = {0}", u);

                if(u.ToString().Equals(rs.ToString()))
                {
                    Console.WriteLine("Signature is integer! SUCCESS!");
                    return true;
                }

                 else
                 {
                    //Console.WriteLine("rs = {0}\nu = {1}", rs, u);
                    Console.WriteLine("Integrity of signature was distracted! FAIL!");
                    return false;
                 }
            }
                
            else
            {
                Console.WriteLine("Numbers are not appropriate! FAIL!");
                return false;
            }
            
               
         }


    }
}
