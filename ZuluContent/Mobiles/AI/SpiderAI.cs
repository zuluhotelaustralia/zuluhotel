//
// This is a first simple AI
//
//

using System;
using System.Threading.Tasks;
using Server.Items;
using Server.Network;

namespace Server.Mobiles
{
    public class SpiderAI : MeleeAI
    {
        public SpiderAI(BaseCreature m) : base(m)
        {
        }

        private async Task SpitWeb(Mobile target)
        {
            m_Mobile.PublicOverheadMessage(MessageType.Regular, 0x3B2, true, "The spider spits a web!");

            var location = target.Location;
            
            await Timer.Pause(300);

            var web = new SpiderWeb(location, target.Map, TimeSpan.FromMinutes(2));
            
            m_Mobile.MovingParticles(web, 0xEE4, 7, 0, false, true, 3043, 4043, 0x211);

            if (target.Location == location)
            {
                SpiderWeb.Stick(target);
            }
        }

        public override bool DoActionCombat()
        {
            base.DoActionCombat();
            
            var combatant = m_Mobile.Combatant;

            if (combatant != null && Utility.Random(20) == 1 && m_Mobile.InLOS(combatant) && !combatant.Paralyzed)
            {
                SpitWeb(combatant);
            }

            return true;
        }
    }
}