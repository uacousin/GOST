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
        public readonly BigInteger y;
        private BigInteger x;
        public HashAlgorithm hasher;
        
        public GOSTSignatureGenerator (BigInteger p, BigInteger q, BigInteger a,  BigInteger x)
        {
            GOSTPrimeNumberGenerator primeNumberGenerator = new GOSTPrimeNumberGenerator();            
            this.p = p;
            this.q = q;
            this.a = a;
            this.x = x;
            var ql = q.BitLength();
            var pl = p.BitLength();
            var al = a.BitLength();
            y = BigInteger.ModPow(a, x, p);
            hasher = MD5.Create();                      
        }

        public static BigInteger GenerateA(BigInteger p, BigInteger q)
        {
            var d = BigIntegerExtentions.GenerateBigIntByBitLength(p.ToBinaryString().Length - 1);
            BigInteger f = 1;
            //var t = (p - 1) % q;
            while (f == 1)
                f = BigInteger.ModPow(d, (p - 1) / q, p);
            //var at = BigInteger.ModPow(f, q, p);
            return f;
        }

        public Signature GenerateSignature (string M)
        {
            var hashOfMBytes = hasher.ComputeHash(Encoding.ASCII.GetBytes(M));
            string hashOfMString  = BitConverter.ToString(hashOfMBytes).Replace("-", "").ToLower();
            BigInteger hashOfMNumber = new BigInteger(hashOfMBytes);
            var ml = hashOfMNumber.BitLength();
            BigInteger k;
            BigInteger r;
            BigInteger rs = 0;
            BigInteger s=0;
            int qLength = q.ToBinaryString().Length;
            while (s == 0)
            {
                while (rs == 0)
                {
                    k = BigIntegerExtentions.GenerateBigIntByBitLength(qLength - 1);
                    
                    r = BigInteger.ModPow(a, k, p);
                    rs = r % q;
                }
                s = (x * rs + k * hashOfMNumber) % q;
            }
            var lk = k.BitLength();
            var ls = s.BitLength();
            var lrs = rs.BitLength();
            return new Signature(s, rs, y, M);
        }


   
    }
}
