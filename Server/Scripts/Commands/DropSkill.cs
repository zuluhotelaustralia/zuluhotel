using System;
using System.Collections;
using Server;

namespace Server.Commands
{
    public class DropSkillCommand
    {
	public static void Initialize() {
	    CommandSystem.Register( "DropSkill", AccessLevel.Player, new CommandEventHandler( DropSkill_OnCommand ) );
	}

	[Usage( "DropSkill <name>" )]
	[Description( "Drops a specific skill down to zero points." )]
	public static void DropSkill_OnCommand( CommandEventArgs arg )
	{
	    if ( arg.Length != 1 ) {
		arg.Mobile.SendMessage("DropSkill <skill name>");
	    }
	    else {
		SkillName skill;
		Skill s;
		Mobile m = arg.Mobile;
		if (Enum.TryParse( arg.GetString( 0 ), true, out skill ) ){
		    s = m.Skills[skill]; //too many "skill" ffs
		    if (s == null) {
			m.SendMessage("Null Skill error!  This is a bug.  Please contact the administrators.");
			return;
		    }

		    s.Base = 0.0;

		    m.SendMessage("{0} dropped to {1}!", skill, s.Base );
		}
		else {
		    arg.Mobile.SendLocalizedMessage( 1005631 ); //You have specified an invalid skill etc etc
		}
	    }
	}
    }
}
		 
