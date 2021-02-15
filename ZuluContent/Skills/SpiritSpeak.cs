using System;
using Scripts.Zulu.Engines.Classes;
using Server.Items;
using Server.Spells;
using Server.Network;

namespace Server.SkillHandlers
{
    class SpiritSpeak
    {
        public static void Initialize()
        {
            SkillInfo.Table[(int)SkillName.SpiritSpeak].Callback = OnUse;
        }

        public static TimeSpan OnUse(Mobile m)
        {
            m.RevealingAction();

            if (m.ShilCheckSkill(SkillName.SpiritSpeak))
            {
                if (!m.CanHearGhosts)
                {
                    var secs = m.Skills[SkillName.SpiritSpeak].Base / 50 * 90;
                    new SpiritSpeakTimer(m,TimeSpan.FromSeconds(secs < 15 ? 15 : secs)).Start();
                    m.CanHearGhosts = true;
                }

                m.PlaySound(0x24A);
                m.SendLocalizedMessage(502444); //You contact the netherworld.
            }
            else
            {
                m.SendLocalizedMessage(502443); //You fail to contact the netherworld.
                m.CanHearGhosts = false;
            }

            return TimeSpan.FromSeconds(1.0);
        }

        private class SpiritSpeakTimer : Timer
        {
            private readonly Mobile m_Owner;

            public SpiritSpeakTimer(Mobile m, TimeSpan duration) : base(duration)
            {
                m_Owner = m;
                Priority = TimerPriority.FiveSeconds;
            }

            protected override void OnTick()
            {
                m_Owner.CanHearGhosts = false;
                m_Owner.SendLocalizedMessage(502445); //You feel your contact with the netherworld fading.
            }
        }

        public class SpiritSpeakSpell : Spell
        {
            public SpiritSpeakSpell(Mobile caster) : base(caster, null)
            {
            }

            public override bool ClearHandsOnCast { get; } = false;
            public override double CastDelayFastScalar { get; } = 0;
            public override TimeSpan CastDelayBase { get; } = TimeSpan.FromSeconds(1.0);
            public override bool CheckNextSpellTime { get; } = false;
            
            public override int GetMana() => 0;
            public override void OnCasterHurt()
            {
                if (IsCasting)
                    Disturb(DisturbType.Hurt, false, true);
            }

            public override bool ConsumeReagents() => true;

            public override bool CheckFizzle() => true;

            public override void OnDisturb(DisturbType type, bool message)
            {
                Caster.NextSkillTime = Core.TickCount;

                base.OnDisturb(type, message);
            }

            public override bool CheckDisturb(DisturbType type, bool checkFirst, bool resistable)
            {
                if (type == DisturbType.EquipRequest || type == DisturbType.UseRequest)
                    return false;

                return true;
            }

            public override void SayMantra()
            {
                // Anh Mi Sah Ko
                Caster.PublicOverheadMessage(MessageType.Regular, 0x3B2, 1062074, "", false);
                Caster.PlaySound(0x24A);
            }

            public override void OnCast()
            {
                Corpse toChannel = null;

                foreach (var item in Caster.GetItemsInRange(3))
                {
                    if (item is Corpse {Channeled: false} corpse)
                    {
                        toChannel = corpse;
                        break;
                    }
                }

                int max, min, mana, number;

                if (toChannel != null)
                {
                    min = 1 + (int) (Caster.Skills[SkillName.SpiritSpeak].Value * 0.25);
                    max = min + 4;
                    mana = 0;
                    number = 1061287; // You channel energy from a nearby corpse to heal your wounds.
                }
                else
                {
                    min = 1 + (int) (Caster.Skills[SkillName.SpiritSpeak].Value * 0.25);
                    max = min + 4;
                    mana = 10;
                    number = 1061286; // You channel your own spiritual energy to heal your wounds.
                }

                if (Caster.Mana < mana)
                {
                    Caster.SendLocalizedMessage(1061285); // You lack the mana required to use this skill.
                }
                else
                {
                    Caster.CheckSkill(SkillName.SpiritSpeak, 0.0, 120.0);

                    if (Utility.RandomDouble() > Caster.Skills[SkillName.SpiritSpeak].Value / 100.0)
                    {
                        Caster.SendLocalizedMessage(502443); // You fail your attempt at contacting the netherworld.
                    }
                    else
                    {
                        if (toChannel != null)
                        {
                            toChannel.Channeled = true;
                            toChannel.Hue = 0x835;
                        }

                        Caster.Mana -= mana;
                        Caster.SendLocalizedMessage(number);

                        if (min > max)
                            min = max;

                        Caster.Hits += Utility.RandomMinMax(min, max);

                        Caster.FixedParticles(0x375A, 1, 15, 9501, 2100, 4, EffectLayer.Waist);
                    }
                }

                FinishSequence();
            }
        }
    }
}
