using System;

using System.Text;
using System.Security.Cryptography;
using System.Numerics;

namespace GOST
{
    internal static class GOSTSignatureChecker
    {
        public static bool Check ( Signature signature, BigInteger p, BigInteger q, BigInteger a)
        {
            var hasher = MD5.Create();
            var hashOfMBytes = hasher.ComputeHash(Encoding.ASCII.GetBytes(signature.M));
            string hashOfMString = BitConverter.ToString(hashOfMBytes).Replace("-", "").ToLower();
            BigInteger hashOfMNumber = BigInteger.Parse(hashOfMString, System.Globalization.NumberStyles.HexNumber);
            BigInteger v = BigInteger.ModPow(hashOfMNumber, q - 2, q);
            BigInteger z1 = (signature.s * v) % q;
            BigInteger z2 = ((q - signature.rs)*v) % q;         
            BigInteger u = ((BigInteger.ModPow(a, z1, p) *BigInteger.ModPow(signature.y, z2, p))% p) % q;
            
            return u==signature.rs;
        }
        

    }
}
