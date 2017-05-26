using System;
using System.Collections.Generic;
using System.Text;

using System.Numerics; 

namespace GOST
{   
    public class GOSTPrimeNumberGenerator 
    {
        public BigInteger p;
        public BigInteger q;
        public BigInteger x0 { get; set; }
        public BigInteger c { get; set; }
        public List<int> tArray { get; }
        public List<BigInteger> xArray { get; }
        public List<BigInteger> yArray;
        private int s;
        private int m;
        public GOSTPrimeNumberGenerator()
        {
           
            tArray = new List<int>();
            xArray = new List<BigInteger>();
            yArray = new List<BigInteger>();
            
                        
        }
        public void Generate(int capacity)
        {   
        
            c = BigIntegerExtentions.GenerateBigIntByBitLength(16);
            if (c % 2 == 0)
                c--;
            x0 = BigIntegerExtentions.GenerateBigIntByBitLength(16);
            BigInteger y0 = x0;
            //yArray.Add(x0); //First step
            InitTArray(capacity); //Second step
            BigInteger[] pArray = new BigInteger[s + 1];
            pArray[s] = 65537; // Third step
            m = s - 1; //4th step
            int k;
            BigInteger N;
            while (m >= 0)
            {
                int rm = tArray[m + 1] / 16 + 1; //5th step
                while (true)
                {
                    GenerateYArray(rm, y0); //6th step
                    BigInteger Ym ;

                    for (int i = 0; i < rm - 1; i++)
                    {
                        Ym += yArray[i];
                    }
                    Ym = BigInteger.Multiply(Ym, 1); // 7th step
                    y0 = yArray[rm];
                    yArray[0] = yArray[rm - 1]; //8th step
                    var temp = BigInteger.Pow(2, tArray[m] - 1) / pArray[m + 1];
                    var temp2 = BigInteger.Pow(2, tArray[m] - 1) * Ym / (pArray[m + 1] * BigInteger.Pow(2,16*rm));
                    N =  temp+ temp2 +1 ;
                    if (N % 2 == 1)
                        N++;
                    k = 0; // 10th step 
                    while (true)
                    {
                        pArray[m] = pArray[m + 1] * (N + k) + 1; //11 step
                        if (pArray[m] > BigInteger.Pow(2, tArray[m]))
                            break; //12th step
                        if (!((BigInteger.ModPow(2, pArray[m + 1] * (N + k), pArray[m]) == 1) && (BigInteger.ModPow(2, (N + k), pArray[m]) != 1)))
                        {
                            k += 2;
                            continue;
                        }
                        else
                        {
                            m--;
                            break;
                            
                        }
                        
                    }
                    if (m < 0)
                    {
                        p = pArray[0];
                        if (capacity == 1024)
                            q = pArray[2];
                        else
                            q = pArray[10];
                        return;
                    }


                }
                
               
            }

        }

       
        private BigInteger NextYForLittle(BigInteger y)
        {

            return BigInteger.ModPow(BigInteger.Multiply(19381,y) + c, 1, BigInteger.Pow(2,16));

        }
        private void GenerateYArray (int rm, BigInteger y0)
        {
            yArray.Clear();
            yArray.Add(y0);
            for (int i=0;  i<rm; i++ )
            {
                yArray.Add(NextYForLittle(yArray[i]));
            }
        }       

        public void InitTArray(int capacity)
        {
           s = 0;
           while(capacity>=16)
           {
                tArray.Add(capacity);
                capacity /= 2;
                s++;
           }
            s--;
        }


        public  BigInteger Next(int capacity)
        {
            return p;
        }
        private int GetDegreeOfTwo(int x)
        {
            int temp = 2;
            int degree = 1;
            while(temp<=x)
            {
                if (temp == x)
                    return degree;
                temp ^= 2;
                degree++;
            }
            return 0;
        }
    }


}
