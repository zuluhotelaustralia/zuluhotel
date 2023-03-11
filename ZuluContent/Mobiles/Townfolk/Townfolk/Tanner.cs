using System.Collections.Generic;

namespace Server.Mobiles
{
    public class Tanner : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new();
        protected override List<SBInfo> SBInfos => m_SBInfos;


        [Constructible]
        public Tanner() : base("the Tanner")
        {
            SetSkill(SkillName.Tailoring, 90.0);
            SetSkill(SkillName.Swords, 45.0);
            SetSkill(SkillName.Tactics, 50.0);
        }

        public override void InitSBInfo()
        {
            m_SBInfos.Add(new SBTanner());
            if (zuluStyleSell)
                m_SBInfos.Add(new SAll());
        }

        [Constructible]
        public Tanner(Serial serial) : base(serial)
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