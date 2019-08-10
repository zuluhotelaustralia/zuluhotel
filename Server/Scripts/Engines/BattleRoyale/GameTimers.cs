using System;

namespace Server.BattleRoyale
{
    public class JoinTimer : Timer {

	public JoinTimer( TimeSpan delay ) : base ( delay ){}
	
	protected override void OnTick() {
	    GameController.EndJoining();
	}
    }

    public class ResTimer : Timer {
	public ResTimer( TimeSpan delay ) : base ( delay ){}

	protected override void OnTick() {
	    GameController.BeginPlay();
	}
    }
}
