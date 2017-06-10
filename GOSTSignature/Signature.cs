using System.Numerics;
namespace GOST
{
    public class Signature
    {
        public BigInteger p;
        public BigInteger q;
        public BigInteger a;
        public  BigInteger s;
        public  BigInteger rs;
        public  BigInteger y;
        public  BigInteger hashOfM;     
        public Signature(BigInteger s, BigInteger rs, BigInteger y , BigInteger hashOfM, BigInteger p, BigInteger q, BigInteger a)
        {
            this.s = s;
            this.rs = rs;
            this.hashOfM = hashOfM;
            this.y = y;
            this.p = p;
            this.q = q;
            this.a = a;
        }
    }
}
