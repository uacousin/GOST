using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Security.Cryptography;

namespace GOST
{
     public class GOSTSignatureChecker
    {
        public static bool Check(Signature signature, BigInteger p, BigInteger q, BigInteger a)
        {
            if (signature.rs < 0 || signature.s < 0)
                return false;
            if (signature.rs > q || signature.s > q)
                return false;

            BigInteger hashOfMNumber = signature.hashOfM;
            BigInteger v = BigInteger.ModPow(hashOfMNumber, q - 2, q);
            BigInteger z1 = (signature.s * v) % q;
            BigInteger z2 = ((q - signature.rs) * v) % q;
            BigInteger u = ((BigInteger.ModPow(a, z1, p) * BigInteger.ModPow(signature.y, z2, p)) % p) % q;

            return u == signature.rs;
        }


    }
}
