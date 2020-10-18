using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Engines.Magic;
using Server.Network;
using Server.Items;
using Server.Spells;
using Server.Targeting;

namespace Scripts.Zulu.Spells.Necromancy
{
    public class AbyssalFlameSpell : NecromancerSpell
    {
        public override TimeSpan CastDelayBase
        {
            get { return TimeSpan.FromSeconds(2); }
        }

        public override double RequiredSkill
        {
            get { return 100.0; }
        }

        public override int RequiredMana
        {
            get { return 60; }
        }

        public AbyssalFlameSpell(Mobile caster, Item scroll) : base(caster, scroll)
        {
        }

        public override void OnCast()
        {
            if (CheckSequence())
            {
                var map = Caster.Map;
                if (map != null)
                    foreach (var m in Caster.GetMobilesInRange(1 + (int) (Caster.Skills[CastSkill].Value / 15.0)))
                        if (Caster != m && SpellHelper.ValidIndirectTarget(Caster, m) &&
                            Caster.CanBeHarmful(m, false) &&
                            Caster.InLOS(m))
                        {
                            var dmg = Utility.Random(30, 30);

                            Caster.DoHarmful(m);

                            //m.Damage( dmg, Caster, m_DamageType ); //resist?  reflect?
                            SpellHelper.Damage(dmg, m, Caster, this, TimeSpan.Zero);
                            m.FixedParticles(0x3709, 10, 30, 5052, EffectLayer.LeftFoot); //flamestrike effect

                            new AbyssalFlameTimer(Caster, m).Start();
                        }

                Caster.PlaySound(0x208);
            }

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