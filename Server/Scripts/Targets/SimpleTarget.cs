using System;
using Server;
using Server.Targeting;
using Server.Items;
using Server.Engines.Harvest;
using Server.Mobiles;
using Server.Engines.Quests;
using Server.Engines.Quests.Hag;

namespace Server.Targets
{
	public class SimpleTarget : Target
	{
            public delegate void Callback(Mobile from, object target);
            
            private Callback m_Callback;

            public SimpleTarget( Callback callback ) : this(-1, true, TargetFlags.None, callback ) {}
            
            public SimpleTarget( int range, bool allowGround, TargetFlags flags, Callback callback ) : base( range, allowGround, TargetFlags.None )
            {
                m_Callback = callback;
            }
            
            protected override void OnTarget( Mobile from, object targeted )
            {
                m_Callback(from, targeted);
            }
        }
}
