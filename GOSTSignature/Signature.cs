using System.Numerics;
namespace GOST
{
    class Signature
    {
        public  BigInteger s;
        public  BigInteger rs;
        public  BigInteger y;
        public  BigInteger hashOfM;     
        public Signature(BigInteger s, BigInteger rs, BigInteger y , BigInteger hashOfM)
        {
            this.s = s;
            this.rs = rs;
            this.hashOfM = hashOfM;
            this.y = y;
        }
    }
}
