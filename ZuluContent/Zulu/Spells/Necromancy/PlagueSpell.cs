using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Network;
using Server.Items;
using Server.Spells;
using Server.Targeting;

namespace Scripts.Zulu.Spells.Necromancy
{
    public class PlagueSpell : NecromancerSpell
    {
        public override TimeSpan CastDelayBase
        {
            get { return TimeSpan.FromSeconds(0); }
        }

        public override double RequiredSkill
        {
            get { return 140.0; }
        }

        public override int RequiredMana
        {
            get { return 130; }
        }

        public PlagueSpell(Mobile caster, Item scroll) : base(caster, scroll)
        {
        }

        public override void OnCast()
        {
            if (!CheckSequence()) goto Return;

            var targets = new List<Mobile>();
            var map = Caster.Map;

            var level = 0;
            var pStr = Caster.Skills[DamageSkill].Value;

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

            if (map != null)
                foreach (var m in Caster.GetMobilesInRange(1 + (int) (Caster.Skills[CastSkill].Value / 15.0)))
                    if (Caster != m &&
                        SpellHelper.ValidIndirectTarget(Caster, m) &&
                        Caster.CanBeHarmful(m, false)
                        && Caster.InLOS(m))
                    {
                        Caster.DoHarmful(m);

                        m.ApplyPoison(Caster, Poison.GetPoison(level));
                    }

            Caster.PlaySound(0x1e2);

            Return:
            FinishSequence();
        }
    }
}