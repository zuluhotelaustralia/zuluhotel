using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;

// note that there is basically a blacklist of certain creatures that cannot be spellbound, no doubt giant monsters like balrogs or something
namespace Server.Spells.Necromancy
{
    public class SpellbindSpell : NecromancerSpell
    {
        private static SpellInfo m_Info = new SpellInfo(
                            "Spellbind", "Nutu Magistri Se Compellere",
                            221, 9002,
                            Reagent.EyeOfNewt, Reagent.VialOfBlood,
                            Reagent.FertileDirt, Reagent.PigIron
                            );

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds(3); } }

        public override double RequiredSkill { get { return 140.0; } }
        public override int RequiredMana { get { return 130; } }

        public SpellbindSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
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
            if (!CheckSequence())
            {
                goto Return;
            }

            if (!m.BeginAction(typeof(SpellbindSpell)))
            {
                goto Return;
            }

            BaseCreature c = m as BaseCreature;

            if (c == null)
            {
                Caster.SendLocalizedMessage(502801); // u cannae tame tha', Harry.
                goto Return;
            }
            if (!c.Tamable)
            {
                c.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 1049655, Caster.NetState); //that cannot be tamed
                goto Return;
            }
            if (c.Controlled)
            {
                c.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 502804, Caster.NetState); //that looks tame already
                goto Return;
            }
            if (Caster.Followers + c.ControlSlots > Caster.FollowersMax)
            {
                Caster.SendLocalizedMessage(1049611); //you have too many followers
                goto Return;
            }

            SpellHelper.Turn(Caster, m);

            // target's difficulty is the higher of their str/dex/int scores divided by 2, scaled by their magicresist skill in some way
            double tdiff = (0.5 * Math.Max(m.Str, Math.Max(m.Dex, m.Int)) / 2.0) + (0.5 * m.Skills[SkillName.MagicResist].Value);

            // caster's power is determined by Magery in zhf, should probably be spirit speak aka DamageSkill
            // if any of you are medically-inclined, yes I called it cdiff on purpose... "shitty" pun ;)
            double cdiff = Caster.Skills[DamageSkill].Value;

            if (cdiff < tdiff)
            {
                DoFizzle();
                goto Return;
            }

            m.PlaySound(0x20d);
            m.FixedParticles(0x37b9, 10, 30, 5052, EffectLayer.Waist); //hopefully this works --sith

            c.Owners.Add(Caster);
            c.SetControlMaster(Caster);

            double duration = cdiff - (m.Skills[SkillName.MagicResist].Value * 0.5);

            new InternalTimer(c, (int)duration).Start();

        Return:
            FinishSequence();
        }

        private class InternalTimer : Timer
        {
            private BaseCreature m_Creature;

            public InternalTimer(BaseCreature target, int duration) : base(TimeSpan.FromSeconds(0))
            {
                Delay = TimeSpan.FromSeconds(duration);
                Priority = TimerPriority.OneSecond;

                m_Creature = target;
            }

            protected override void OnTick()
            {
                m_Creature.SetControlMaster(null); //see BaseCreature
            }
        }

        private class InternalTarget : Target
        {
            private SpellbindSpell m_Owner;

            // TODO: What is thie Core.ML stuff, is it needed?
            public InternalTarget(SpellbindSpell owner) : base(Core.ML ? 10 : 12, false, TargetFlags.Harmful)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile)
                    m_Owner.Target((Mobile)o);
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }

    }
}
