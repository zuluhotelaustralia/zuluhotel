using System;
using System.Collections;
using Server;
using Server.Engines.Magic;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using Server.Spells;

namespace Scripts.Zulu.Spells.Necromancy
{
    public class WyvernStrikeSpell : NecromancerSpell
    {
        public override TimeSpan CastDelayBase
        {
            get { return TimeSpan.FromSeconds(2); }
        }

        public override double RequiredSkill
        {
            get { return 120.0; }
        }

        public override int RequiredMana
        {
            get { return 100; }
        }

        public WyvernStrikeSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
        {
        }

        public override void OnCast()
        {
            Caster.Target = new InternalTarget(this);
        }

        public void Target(Mobile m)
        {
            if (!Caster.CanSee(m))
            {
                // Seems like this should be responsibility of the targetting system.  --daleron
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
                goto Return;
            }

            if (!CheckSequence()) goto Return;

            SpellHelper.Turn(Caster, m);

            m.FixedParticles(0x3709, 10, 15, 5021, EffectLayer.Waist);
            m.PlaySound(0x1e2);

            Caster.DoHarmful(m);

            var level = 0;
            var pStr = Caster.Skills[CastSkill].Value;

            if (pStr > 100)
                level = 1;
            else if (pStr > 110)
                level = 2;
            else if (pStr > 130)
                level = 3;
            else if (pStr > 140)
                level = 4;
            else
                level = 0;

            var ss = Caster.Skills[DamageSkill].Value;
            var bonus = (int) ss / 4;

            var dmg = (double) Utility.Dice(3, 5, bonus);
            dmg /= 2; //necessary?

            //sith: change this, see issue tracker on gitlab
            if (CheckResisted(m))
            {
                dmg *= 0.75;

                m.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
            }

            //m.Damage((int)dmg, m, ElementalType.Necro);
            SpellHelper.Damage(dmg, m, Caster, this, TimeSpan.Zero);
            m.ApplyPoison(Caster, Poison.GetPoison(level));

            Return:
            FinishSequence();
        }

        private class InternalTarget : Target
        {
            private WyvernStrikeSpell m_Owner;

            // TODO: What is thie Core.ML stuff, is it needed?
            public InternalTarget(WyvernStrikeSpell owner) : base(12, false, TargetFlags.Harmful)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile mobile)
                    m_Owner.Target(mobile);
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }
    }
}