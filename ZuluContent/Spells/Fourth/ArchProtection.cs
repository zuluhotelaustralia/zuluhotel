using System;
using System.Collections.Generic;
using Server.Targeting;

namespace Server.Spells.Fourth
{
    public class ArchProtectionSpell : MagerySpell
    {
        private static readonly Dictionary<Mobile, int> Table = new();

        public ArchProtectionSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
        {
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

                if (map != null)
                {
                    IPooledEnumerable eable = map.GetMobilesInRange(new Point3D(p), 3);

                    foreach (Mobile m in eable)
                        if (Caster.CanBeBeneficial(m, false))
                            targets.Add(m);

                    eable.Free();
                }

                Effects.PlaySound(new Point3D(p), Caster.Map, 0x299);

                var val = (int) (Caster.Skills[SkillName.Magery].Value / 10.0 + 1);
                
                var time = Caster.Skills[SkillName.Magery].Value * 1.2;
                var duration = TimeSpan.FromSeconds(time > 144 ? 144 : time);

                if (targets.Count > 0)
                    foreach (var m in targets)
                        Buff(Caster, m, val, duration);
            }

            FinishSequence();
        }

        public static bool Buff(Mobile caster, Mobile m, int value, TimeSpan duration)
        {
            if (!m.BeginAction(typeof(ArchProtectionSpell)))
                return false;

            caster.DoBeneficial(m);
            m.VirtualArmorMod += value;

            AddEntry(m, value);
            new InternalTimer(m, duration).Start();

            m.FixedParticles(0x375A, 9, 20, 5027, EffectLayer.Waist);
            m.PlaySound(0x1F7);

            return true;
        }

        private static void AddEntry(Mobile m, int v)
        {
            Table[m] = v;
        }

        public static void RemoveEntry(Mobile m)
        {
            if (Table.ContainsKey(m))
            {
                var v = Table[m];
                Table.Remove(m);
                m.EndAction(typeof(ArchProtectionSpell));
                m.VirtualArmorMod -= v;
                if (m.VirtualArmorMod < 0)
                    m.VirtualArmorMod = 0;
            }
        }

        private class InternalTimer : Timer
        {
            private readonly Mobile m_Owner;

            public InternalTimer(Mobile target, TimeSpan delay) : base(delay)
            {
                Priority = TimerPriority.OneSecond;
                m_Owner = target;
            }

            protected override void OnTick()
            {
                RemoveEntry(m_Owner);
            }
        }

        public class InternalTarget : Target
        {
            private readonly ArchProtectionSpell m_Owner;

            public InternalTarget(ArchProtectionSpell owner) : base(12, true, TargetFlags.None)
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