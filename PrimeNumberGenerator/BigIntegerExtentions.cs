using System;
using System.Numerics;
using System.Text;

namespace GOST
{
    public static class  BigIntegerExtentions
    {
        public static bool MillerRabin(BigInteger p, int  k)
        {
            BigInteger p_1 = p - 1;            
            int temp_len = p_1.ToByteArray().Length - 1;
            int len = temp_len * 8 + Convert.ToString(p_1.ToByteArray()[temp_len], 2).Length;

            for (int i = 0; i < k; i++)
            {
                BigInteger x = GenerateBigIntByBitLength(len);
                if (x == 0)
                {
                    x = GenerateBigIntByBitLength(len);
                }
                if (x % 2 == 0)
                {
                    x--;
                }
                if (BigInteger.GreatestCommonDivisor(p, x) > 1)
                {
                    return false;
                }

                if (!MillerRabin(p, x))
                    return false;
            }                      
            return true;
        }

        public static bool MillerRabin(BigInteger p, BigInteger x)
        {
            BigInteger d = p - 1;
            int s = 0;
            while (d % 2 == 0)
            {
                d = d / 2;
                s++;
            }

            

            BigInteger xr = BigInteger.ModPow(x, d, p);

            if (xr == 1 || xr == p - 1)
                return true;            

            for (int r = 1; r < s; r++)
            {
                xr = BigInteger.ModPow(xr, 2, p);
                if (xr == p - 1)
                    return true;             
                if (xr == 1)               
                    return false;              
            }
            return false;           
        }


        public static byte [] GenerateRandomByteArray(int arrayLength)
        {
            Byte[] bytearray = new Byte[arrayLength];
            Random r = new Random();
            
            r.NextBytes(bytearray);
           
            return bytearray;
            
        }

        public static BigInteger GenerateBigIntByBitLength(int bitlenth)
        {

            var bytearray = GenerateRandomByteArray(bitlenth / 8+1 );
            bytearray[bytearray.Length - 1] = (byte)0x00;
            return new BigInteger(bytearray);
        }

        public static String ToBinaryString (this BigInteger bi)
        {            
            byte[] bytearray = bi.ToByteArray();
            var length = bytearray.Length;

            StringBuilder resultbuilder = new StringBuilder(length * 8+1);
            var binary = Convert.ToString(bytearray[length - 1], 2);

            if (binary[0] != '0'&&bi.Sign == 1)
                resultbuilder.Append("0");

            string temp;

            for (int i= length - 1; i>=0; i--)
            {
                temp = Convert.ToString(bytearray[i], 2);
                resultbuilder.Append(temp.PadLeft(8, '0'));
            }

            return resultbuilder.ToString();

        }


    }
}
