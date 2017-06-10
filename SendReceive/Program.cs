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
            int keysize = 2048;

            Console.WriteLine("Initializing Receiver...");
            Receiver receiver = new Receiver(keysize);

            Console.WriteLine("Public RSA parameters");
            var publicKey = receiver.publicParameters;
            Console.WriteLine("modulus: {0}, ", BigIntegerExtentions.FromByteArray(receiver.publicParameters.Modulus), BigIntegerExtentions.FromByteArray(receiver.publicParameters.Exponent));
            Console.WriteLine("\nInitializing Sender...");
            Sender sender = new Sender(keysize);
            Console.WriteLine("Receive RSA public params");
            sender.GetOpenParams(publicKey);

            var message = sender.Send();
            Console.WriteLine("valid sign?: {0}", receiver.GetKey(message));
            Console.WriteLine("receiver session key: {0}", receiver.sessionKey);
            Console.WriteLine("sender session key: {0}", sender.sessionKey);
          
            Console.ReadKey();
        }
    }
}