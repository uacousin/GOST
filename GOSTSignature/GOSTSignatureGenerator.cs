using System;

using System.Text;
using System.Numerics;

using System.Security.Cryptography;


namespace GOST
{
     class GOSTSignatureGenerator
    {
        public readonly BigInteger p;
        public readonly BigInteger q;
        public readonly BigInteger a;
        private BigInteger x;
        public HashAlgorithm hasher;
        
        public GOSTSignatureGenerator (BigInteger p, BigInteger q, BigInteger x)
        {
            GOSTPrimeNumberGenerator primeNumberGenerator = new GOSTPrimeNumberGenerator();            
            this.p = p;
            this.q = q;
            a = GenerateA();
            hasher = SHA256.Create();                      
        }

        public BigInteger GenerateA()
        {
            var d = BigIntegerExtentions.GenerateBigIntByBitLength(p.ToBinaryString().Length - 1);
            BigInteger f = 1;
            while (f == 1)
                f = BigInteger.ModPow(d, (p - 1) / q, p);
            return f;
        }

        public Signature GenerateSignature (string M)
        {
            var hashOfMBytes = hasher.ComputeHash(Encoding.ASCII.GetBytes(M));
            string hashOfMString  = BitConverter.ToString(hashOfMBytes).Replace("-", "").ToLower();
            //BigInteger hashOfMNumber = new BigInteger(hashOfMBytes);
            BigInteger hashOfMNumber = BigInteger.Parse("0" + hashOfMString, System.Globalization.NumberStyles.AllowHexSpecifier);

            BigInteger k;
            BigInteger r;
            BigInteger rs = 0;
            BigInteger s=0;
            while (s == 0)
            {
                while (rs == 0)
                {
                    k = BigIntegerExtentions.GenerateBigIntByBitLength(q.ToBinaryString().Length - 1);
                    r = BigInteger.ModPow(a, k, p);
                    rs = r % q;
                }
                s = (x * rs + k * hashOfMNumber) % q;
            }
            return new Signature(s, rs, rs.ToString() + s.ToString(), hashOfMString);
        }


   
    }
}
