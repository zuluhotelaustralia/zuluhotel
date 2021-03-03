using System;
using System.Collections;
using Server.Items;
using Server.Mobiles;
using Server.Spells.Seventh;

namespace Server.Spells.Fifth
{
    public class IncognitoSpell : MagerySpell
    {
        private static readonly Hashtable m_Timers = new Hashtable();

        private static int[] m_HairIDs =
        {
            0x2044, 0x2045, 0x2046,
            0x203C, 0x203B, 0x203D,
            0x2047, 0x2048, 0x2049,
            0x204A, 0x0000
        };

        private static int[] m_BeardIDs =
        {
            0x203E, 0x203F, 0x2040,
            0x2041, 0x204B, 0x204C,
            0x204D, 0x0000
        };

        public IncognitoSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
        {
        }


        public override bool CanCast()
        {
            if (!base.CanCast())
                return false;
            
            if (!Caster.CanBeginAction(typeof(IncognitoSpell)))
            {
                Caster.SendLocalizedMessage(1005559); // This spell is already in effect.
                return false;
            }

            if (Caster.BodyMod == 183 || Caster.BodyMod == 184)
            {
                Caster.SendLocalizedMessage(1042402); // You cannot use incognito while wearing body paint
                return false;
            }

            return true;
        }

        public override void OnCast()
        {
            if (!Caster.CanBeginAction(typeof(IncognitoSpell)))
            {
                Caster.SendLocalizedMessage(1005559); // This spell is already in effect.
            }
            else if (Caster.BodyMod == 183 || Caster.BodyMod == 184)
            {
                Caster.SendLocalizedMessage(1042402); // You cannot use incognito while wearing body paint
            }
            else if (DisguiseTimers.IsDisguised(Caster))
            {
                Caster.SendLocalizedMessage(1061631); // You can't do that while disguised.
            }
            else if (!Caster.CanBeginAction(typeof(PolymorphSpell)) || Caster.IsBodyMod)
            {
                DoFizzle();
            }
            else if (CheckSequence())
            {
                if (Caster.BeginAction(typeof(IncognitoSpell)))
                {
                    DisguiseTimers.StopTimer(Caster);

                    Caster.HueMod = Caster.Race.RandomSkinHue();
                    Caster.NameMod = Caster.Female ? NameList.RandomName("female") : NameList.RandomName("male");

                    var pm = Caster as PlayerMobile;

                    if (pm != null && pm.Race != null)
                    {
                        pm.SetHairMods(pm.Race.RandomHair(pm.Female), pm.Race.RandomFacialHair(pm.Female));
                        pm.HairHue = pm.Race.RandomHairHue();
                        pm.FacialHairHue = pm.Race.RandomHairHue();
                    }

                    Caster.FixedParticles(0x373A, 10, 15, 5036, EffectLayer.Head);
                    Caster.PlaySound(0x3BD);

                    BaseArmor.ValidateMobile(Caster);
                    BaseClothing.ValidateMobile(Caster);

                    StopTimer(Caster);


                    var timeVal = 6 * Caster.Skills.Magery.Fixed / 50 + 1;

                    if (timeVal > 144)
                        timeVal = 144;

                    var length = TimeSpan.FromSeconds(timeVal);


                    Timer t = new InternalTimer(Caster, length);

                    m_Timers[Caster] = t;

                    t.Start();
                }
                else
                {
                    Caster.SendLocalizedMessage(1079022); // You're already incognitoed!
                }
            }

            FinishSequence();
        }

        public static bool StopTimer(Mobile m)
        {
            var t = (Timer) m_Timers[m];

            if (t != null)
            {
                t.Stop();
                m_Timers.Remove(m);
            }

            return t != null;
        }

        private class InternalTimer : Timer
        {
            private readonly Mobile m_Owner;

            public InternalTimer(Mobile owner, TimeSpan length) : base(length)
            {
                m_Owner = owner;

                /*
                int val = ((6 * owner.Skills.Magery.Fixed) / 50) + 1;

                if ( val > 144 )
                    val = 144;

                Delay = TimeSpan.FromSeconds( val );
                 * */
                Priority = TimerPriority.OneSecond;
            }

            protected override void OnTick()
            {
                if (!m_Owner.CanBeginAction(typeof(IncognitoSpell)))
                {
                    if (m_Owner is PlayerMobile)
                        ((PlayerMobile) m_Owner).SetHairMods(-1, -1);

                    m_Owner.BodyMod = 0;
                    m_Owner.HueMod = -1;
                    m_Owner.NameMod = null;
                    m_Owner.EndAction(typeof(IncognitoSpell));

                    BaseArmor.ValidateMobile(m_Owner);
                    BaseClothing.ValidateMobile(m_Owner);
                }
            }
        }
    }
}