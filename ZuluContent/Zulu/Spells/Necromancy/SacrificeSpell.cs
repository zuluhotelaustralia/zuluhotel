using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Scripts.Zulu.Utilities;
using Server;
using Server.Engines.Magic;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic;

namespace Scripts.Zulu.Spells.Necromancy
{
    public class SacrificeSpell : NecromancerSpell, ITargetableAsyncSpell<BaseCreature>
    {
        public SacrificeSpell(Mobile caster, Item spellItem = null) : base(caster, spellItem)
        {
        }

        public void Target(Mobile from, Mobile m)
        {
            if (!Caster.CanSee(m))
            {
                // Seems like this should be responsibility of the targetting system.  --daleron
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
                goto Return;
            }

            if (!CheckSequence()) goto Return;

            var c = m as BaseCreature;

            if (c == null)
            {
                Caster.SendMessage("You cannot sacrifice that.");
                goto Return;
            }

            if (c.ControlMaster != Caster)
            {
                Caster.SendMessage("You cannot sacrifice a creature that does not obey you.");
                goto Return;
            }

            if (c.Summoned) Caster.SendMessage("There is not enough life there to sacrifice.");


            Caster.BoltEffect(0);
            Caster.PlaySound(0x207);

            var dmg = c.Hits * SpellHelper.GetEffectiveness(Caster);
            dmg = ZuluUtil.RandomGaussian(dmg, dmg / 4);

            foreach (var target in Caster.Map.GetMobilesInRange(Caster.Location, 4))
            {
                if (!Caster.CanSee(target)) continue;

                Caster.DoHarmful(target);
                target.Damage((int) dmg, Caster /*, ElementalType.Necro*/);
            }

            Return:
            FinishSequence();
        }

        public async Task OnTargetAsync(ITargetResponse<BaseCreature> response)
        {
            if (!response.HasValue)
                return;
            
            var target = response.Target;
            SpellHelper.Turn(Caster, target);
            
            if (target.ControlMaster != Caster)
            {
                Caster.SendMessage("You can only sacrifice one of yours pets");
                return;
            }

            const int range = 4;
            var hp = (double)target.Hits;
            var loc = target.Location;
            
            target.BoltEffect(0);
            target.DeleteCorpseOnDeath = true;
            target.Kill();

            var eable = Caster.Map.GetMobilesInRange(loc, range);
            var victims = eable.Where(m => m != Caster).ToList();
            eable.Free();

            var damage = hp / victims.Count;
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref damage));

            if (damage < 1)
                damage = 1;

            foreach (var mobile in victims)
            {
                if (Caster == mobile || 
                    !SpellHelper.ValidIndirectTarget(Caster, mobile) ||
                    !Caster.CanBeHarmful(mobile, false) || 
                    !Caster.InLOS(mobile))
                {
                    continue;
                }

                SpellHelper.Damage((int)(mobile is BaseCreature ? damage : damage / 2), mobile, Caster, this);

                mobile.FixedParticles(0x36B1, 7, 16, 5021, EffectLayer.Waist);
                mobile.PlaySound(0x207);
            }
        }
    }
}