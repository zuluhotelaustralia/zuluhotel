using System;
using System.Collections;
using Server.Gumps;
using Server.Items;
using Server.Spells.Fifth;

namespace Server.Spells.Seventh
{
    public class PolymorphSpell : MagerySpell
    {
        public static readonly PolymorphCategory[] Categories =
        {
            new(1015235, // Animals
                PolymorphEntry.Chicken,
                PolymorphEntry.Dog,
                PolymorphEntry.Wolf,
                PolymorphEntry.Panther,
                PolymorphEntry.Gorilla,
                PolymorphEntry.BlackBear,
                PolymorphEntry.GrizzlyBear,
                PolymorphEntry.PolarBear,
                PolymorphEntry.HumanMale
            ),

            new(1015245, // Monsters
                PolymorphEntry.Slime,
                PolymorphEntry.Orc,
                PolymorphEntry.LizardMan,
                PolymorphEntry.Gargoyle,
                PolymorphEntry.Ogre,
                PolymorphEntry.Troll,
                PolymorphEntry.Ettin,
                PolymorphEntry.Daemon,
                PolymorphEntry.HumanFemale
            )
        };
        
        private static readonly Hashtable Timers = new();
        

        private readonly int m_NewBody;

        public PolymorphSpell(Mobile caster, Item scroll, int body) : base(caster, scroll)
        {
            m_NewBody = body;
        }

        public PolymorphSpell(Mobile caster, Item scroll) : this(caster, scroll, 0)
        {
        }


        public override bool CheckCast()
        {
            if (Caster.Mounted)
            {
                Caster.SendLocalizedMessage(1042561); //Please dismount first.
                return false;
            }

            if (DisguiseTimers.IsDisguised(Caster))
            {
                Caster.SendLocalizedMessage(502167); // You cannot polymorph while disguised.
                return false;
            }

            if (Caster.BodyMod == 183 || Caster.BodyMod == 184)
            {
                Caster.SendLocalizedMessage(1042512); // You cannot polymorph while wearing body paint
                return false;
            }

            if (!Caster.CanBeginAction(typeof(PolymorphSpell)))
            {
                Caster.SendLocalizedMessage(1005559); // This spell is already in effect.
                return false;
            }

            if (m_NewBody == 0)
            {
                Gump gump = new PolymorphGump(Caster, Scroll, Categories);

                Caster.SendGump(gump);
                return false;
            }

            return true;
        }

        public override void OnCast()
        {
            if (Caster.Mounted)
            {
                Caster.SendLocalizedMessage(1042561); //Please dismount first.
            }
            else if (!Caster.CanBeginAction(typeof(PolymorphSpell)))
            {
                Caster.SendLocalizedMessage(1005559); // This spell is already in effect.
            }
            else if (DisguiseTimers.IsDisguised(Caster))
            {
                Caster.SendLocalizedMessage(502167); // You cannot polymorph while disguised.
            }
            else if (Caster.BodyMod == 183 || Caster.BodyMod == 184)
            {
                Caster.SendLocalizedMessage(1042512); // You cannot polymorph while wearing body paint
            }
            else if (!Caster.CanBeginAction(typeof(IncognitoSpell)) || Caster.IsBodyMod)
            {
                DoFizzle();
            }
            else if (CheckSequence())
            {
                if (Caster.BeginAction(typeof(PolymorphSpell)))
                {
                    if (m_NewBody != 0)
                    {
                        if (!((Body) m_NewBody).IsHuman)
                        {
                            var mt = Caster.Mount;

                            if (mt != null)
                                mt.Rider = null;
                        }

                        Caster.BodyMod = m_NewBody;

                        if (m_NewBody == 400 || m_NewBody == 401)
                            Caster.HueMod = Race.DefaultRace.RandomSkinHue();
                        else
                            Caster.HueMod = 0;
                        
                        var val = (int) Caster.Skills[SkillName.Magery].Value;
                        var duration = TimeSpan.FromSeconds(val > 120 ? 120 : val);

                        BaseArmor.ValidateMobile(Caster);
                        BaseClothing.ValidateMobile(Caster);

                        Buff(Caster, m_NewBody, duration);
                    }
                }
                else
                {
                    Caster.SendLocalizedMessage(1005559); // This spell is already in effect.
                }
            }

            FinishSequence();
        }

        public static bool Buff(Mobile caster, int body, TimeSpan duration)
        {
            if (caster.BeginAction(typeof(PolymorphSpell)))
            {
                if (body != 0)
                {
                    if (!((Body) body).IsHuman)
                    {
                        var mt = caster.Mount;

                        if (mt != null)
                            mt.Rider = null;
                    }

                    caster.BodyMod = body;

                    if (body == 400 || body == 401)
                        caster.HueMod = Race.DefaultRace.RandomSkinHue();
                    else
                        caster.HueMod = 0;

                    BaseArmor.ValidateMobile(caster);
                    BaseClothing.ValidateMobile(caster);

                    StopTimer(caster);
                    
                    Timer t = new InternalTimer(caster, duration);
                    Timers[caster] = t;

                    t.Start();
                    return true;
                }
            }
            else
            {
                caster.SendLocalizedMessage(1005559); // This spell is already in effect.
            }
            return false;
        }

        public static bool StopTimer(Mobile m)
        {
            var t = (Timer) Timers[m];

            if (t != null)
            {
                t.Stop();
                Timers.Remove(m);
            }

            return t != null;
        }

        private static void EndPolymorph(Mobile m)
        {
            if (!m.CanBeginAction(typeof(PolymorphSpell)))
            {
                m.BodyMod = 0;
                m.HueMod = -1;
                m.EndAction(typeof(PolymorphSpell));

                BaseArmor.ValidateMobile(m);
                BaseClothing.ValidateMobile(m);
            }
        }

        private class InternalTimer : Timer
        {
            private readonly Mobile m_Owner;

            public InternalTimer(Mobile owner, TimeSpan duration) : base(duration)
            {
                m_Owner = owner;
                Priority = TimerPriority.OneSecond;
            }

            protected override void OnTick()
            {
                EndPolymorph(m_Owner);
            }
        }
    }
}