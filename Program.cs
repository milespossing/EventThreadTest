using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventThreadTest
{
    class Program
    {
        static List<IHaultObject> objects = new List<IHaultObject>();
        static void Main(string[] args)
        {
            SignalServer server = new SignalServer();
            objects.Add(server);
            Thread t = new Thread(server.Run);
            t.Start();
            for (int i = 0; i < 10; i++) MakeNewThread(server);
            Thread.Sleep(10000);
            Stop();
            Console.WriteLine("Done");
            Console.ReadLine();
        }

        static void MakeNewThread(SignalServer s)
        {
            Consumer c = new Consumer(s);
            Thread t = new Thread(c.Run);
            t.Start();
            objects.Add(c);
        }

        static void Stop()
        {
            foreach (var haultObject in objects)
            {
                haultObject.HaultToken = true;
            }
        }
    }
}
