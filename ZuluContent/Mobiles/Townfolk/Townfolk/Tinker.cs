using System.Collections.Generic;

namespace Server.Mobiles
{
    public class Tinker : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new();
        protected override List<SBInfo> SBInfos => m_SBInfos;

        public override NpcGuild NpcGuild => NpcGuild.TinkersGuild;


        [Constructible]
        public Tinker() : base("the Tinker")
        {
            SetSkill(SkillName.RemoveTrap, 80.0);
            SetSkill(SkillName.Carpentry, 75.0);
            SetSkill(SkillName.Tinkering, 95.0);
        }

        public override void InitSBInfo()
        {
            m_SBInfos.Add(new SBTinker());
        }

        [Constructible]
        public Tinker(Serial serial) : base(serial)
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