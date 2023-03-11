using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    public class Miner : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new();
        protected override List<SBInfo> SBInfos => m_SBInfos;


        [Constructible]
        public Miner() : base("the Miner")
        {
            SetSkill(SkillName.Mining, 70.0);
        }

        public override void InitSBInfo()
        {
            m_SBInfos.Add(new SBMiner());
            if (zuluStyleSell)
                m_SBInfos.Add(new SAll());
        }

        public override void InitOutfit()
        {
            base.InitOutfit();

            AddItem(new FancyShirt(0x3E4));
            AddItem(new LongPants(0x192));
            AddItem(new Pickaxe());
            AddItem(new ThighBoots());
        }

        [Constructible]
        public Miner(Serial serial) : base(serial)
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