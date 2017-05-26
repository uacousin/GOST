using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;



namespace GOST
{
    class Program
    {
        static void Main(string[] args)
        {

            int quantity = 10;
            int capacity = 2048;
            
            Console.WriteLine("quantity: {0}, length: {1} bits", quantity, capacity);
            GOSTPrimeNumberGenerator gostgenerator = new GOSTPrimeNumberGenerator();
            var time1 = DateTime.Now.TimeOfDay;
            List<BigInteger> primesByGost = new List<BigInteger>();
            for (int i = 0; i < quantity; i++)
            {
                gostgenerator.Generate(capacity);
                primesByGost.Add(gostgenerator.p);                
            }
            var time = DateTime.Now.TimeOfDay - time1;
            //primesByGost.ForEach((x) => { Console.WriteLine(x); });
            //primesByGost.ForEach((x) => { Console.WriteLine(BigIntegerExtentions.MillerRabin(x, 10)); });
            Console.WriteLine("Time: {0} ms,\n Avg: {1} ms", time.TotalMilliseconds, (double)time.TotalMilliseconds / quantity);
            Console.WriteLine("Done!");
            
            Console.WriteLine("Parallel");
            primesByGost.Clear();

            time1 = DateTime.Now.TimeOfDay;
            var result = Parallel.For(0, quantity, (x) => { var gg = new GOSTPrimeNumberGenerator(); gg.Generate(capacity); primesByGost.Add(gostgenerator.p); });
            
            while(!result.IsCompleted)
            {
                continue;
            }
            time = DateTime.Now.TimeOfDay - time1;
            Console.WriteLine("Time: {0} ms,\n Avg: {1} ms", time, (double)time.TotalMilliseconds / quantity);
            Console.WriteLine("Done!");
            //Console.WriteLine(gostgenerator.p.ToBinaryString().Length-8);


            Console.ReadKey();
        }
    }
}