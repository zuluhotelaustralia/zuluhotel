using System;

namespace Server
{
	public class SimpleTimeout : Timer
	{
            public delegate void Callback();
            
            private Callback m_Callback;
            
            public SimpleTimeout( TimeSpan span, Callback callback ) : base( span )
            {
                m_Callback = callback;
            }

            protected override void OnTick()
            {
                m_Callback();
            }
	}
}
