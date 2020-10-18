using System;
using System.Collections.Generic;

namespace Server.Spells.Eighth
{
    public class EarthquakeSpell : MagerySpell
    {
        public EarthquakeSpell(Mobile caster, Item scroll) : base(caster, scroll)
        {
        }

        public override bool DelayedDamage
        {
            get { return true; }
        }


        public override void OnCast()
        {
            if (SpellHelper.CheckTown(Caster, Caster) && CheckSequence())
            {
                var targets = new List<Mobile>();

                var map = Caster.Map;

                if (map != null)
                    foreach (var m in Caster.GetMobilesInRange(
                        1 + (int) (Caster.Skills[SkillName.Magery].Value / 15.0)))
                        if (Caster != m && SpellHelper.ValidIndirectTarget(Caster, m) && Caster.CanBeHarmful(m, false))
                            targets.Add(m);

                Caster.PlaySound(0x220);

                foreach (var m in targets)
                {
                    var damage = m.Hits * 6 / 10;

                    if (!m.Player && damage < 10)
                        damage = 10;
                    else if (damage > 75)
                        damage = 75;

                    Caster.DoHarmful(m);
                    SpellHelper.Damage(damage, m, Caster, this, TimeSpan.Zero);
                }
            }

            FinishSequence();
        }
    }
}