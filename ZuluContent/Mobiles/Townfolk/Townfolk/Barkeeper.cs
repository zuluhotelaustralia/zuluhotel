using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    public class Barkeeper : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new();
        protected override List<SBInfo> SBInfos => m_SBInfos;

        public override void InitSBInfo()
        {
            m_SBInfos.Add(new SBBarkeeper());
        }

        public override VendorShoeType ShoeType =>
            Utility.RandomBool() ? VendorShoeType.ThighBoots : VendorShoeType.Boots;

        public override void InitOutfit()
        {
            base.InitOutfit();

            AddItem(new HalfApron(Utility.RandomBrightHue()));
        }


        [Constructible]
        public Barkeeper() : base("the barkeeper")
        {
        }

        [Constructible]
        public Barkeeper(Serial serial) : base(serial)
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