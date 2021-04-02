using System;
using System.Threading.Tasks;
using Scripts.Zulu.Utilities;
using Server.Engines.Magic;
using Server.Gumps;
using Server.Mobiles;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;

namespace Server.Spells.Eighth
{
    public class ResurrectionSpell : MagerySpell, ITargetableAsyncSpell<Mobile>
    {
        public ResurrectionSpell(Mobile caster, Item spellItem = null) : base(caster, spellItem) { }

        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;
            
            var target = response.Target;
            
            SpellHelper.Turn(Caster, target);
            
            if (!Caster.CanSee(target))
            {
                Caster.SendFailureMessage(500237); // Target can not be seen.
                return;
            }

            if (target == Caster)
            {
                Caster.SendFailureMessage(501039); // Thou can not resurrect thyself.
                return;
            }
            
            if (!Caster.Alive)
            {
                Caster.SendFailureMessage(501040); // The resurrecter must be alive.
                return;
            }

            if (target.Map == null || !target.Map.CanFit(target.Location, 16, false, false))
            {
                Caster.SendFailureMessage(501042); // Target can not be resurrected at that location.
                target.SendFailureMessage(502391); // Thou can not be resurrected there!
                return;
            }
            
            if (target.Region != null && target.Region.IsPartOf("Khaldun"))
            {
                // The veil of death in this area is too strong and resists thy efforts to restore life.
                Caster.SendFailureMessage(1010395); 
                return;
            }
            
            target.PlaySound(0x214);
            target.FixedEffect(0x376A, 10, 16);
            
            switch (target)
            {
                case BaseCreature {CreatureType: CreatureType.Undead}:
                {
                    // Note: this seems OP? One sots undead if they don't resist it
                    var damage = SpellHelper.TryResist(Caster, target, SpellCircle.Eighth) ? target.Hits / 2 : target.Hits;
                    target.Damage(damage, Caster); // Raw damage
                    return;
                }
                case PlayerMobile {Alive: false} player:
                {
                    player.CloseGump<ResurrectGump>();
                    player.SendGump(new ResurrectGump(player, Caster));
                    break;
                }
            }
        }
    }
}