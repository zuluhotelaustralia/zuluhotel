using System;
using System.Collections.Generic;
using Server.Engines.Harvest;

namespace Server.Items
{
    public abstract class BasePoleArm : BaseMeleeWeapon, IUsesRemaining
    {
        public virtual HarvestSystem HarvestSystem => Lumberjacking.System;

        public override int DefaultHitSound => 0x236;
        public override int DefaultMissSound => 0x232;

        public override SkillName DefaultSkill => SkillName.Swords;

        public override WeaponType DefaultWeaponType => WeaponType.Polearm;

        public override WeaponAnimation DefaultAnimation => WeaponAnimation.Slash2H;

        private int m_UsesRemaining;
        private bool m_ShowUsesRemaining;

        [CommandProperty(AccessLevel.GameMaster)]
        public int UsesRemaining
        {
            get { return m_UsesRemaining; }
            set { m_UsesRemaining = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool ShowUsesRemaining
        {
            get { return m_ShowUsesRemaining; }
            set { m_ShowUsesRemaining = value; }
        }

        public BasePoleArm(int itemID) : base(itemID)
        {
            m_UsesRemaining = 150;
        }

        public BasePoleArm(Serial serial) : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (HarvestSystem == null)
                return;

            if (IsChildOf(from.Backpack) || Parent == from)
                HarvestSystem.BeginHarvesting(from, this);
            else
                from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 2); // version

            writer.Write((bool) m_ShowUsesRemaining);

            writer.Write((int) m_UsesRemaining);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 2:
                {
                    m_ShowUsesRemaining = reader.ReadBool();
                    goto case 1;
                }
                case 1:
                {
                    m_UsesRemaining = reader.ReadInt();
                    goto case 0;
                }
                case 0:
                {
                    if (m_UsesRemaining < 1)
                        m_UsesRemaining = 150;

                    break;
                }
            }
        }

        public override void OnHit(Mobile attacker, Mobile defender, double damageBonus)
        {
            base.OnHit(attacker, defender, damageBonus);

            if ((attacker.Player || attacker.Body.IsHuman) && Layer == Layer.TwoHanded &&
                attacker.Skills[SkillName.Anatomy].Value / 400.0 >= Utility.RandomDouble())
            {
                StatMod mod = defender.GetStatMod("Concussion");

                if (mod == null)
                {
                    defender.SendMessage("You receive a concussion blow!");
                    defender.AddStatMod(new StatMod(StatType.Int, "Concussion", -(defender.RawInt / 2),
                        TimeSpan.FromSeconds(30.0)));

                    attacker.SendMessage("You deliver a concussion blow!");
                    attacker.PlaySound(0x11C);
                }
            }
        }
    }
}