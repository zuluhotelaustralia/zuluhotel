using System.Collections.Generic;

namespace Server.Mobiles
{
    public class Architect : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new();
        protected override List<SBInfo> SBInfos => m_SBInfos;

        public override NpcGuild NpcGuild => NpcGuild.TinkersGuild;


        [Constructible]
        public Architect() : base("the Architect")
        {
            SetSkill(SkillName.Cartography, 40.0);
            SetSkill(SkillName.Tactics, 50.0);
        }

        public override void InitSBInfo()
        {
            m_SBInfos.Add(new SBHouseDeed());
            m_SBInfos.Add(new SBArchitect());
            if (zuluStyleSell)
                m_SBInfos.Add(new SAll());
        }

        [Constructible]
        public Architect(Serial serial) : base(serial)
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