using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace GOST
{
    class Signature
    {
        public readonly string signature;
        public readonly string M;
        public readonly BigInteger s;
        public readonly BigInteger rs;

        
        public Signature (BigInteger s, BigInteger rs, string signature, string M)
        {
            this.s = s;
            this.rs = rs;
            this.signature = signature;
            this.M = M;
        }

    }
}
