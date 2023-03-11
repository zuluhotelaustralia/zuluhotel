using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    public class Shipwright : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new();
        protected override List<SBInfo> SBInfos => m_SBInfos;


        [Constructible]
        public Shipwright() : base("the Shipwright")
        {
            SetSkill(SkillName.Inscribe, 90.0);
            SetSkill(SkillName.Cartography, 60.0);
            SetSkill(SkillName.Magery, 70.0);
            SetSkill(SkillName.Tactics, 50.0);
        }

        public override void InitSBInfo()
        {
            m_SBInfos.Add(new SBShipwright());
            if (zuluStyleSell)
                m_SBInfos.Add(new SAll());
        }

        public override void InitOutfit()
        {
            base.InitOutfit();

            AddItem(new SmithHammer());
        }

        [Constructible]
        public Shipwright(Serial serial) : base(serial)
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