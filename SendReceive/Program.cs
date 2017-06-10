using System;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace GOST
{
    class Program
    {
        static void Main(string[] args)
        {
            int keysize = 512;
            Receiver receiver = new Receiver(keysize);
            var publicKey = receiver.publicParameters;
            Sender sender = new Sender(keysize);
            sender.GetOpenParams(publicKey);
            var message = sender.Send();
            Console.WriteLine( receiver.GetKey(message));
            Console.WriteLine(receiver.sessionKey);
            Console.WriteLine(sender.sessionKey);
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}