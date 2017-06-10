using System;
using System.Numerics;
using GOST;
namespace GOST
{
    class Program
    {
        static void Main(string[] args)
        {
            string receiverPath = @"C:\Users\Oleh\Documents\Visual Studio 2017\Projects\GOST\KeyExchange\Receive.json";
            string senderPath = @"C:\Users\Oleh\Documents\Visual Studio 2017\Projects\GOST\KeyExchange\Send.json";
            Sender sender = new Sender(1024, receiverPath, senderPath);

            sender.GetOpenKey();
            
            Console.WriteLine(BigIntegerExtentions.FromByteArray(sender.publicParameters.P));

            Console.WriteLine("Hello World!");
        }
    }
}