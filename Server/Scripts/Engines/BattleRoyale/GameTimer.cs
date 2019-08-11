using System;

namespace Server.BattleRoyale
{
    public class GameTimer : Timer {
	private Action m_Callback;
	
	public GameTimer( TimeSpan delay, Action callback ) : base ( delay ){
	    m_Callback = callback;
	}
	
	protected override void OnTick() {
	    m_Callback();
	}
    }
}

