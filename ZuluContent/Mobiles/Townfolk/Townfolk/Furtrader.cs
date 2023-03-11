using System.Collections.Generic;

namespace Server.Mobiles
{
    public class Furtrader : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new();
        protected override List<SBInfo> SBInfos => m_SBInfos;


        [Constructible]
        public Furtrader() : base("the Furtrader")
        {
            SetSkill(SkillName.Camping, 55.0, 78.0);
            //SetSkill( SkillName.Alchemy, 60.0, 83.0 );
            SetSkill(SkillName.AnimalLore, 85.0, 100.0);
            SetSkill(SkillName.Cooking, 45.0, 68.0);
            SetSkill(SkillName.Tracking, 36.0, 68.0);
        }

        public override void InitSBInfo()
        {
            m_SBInfos.Add(new SBFurtrader());
            if (zuluStyleSell)
                m_SBInfos.Add(new SAll());
        }

        [Constructible]
        public Furtrader(Serial serial) : base(serial)
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