using System;
using System.Collections.Generic;
using Server.Gumps;
using Server.Items;

namespace Server.Mobiles
{
    public abstract class BasePriest : BaseVendor
    {
        protected override List<SBInfo> SBInfos { get; } = new();

        public override bool IsActiveVendor => false;
        public override bool IsInvulnerable => false;

        public override void InitSBInfo()
        {
        }

        public BasePriest() : base(null)
        {
            if (!IsInvulnerable)
            {
                AI = AIType.AI_Mage;
                ActiveSpeed = 0.2;
                PassiveSpeed = 0.8;
                RangePerception = DefaultRangePerception;
                FightMode = FightMode.Aggressor;
            }

            SpeechHue = 0;

            SetStr(304, 400);
            SetDex(102, 150);
            SetInt(204, 300);

            SetDamage(10, 23);

            SetSkill(SkillName.Anatomy, 75.0, 97.5);
            SetSkill(SkillName.EvalInt, 82.0, 100.0);
            SetSkill(SkillName.Healing, 75.0, 97.5);
            SetSkill(SkillName.Magery, 82.0, 100.0);
            SetSkill(SkillName.MagicResist, 82.0, 100.0);
            SetSkill(SkillName.Tactics, 82.0, 100.0);

            Fame = 1000;
            Karma = 10000;

            PackItem(new Bandage(Utility.RandomMinMax(5, 10)));
            PackItem(new HealPotion());
            PackItem(new CurePotion());
        }

        public override VendorShoeType ShoeType => VendorShoeType.Sandals;

        public virtual int GetRobeColor()
        {
            return Utility.RandomYellowHue();
        }

        public override void InitOutfit()
        {
            base.InitOutfit();

            AddItem(new Robe(GetRobeColor()));
        }

        public virtual bool HealsYoungPlayers => true;

        public virtual bool CheckResurrect(Mobile m)
        {
            return true;
        }

        private DateTime m_NextResurrect;
        private static readonly TimeSpan ResurrectDelay = TimeSpan.FromSeconds(2.0);

        public virtual void OfferResurrection(Mobile m)
        {
            Direction = GetDirectionTo(m);

            m.PlaySound(0x1F2);
            m.FixedEffect(0x376A, 10, 16);

            m.CloseGump<ResurrectGump>();
            m.SendGump(new ResurrectGump(m, ResurrectMessage.Healer));
        }

        public virtual void OfferHeal(PlayerMobile m)
        {
            Direction = GetDirectionTo(m);

            if (m.CheckYoungHealTime())
            {
                Say(501229); // You look like you need some healing my child.

                m.PlaySound(0x1F2);
                m.FixedEffect(0x376A, 9, 32);

                m.Hits = m.HitsMax;
            }
            else
            {
                Say(501228); // I can do no more for you at this time.
            }
        }

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            if (!m.Frozen && DateTime.Now >= m_NextResurrect && InRange(m, 4) && !InRange(oldLocation, 4) && InLOS(m))
            {
                if (!m.Alive)
                {
                    m_NextResurrect = DateTime.Now + ResurrectDelay;

                    if (m.Map == null || !m.Map.CanFit(m.Location, 16, false, false))
                        m.SendLocalizedMessage(502391); // Thou can not be resurrected there!
                    else if (CheckResurrect(m)) OfferResurrection(m);
                }
                else if (HealsYoungPlayers && m.Hits < m.HitsMax && m is PlayerMobile && ((PlayerMobile)m).Young)
                {
                    OfferHeal((PlayerMobile)m);
                }
            }
        }

        public BasePriest(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            if (!IsInvulnerable)
            {
                AI = AIType.AI_Mage;
                ActiveSpeed = 0.2;
                PassiveSpeed = 0.8;
                RangePerception = DefaultRangePerception;
                FightMode = FightMode.Aggressor;
            }
        }
    }
}