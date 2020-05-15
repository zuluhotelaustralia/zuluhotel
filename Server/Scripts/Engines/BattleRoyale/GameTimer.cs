using System;

namespace Server.BattleRoyale
{
    public class GameTimer : Timer
    {
        private Action m_Callback;

        public GameTimer(TimeSpan delay, Action callback) : base(delay)
        {
            m_Callback = callback;
        }

        protected override void OnTick()
        {
            //Server.World.Broadcast( 0x35, true, "GameTimer Tick");
            m_Callback();
        }
    }
}

