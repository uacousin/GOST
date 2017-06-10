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
            InitTArray(capacity); //1
            BigInteger[] pArray = new BigInteger[tArray.Count];
            pArray[s] = 65537; //2
            m = s - 1; //3
            int u;
            Random rnd = new Random();
            double d;
            int d1;
            BigInteger N;
            bool flag = false;
            while (m >= 0)
            {
                d = rnd.NextDouble();//4
                d1 = (int)(d * 1000);           
                var temp = BigInteger.Pow(2, tArray[m] - 1) / pArray[m + 1]+1;
                var temp2 = (BigInteger.Pow(2, tArray[m] - 1) * d1) / (pArray[m + 1] * 1000);
                N = temp + temp2;
                if (N % 2 == 1)
                    N++;
               
                u = 0; //5
                while (true)
                {
                    pArray[m] = pArray[m + 1] * (N + u) + 1; //6
                    if (pArray[m] > BigInteger.Pow(2, tArray[m]))
                    { 
                        flag = true;
                        break; //7   
                    }
                    if (BigInteger.ModPow(2, (N + u) * pArray[m + 1], pArray[m]) == 1 && BigInteger.ModPow(2, (N + u), pArray[m]) != 1)//8
                    {
                        m--;
                        flag = true;
                        break;
                    }
                    u += 2;                 

                }
                
               
            }
            p = pArray[0];
            q = pArray[1];


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
