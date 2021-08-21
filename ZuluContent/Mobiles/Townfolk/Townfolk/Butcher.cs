using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    public class Butcher : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new();
        protected override List<SBInfo> SBInfos => m_SBInfos;


        [Constructible]
        public Butcher() : base("the Butcher")
        {
            SetSkill(SkillName.Cooking, 60.0);
            SetSkill(SkillName.TasteID, 90.0);
        }

        public override void InitSBInfo()
        {
            m_SBInfos.Add(new SBButcher());
        }

        public override void InitOutfit()
        {
            base.InitOutfit();

            AddItem(new HalfApron());
            AddItem(new Cleaver());
        }

        [Constructible]
        public Butcher(Serial serial) : base(serial)
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