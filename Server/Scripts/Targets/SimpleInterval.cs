using System;

namespace Server
{
    public class SimpleInterval : Timer
    {
        public delegate void Callback();

        private Callback m_Callback;

        public SimpleInterval(TimeSpan interval, Callback callback) :
            base(TimeSpan.Zero, interval)
        {
            m_Callback = callback;
        }

        protected override void OnTick()
        {
            m_Callback();
        }
    }
}
