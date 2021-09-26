using System.Collections.Generic;

namespace Server.Mobiles
{
    public class Weaver : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new();
        protected override List<SBInfo> SBInfos => m_SBInfos;

        public override NpcGuild NpcGuild => NpcGuild.TailorsGuild;


        [Constructible]
        public Weaver() : base("the Weaver")
        {
            SetSkill(SkillName.Tailoring, 90.0);
        }

        public override void InitSBInfo()
        {
            m_SBInfos.Add(new SBWeaver());
        }

        public override VendorShoeType ShoeType => VendorShoeType.Sandals;

        [Constructible]
        public Weaver(Serial serial) : base(serial)
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