using System.Collections.Generic;

namespace Server.Mobiles
{
    public class Baker : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new();
        protected override List<SBInfo> SBInfos => m_SBInfos;


        [Constructible]
        public Baker() : base("the Baker")
        {
            SetSkill(SkillName.Cooking, 90.0);
            SetSkill(SkillName.TasteID, 60.0);
        }

        public override void InitSBInfo()
        {
            m_SBInfos.Add(new SBBaker());
            if (zuluStyleSell)
                m_SBInfos.Add(new SAll());
        }

        [Constructible]
        public Baker(Serial serial) : base(serial)
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