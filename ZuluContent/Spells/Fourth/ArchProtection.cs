using System;
using System.Collections.Generic;
using Server.Targeting;

namespace Server.Spells.Fourth
{
    public class ArchProtectionSpell : MagerySpell
    {
        private static readonly Dictionary<Mobile, int> _Table = new Dictionary<Mobile, int>();

        public ArchProtectionSpell(Mobile caster, Item scroll) : base(caster, scroll)
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

                Effects.PlaySound(p, Caster.Map, 0x299);

                var val = (int) (Caster.Skills[SkillName.Magery].Value / 10.0 + 1);

                if (targets.Count > 0)
                    for (var i = 0; i < targets.Count; ++i)
                    {
                        var m = targets[i];

                        if (m.BeginAction(typeof(ArchProtectionSpell)))
                        {
                            Caster.DoBeneficial(m);
                            m.VirtualArmorMod += val;

                            AddEntry(m, val);
                            new InternalTimer(m, Caster).Start();

                            m.FixedParticles(0x375A, 9, 20, 5027, EffectLayer.Waist);
                            m.PlaySound(0x1F7);
                        }
                    }
            }

            FinishSequence();
        }

        private static void AddEntry(Mobile m, int v)
        {
            _Table[m] = v;
        }

        public static void RemoveEntry(Mobile m)
        {
            if (_Table.ContainsKey(m))
            {
                var v = _Table[m];
                _Table.Remove(m);
                m.EndAction(typeof(ArchProtectionSpell));
                m.VirtualArmorMod -= v;
                if (m.VirtualArmorMod < 0)
                    m.VirtualArmorMod = 0;
            }
        }

        private class InternalTimer : Timer
        {
            private readonly Mobile m_Owner;

            public InternalTimer(Mobile target, Mobile caster) : base(TimeSpan.FromSeconds(0))
            {
                var time = caster.Skills[SkillName.Magery].Value * 1.2;
                if (time > 144)
                    time = 144;
                Delay = TimeSpan.FromSeconds(time);
                Priority = TimerPriority.OneSecond;

                m_Owner = target;
            }

            protected override void OnTick()
            {
                RemoveEntry(m_Owner);
            }
        }

        private class InternalTarget : Target
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