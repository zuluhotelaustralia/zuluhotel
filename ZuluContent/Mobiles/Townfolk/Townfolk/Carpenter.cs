using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    public class Carpenter : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new();
        protected override List<SBInfo> SBInfos => m_SBInfos;

        public override NpcGuild NpcGuild => NpcGuild.TinkersGuild;


        [Constructible]
        public Carpenter() : base("the Carpenter")
        {
            SetSkill(SkillName.Carpentry, 90.0);
            SetSkill(SkillName.Lumberjacking, 50.0);
            SetSkill(SkillName.Swords, 50.0);
            SetSkill(SkillName.Tactics, 40.0);
        }

        public override void InitSBInfo()
        {
            m_SBInfos.Add(new SBStavesWeapon());
            m_SBInfos.Add(new SBCarpenter());
            m_SBInfos.Add(new SBWoodenShields());
            if (zuluStyleSell)
                m_SBInfos.Add(new SAll());
        }

        public override void InitOutfit()
        {
            base.InitOutfit();

            AddItem(new HalfApron());
        }

        [Constructible]
        public Carpenter(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            var version = reader.ReadInt();
        }
    }
}