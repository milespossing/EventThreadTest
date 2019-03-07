using System;
using System.Threading;

namespace EventThreadTest
{
    public class Consumer : IHaultObject
    {
        private Guid Id = Guid.NewGuid();
        private SignalServer signaler;
        public Consumer(SignalServer server)
        {
            signaler = server;
        }

        public bool HaultToken { get; set; } = false;

        public void Run()
        {
            while (!HaultToken)
            {
                signaler.SimpleEvent += SignalerOnSimpleEvent;
                Thread.Sleep(5);
                signaler.SimpleEvent -= SignalerOnSimpleEvent;
            }
        }

        private void SignalerOnSimpleEvent(object sender, EventArgs e)
        {
            
            Thread.Sleep(2);
            Console.WriteLine($"Invoked {Id}");
        }
    }
}