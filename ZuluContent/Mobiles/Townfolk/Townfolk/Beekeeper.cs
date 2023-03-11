using System.Collections.Generic;

namespace Server.Mobiles
{
    public class Beekeeper : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new();
        protected override List<SBInfo> SBInfos => m_SBInfos;


        [Constructible]
        public Beekeeper() : base("the Beekeeper")
        {
            SetSkill(SkillName.ArmsLore, 60.0);
            SetSkill(SkillName.Blacksmith, 90.0);
            SetSkill(SkillName.Swords, 50.0);
            SetSkill(SkillName.Tactics, 50.0);
            SetSkill(SkillName.ItemID, 80.0);
        }

        public override void InitSBInfo()
        {
            m_SBInfos.Add(new SBBeekeeper());
            if (zuluStyleSell)
                m_SBInfos.Add(new SAll());
        }

        public override VendorShoeType ShoeType => VendorShoeType.Boots;

        [Constructible]
        public Beekeeper(Serial serial) : base(serial)
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