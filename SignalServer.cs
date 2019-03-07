using System;

namespace EventThreadTest
{
    public class SignalServer : IHaultObject
    {
        private int count = 0;
        public event EventHandler SimpleEvent;

        public bool HaultToken { get; set; } = false;

        public void Run()
        {
            while (!HaultToken)
            {
                count += 1;
                Console.WriteLine($"Invoke {count}");
                SimpleEvent?.Invoke(this, new EventArgs());
            }
        }
    }
}