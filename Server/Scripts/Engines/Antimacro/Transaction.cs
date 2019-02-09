using System;
using Server;
using Server.Accounting;
using Server.Mobiles;

/* 
   An "antimacro transaction" represents an event where in a player is sent a challenge from the system and either responds correctly, incorrectly, or not at all and then the system 
   decides what action to take against the player, if any
   --sith
   */

namespace Server.Antimacro
{
    class AntimacroTransaction {
	private Mobile m_Subject;
	private Account m_SubjectAccount;

	public AntimacroTransaction( Mobile m ) {
	    m_Subject = m;
	    m_SubjectAccount = m_Subject.Account as Accounting.Account;
	}

	public void Decide
    }
}
