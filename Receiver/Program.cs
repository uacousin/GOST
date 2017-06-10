using System;

namespace GOST
{
    class Program
    {
        static void Main(string[] args)
        {
            Receiver receiver = new Receiver(1024, @"C:\Users\Oleh\Documents\Visual Studio 2017\Projects\GOST\KeyExchange\Receive.json");
            receiver.PublicOpenKey();
            
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}