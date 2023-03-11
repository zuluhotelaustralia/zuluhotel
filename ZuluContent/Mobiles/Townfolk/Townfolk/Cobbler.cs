using System.Collections.Generic;

namespace Server.Mobiles
{
    public class Cobbler : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new();
        protected override List<SBInfo> SBInfos => m_SBInfos;


        [Constructible]
        public Cobbler() : base("the Cobbler")
        {
            SetSkill(SkillName.Tailoring, 60.0);
        }

        public override void InitSBInfo()
        {
            m_SBInfos.Add(new SBCobbler());
            if (zuluStyleSell)
                m_SBInfos.Add(new SAll());
        }

        public override VendorShoeType ShoeType => Utility.RandomBool() ? VendorShoeType.Sandals : VendorShoeType.Shoes;

        [Constructible]
        public Cobbler(Serial serial) : base(serial)
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