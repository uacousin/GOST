using System;
using System.Collections.Generic;
using System.Text;

namespace GOST
{
    class Signature
    {
        public readonly string signature;
        public readonly string M;
        public Signature (string signature, string M)
        {
            this.signature = signature;
            this.M = M;
        }

    }
}
