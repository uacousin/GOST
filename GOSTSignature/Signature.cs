using System.Numerics;
namespace GOST
{
    class Signature
    {
        public  BigInteger s;
        public  BigInteger rs;
        public  BigInteger y;
        public  string M;     
        public Signature(BigInteger s, BigInteger rs, BigInteger y , string M)
        {
            this.s = s;
            this.rs = rs;
            this.M = M;
            this.y = y;
        }
    }
}
