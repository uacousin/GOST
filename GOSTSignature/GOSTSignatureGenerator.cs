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
            //var ql = q.BitLength();
            //var pl = p.BitLength();
            //var al = a.BitLength();
            y = BigInteger.ModPow(a, x, p);
                           
        }

        public static BigInteger GenerateA(BigInteger p, BigInteger q)
        {
            var d = BigIntegerExtentions.GenerateBigIntByBitLength(p.ToBinaryString().Length - 1);
            var dl = p.ToBinaryString().Length - 1;


            BigInteger f = 1;
            
            //var t = (p - 1) % q;
            while (f == 1)
            {              
                f = BigInteger.ModPow(d, (p-1)/q, p);
                d = BigIntegerExtentions.GenerateBigIntByBitLength(p.ToBinaryString().Length - 1);
            }
            
            //var at = BigInteger.ModPow(f, q, p);
            return f;
        }

        public Signature GenerateSignature (BigInteger hashOfMNumber)
        {
            
            //var ml = hashOfMNumber.BitLength();
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
                    //k = BigInteger.Parse("0" + "90F3A564439242F5186EBB224C8E223811B7105C64E4F5390807E6362DF4C72A", System.Globalization.NumberStyles.AllowHexSpecifier);
                    
                    if (k > q)
                    {
                        //Console.WriteLine("Next k");
                        continue;
                    }
                    r = BigInteger.ModPow(a, k, p);
                    rs = r % q;
                }
                s = (x * rs + k * hashOfMNumber) % q;
            }
            //var lk = k.BitLength();
            //var ls = s.BitLength();
            //var lrs = rs.BitLength();
            return new Signature(s, rs, y, hashOfMNumber);
        }


   
    }
}
