using System.Collections.Generic;

namespace Server.Mobiles
{
    public class Bard : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new();
        protected override List<SBInfo> SBInfos => m_SBInfos;

        public override NpcGuild NpcGuild => NpcGuild.BardsGuild;


        [Constructible]
        public Bard() : base("the Bard")
        {
            SetSkill(SkillName.Discordance, 100.0);
            SetSkill(SkillName.Musicianship, 95.0);
            SetSkill(SkillName.Peacemaking, 100.0);
            SetSkill(SkillName.Provocation, 85.0);
            SetSkill(SkillName.Tinkering, 45.0);
        }

        public override void InitSBInfo()
        {
            m_SBInfos.Add(new SBBard());
            if (zuluStyleSell)
                m_SBInfos.Add(new SAll());
        }

        [Constructible]
        public Bard(Serial serial) : base(serial)
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