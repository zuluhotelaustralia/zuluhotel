using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    public class Farmer : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new();
        protected override List<SBInfo> SBInfos => m_SBInfos;


        [Constructible]
        public Farmer() : base("the Farmer")
        {
            SetSkill(SkillName.Fishing, 120.0);
            SetSkill(SkillName.Cooking, 90.0);
            SetSkill(SkillName.Alchemy, 80.0);
        }

        public override void InitSBInfo()
        {
            m_SBInfos.Add(new SBFarmer());
        }

        public override VendorShoeType ShoeType => VendorShoeType.ThighBoots;

        public override int GetShoeHue()
        {
            return 0;
        }

        public override void InitOutfit()
        {
            base.InitOutfit();

            AddItem(new WideBrimHat(Utility.RandomNeutralHue()));
        }

        [Constructible]
        public Farmer(Serial serial) : base(serial)
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