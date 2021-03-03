using System;
using System.Collections.Generic;
using Server.Targeting;

namespace Server.Spells.Fourth
{
    public class ArchCureSpell : MagerySpell
    {
        public ArchCureSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
        {
        }


        // Arch cure is now 1/4th of a second faster
        public override TimeSpan CastDelayBase
        {
            get { return base.CastDelayBase - TimeSpan.FromSeconds(0.25); }
        }


        public override void OnCast()
        {
            Caster.Target = new InternalTarget(this);
        }

        public void Target(IPoint3D p)
        {
            if (!Caster.CanSee(p))
            {
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (CheckSequence())
            {
                SpellHelper.Turn(Caster, p);

                SpellHelper.GetSurfaceTop(ref p);

                var targets = new List<Mobile>();

                var map = Caster.Map;
                var directTarget = p as Mobile;

                if (map != null)
                {
                    var feluccaRules = map.Rules == MapRules.FeluccaRules;

                    // You can target any living mobile directly, beneficial checks apply
                    if (directTarget != null && Caster.CanBeBeneficial(directTarget, false))
                        targets.Add(directTarget);

                    IPooledEnumerable eable = map.GetMobilesInRange(new Point3D(p), 2);

                    foreach (Mobile m in eable)
                    {
                        if (m == directTarget)
                            continue;

                        if (AreaCanTarget(m, feluccaRules))
                            targets.Add(m);
                    }

                    eable.Free();
                }

                Effects.PlaySound((Point3D)p, Caster.Map, 0x299);

                if (targets.Count > 0)
                {
                    var cured = 0;

                    for (var i = 0; i < targets.Count; ++i)
                    {
                        var m = targets[i];

                        Caster.DoBeneficial(m);

                        var poison = m.Poison;

                        if (poison != null)
                        {
                            var chanceToCure = 10000 + (int) (Caster.Skills[SkillName.Magery].Value * 75) -
                                               (poison.Level + 1) * 1750;
                            chanceToCure /= 100;
                            chanceToCure -= 1;

                            if (chanceToCure > Utility.Random(100) && m.CurePoison(Caster))
                                ++cured;
                        }

                        m.FixedParticles(0x373A, 10, 15, 5012, EffectLayer.Waist);
                        m.PlaySound(0x1E0);
                    }

                    if (cured > 0)
                        Caster.SendLocalizedMessage(1010058); // You have cured the target of all poisons!
                }
            }

            FinishSequence();
        }

        private bool AreaCanTarget(Mobile target, bool feluccaRules)
        {
            /* Arch cure area effect won't cure aggressors, victims, murderers, criminals or monsters.
             * In Felucca, it will also not cure summons and pets.
             * For red players it will only cure themselves and guild members.
             */

            if (!Caster.CanBeBeneficial(target, false))
                return false;

            return true;
        }

        private bool IsAggressor(Mobile m)
        {
            foreach (var info in Caster.Aggressors)
                if (m == info.Attacker && !info.Expired)
                    return true;

            return false;
        }

        private bool IsAggressed(Mobile m)
        {
            foreach (var info in Caster.Aggressed)
                if (m == info.Defender && !info.Expired)
                    return true;

            return false;
        }

        private static bool IsInnocentTo(Mobile from, Mobile to)
        {
            return Notoriety.Compute(from, to) == Notoriety.Innocent;
        }

        private static bool IsAllyTo(Mobile from, Mobile to)
        {
            return Notoriety.Compute(from, to) == Notoriety.Ally;
        }

        private class InternalTarget : Target
        {
            private readonly ArchCureSpell m_Owner;

            public InternalTarget(ArchCureSpell owner) : base(12, true, TargetFlags.None)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                var p = o as IPoint3D;

                if (p != null)
                    m_Owner.Target(p);
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }
    }
}