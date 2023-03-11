using System.Collections.Generic;

namespace Server.Mobiles
{
    public class Herbalist : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new();
        protected override List<SBInfo> SBInfos => m_SBInfos;

        public override NpcGuild NpcGuild => NpcGuild.MagesGuild;


        [Constructible]
        public Herbalist() : base("the Herbalist")
        {
            SetSkill(SkillName.Alchemy, 90.0);
            SetSkill(SkillName.Inscribe, 75.0);
            SetSkill(SkillName.Magery, 40.0);
        }

        public override void InitSBInfo()
        {
            m_SBInfos.Add(new SBHerbalist());
            if (zuluStyleSell)
                m_SBInfos.Add(new SAll());
        }

        public override VendorShoeType ShoeType => Utility.RandomBool() ? VendorShoeType.Shoes : VendorShoeType.Sandals;

        [Constructible]
        public Herbalist(Serial serial) : base(serial)
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