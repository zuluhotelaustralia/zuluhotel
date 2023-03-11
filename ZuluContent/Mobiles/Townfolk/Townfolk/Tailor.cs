using System.Collections.Generic;

namespace Server.Mobiles
{
    public class Tailor : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new();
        protected override List<SBInfo> SBInfos => m_SBInfos;

        public override NpcGuild NpcGuild => NpcGuild.TailorsGuild;


        [Constructible]
        public Tailor() : base("the Tailor")
        {
            SetSkill(SkillName.Tailoring, 90.0);
            SetSkill(SkillName.Fencing, 40.0);
            SetSkill(SkillName.Tactics, 40.0);
        }

        public override void InitSBInfo()
        {
            m_SBInfos.Add(new SBTailor());
            if (zuluStyleSell)
                m_SBInfos.Add(new SAll());
        }

        public override VendorShoeType ShoeType => Utility.RandomBool() ? VendorShoeType.Sandals : VendorShoeType.Shoes;

        [Constructible]
        public Tailor(Serial serial) : base(serial)
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