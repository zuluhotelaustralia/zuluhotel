using System;
using System.Collections.Generic;
using Server.Engines.Harvest;
using Server.Network;

namespace Server.Items
{
    public interface IAxe
    {
        bool Axe(Mobile from, BaseAxe axe);
    }

    public abstract class BaseAxe : BaseMeleeWeapon
    {
        public override int DefaultHitSound => 0x23B;
        public override int DefaultMissSound => 0x239;

        public override SkillName DefaultSkill => SkillName.Swords;

        public override WeaponType DefaultWeaponType => WeaponType.Axe;

        public override WeaponAnimation DefaultAnimation => WeaponAnimation.Slash2H;

        public virtual HarvestSystem HarvestSystem => Lumberjacking.System;

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

        public virtual int GetUsesScalar()
        {
            if (Mark == MarkQuality.Exceptional)
                return 200;

            return 100;
        }

        public override void UnscaleDurability()
        {
            base.UnscaleDurability();

            int scale = GetUsesScalar();

            m_UsesRemaining = (m_UsesRemaining * 100 + (scale - 1)) / scale;
        }

        public override void ScaleDurability()
        {
            base.ScaleDurability();

            int scale = GetUsesScalar();

            m_UsesRemaining = (m_UsesRemaining * scale + 99) / 100;
        }

        public BaseAxe(int itemID) : base(itemID)
        {
            m_UsesRemaining = 150;
        }

        public BaseAxe(Serial serial) : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (HarvestSystem == null || Deleted)
                return;

            Point3D loc = GetWorldLocation();

            if (!from.InLOS(loc) || !from.InRange(loc, 2))
            {
                from.LocalOverheadMessage(MessageType.Regular, 0x3E9, 1019045); // I can't reach that
                return;
            }
            
            if (!IsAccessibleTo(from))
            {
                PublicOverheadMessage(MessageType.Regular, 0x3E9, 1061637); // You are not allowed to access this.
                return;
            }

            if (HarvestSystem is not Mining)
                from.SendLocalizedMessage(1010018); // What do you want to use this item on?

            HarvestSystem.BeginHarvesting(from, this);
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
    }
}