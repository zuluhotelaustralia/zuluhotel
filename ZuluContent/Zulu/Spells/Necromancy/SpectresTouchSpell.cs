using System;
using System.Collections.Generic;
using System.Collections;
using Server;
using Server.Engines.Magic;
using Server.Network;
using Server.Items;
using Server.Spells;
using Server.Targeting;

namespace Scripts.Zulu.Spells.Necromancy
{
    public class SpectresTouchSpell : NecromancerSpell
    {
        public override TimeSpan CastDelayBase
        {
            get { return TimeSpan.FromSeconds(1); }
        }

        public override double RequiredSkill
        {
            get { return 80.0; }
        }

        public override int RequiredMana
        {
            get { return 40; }
        }

        public SpectresTouchSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
        {
        }

        public override void OnCast()
        {
            if (!CheckSequence()) goto Return;

            var targets = new List<Mobile>();
            var map = Caster.Map;
            if (map != null)
                foreach (var m in Caster.GetMobilesInRange(1 + (int) (Caster.Skills[CastSkill].Value / 15.0)))
                    if (Caster != m &&
                        SpellHelper.ValidIndirectTarget(Caster, m) &&
                        Caster.CanBeHarmful(m, false) &&
                        Caster.InLOS(m)
                    )
                        targets.Add(m);

            double dmg = Utility.Dice(3, 5, (int) (Caster.Skills[DamageSkill].Value / 4.0)); //avg 41 or so

            Caster.PlaySound(0x1F1);
            foreach (var m in targets)
            {
                Caster.DoHarmful(m);
                //m.Damage( (int)dmg, Caster, m_DamageType );
                SpellHelper.Damage(dmg, m, Caster, this, TimeSpan.Zero);
                m.FixedParticles(0x374A, 10, 15, 5013, EffectLayer.Waist);
                m.PlaySound(0x1f2);
            }

            Return:
            FinishSequence();
        }

        private class InternalTimer : Timer
        {
            private Mobile m_Target;

            public InternalTimer(Mobile target, Mobile caster) : base(TimeSpan.FromSeconds(0))
            {
                m_Target = target;

                // TODO: Compute a reasonable duration, this is stolen from ArchProtection
                var time = caster.Skills[SkillName.Magery].Value * 1.2;
                if (time > 144)
                    time = 144;
                Delay = TimeSpan.FromSeconds(time);
                Priority = TimerPriority.OneSecond;
            }

            protected override void OnTick()
            {
                m_Target.EndAction(typeof(SpectresTouchSpell));
            }
        }
    }
}