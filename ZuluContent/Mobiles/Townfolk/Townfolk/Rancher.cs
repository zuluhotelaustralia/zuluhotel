using System.Collections.Generic;

namespace Server.Mobiles
{
    public class Rancher : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new();
        protected override List<SBInfo> SBInfos => m_SBInfos;


        [Constructible]
        public Rancher() : base("the Rancher")
        {
            SetSkill(SkillName.AnimalLore, 55.0, 78.0);
            SetSkill(SkillName.AnimalTaming, 55.0, 78.0);
            SetSkill(SkillName.Herding, 64.0, 100.0);
            SetSkill(SkillName.Veterinary, 60.0, 83.0);
        }

        public override void InitSBInfo()
        {
            m_SBInfos.Add(new SBRancher());
            if (zuluStyleSell)
                m_SBInfos.Add(new SAll());
        }

        [Constructible]
        public Rancher(Serial serial) : base(serial)
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