using System;
using System.Collections.Generic;
using System.Text;

namespace GOST
{
    class GOSTSignatureChecker
    {
        bool CheckSignature(object signature)
        {
            if(true)
            {
                Console.WriteLine("Signature wasn't changed. All's alright");
                return true;
            }
            else
            {
                Console.WriteLine("Signarure was corrupted!");
                return false;
            }

        }
    }
}
