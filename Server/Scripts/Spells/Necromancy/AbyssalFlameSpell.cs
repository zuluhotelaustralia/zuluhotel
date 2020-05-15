using System;
using System.Collections;
using System.Collections.Generic;
using Server.Network;
using Server.Items;
using Server.Targeting;

namespace Server.Spells.Necromancy
{
    public class AbyssalFlameSpell : NecromancerSpell
    {
        private static SpellInfo m_Info = new SpellInfo(
                            "Abyssal Flame", "Orinundus Barathrum Erado Hostes Hostium",
                            227, 9031,
                            Reagent.Brimstone, Reagent.Obsidian, Reagent.VolcanicAsh,
                            Reagent.DaemonBone
                            );

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds(2); } }

        public override double RequiredSkill { get { return 100.0; } }
        public override int RequiredMana { get { return 60; } }

        public AbyssalFlameSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
        {
        }

        public override void OnCast()
        {
            if (!CheckSequence())
            {
                goto Return;

            }

            Map map = Caster.Map;
            if (map != null)
            {
                foreach (Mobile m in Caster.GetMobilesInRange(1 + (int)(Caster.Skills[CastSkill].Value / 15.0)))
                {
                    if (Caster != m && SpellHelper.ValidIndirectTarget(Caster, m) && Caster.CanBeHarmful(m, false) && Caster.InLOS(m))
                    {
                        int dmg = Utility.Random(30, 30);

                        Caster.DoHarmful(m);

                        //m.Damage( dmg, Caster, m_DamageType ); //resist?  reflect?
                        SpellHelper.Damage(this, TimeSpan.Zero, m, Caster, dmg, DamageType.Fire);
                        m.FixedParticles(0x3709, 10, 30, 5052, EffectLayer.LeftFoot); //flamestrike effect

                        new AbyssalFlameTimer(Caster, m).Start();
                    }
                }
            }

            Caster.PlaySound(0x208);

        Return:
            FinishSequence();
        }

        private class AbyssalFlameTimer : Timer
        {
            private Mobile m_Target;
            private Mobile m_Caster;
            public AbyssalFlameTimer(Mobile caster, Mobile target) : base(TimeSpan.Zero, TimeSpan.FromSeconds(1), 3)
            {
                m_Target = target;
                m_Caster = caster;
            }

            protected override void OnTick()
            {
                m_Target.FixedParticles(0x3709, 10, 30, 5052, EffectLayer.LeftFoot); //flamestrike effect
                m_Target.PlaySound(0x208); //foom
            }
        }
    }
}
