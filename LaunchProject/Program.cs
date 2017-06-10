using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace GOST
{
    class Launcher
    {
        static void Main(string[] args)
        {
            string message = "HelloWorldAndPeople";

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Our message: {0}", message);
            try
            {
                Receiver r = new Receiver();
                Sender s = new Sender(message, r.GenerateSend());

                
                Console.WriteLine("Session key in sender : {0}", BigIntegerExtentions.FromByteArray(s.sessionKey));
                List<object> parameters = s.SendParams();
                 

                
                r.ReadFileSender();

                GOSTSignatureGenerator g = parameters[3] as GOSTSignatureGenerator;
                Signature signature = r.signature;

                if (g != null && signature != null)
                {
                    if (r.Check(g, signature))
                    {
                        byte[] key = r.DecryptSessionKey(r.encryptedKey.ToArray());
                        string decryptedMessage = r.DecryptMessage(r.message.ToArray(), s.tripleDES);
                        Console.WriteLine();
                        Console.WriteLine("Session key in receiver: {0}", BigIntegerExtentions.FromByteArray(r.sessionKey));
                        Console.WriteLine("Decrypted message: {0}", decryptedMessage);
                        Console.WriteLine();

                        if(message.Equals(decryptedMessage))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("MESSAGES ARE EQUAL");
                            
                        }

                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("MESSAGES ARE NOT EQUAL");

                        }

                    }
                }

                else
                {
                    Console.WriteLine("Error of converting!!!");
                }

            }

            catch(Exception e)
            {
                Console.WriteLine("Exception occured!!!");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

            }

          


        }
    }
}
