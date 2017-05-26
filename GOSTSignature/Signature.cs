using System.Numerics;
namespace GOST
{
    class Signature
    {
        public readonly BigInteger s;
        public readonly BigInteger rs;
        public readonly BigInteger y;
        public readonly string M;     
        public Signature(BigInteger s, BigInteger rs, BigInteger y , string M)
        {
            this.s = s;
            this.rs = rs;
            this.M = M;
            this.y = y;
        }
    }
}
