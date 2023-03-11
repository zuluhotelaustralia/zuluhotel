using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    public class TavernKeeper : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new();
        protected override List<SBInfo> SBInfos => m_SBInfos;


        [Constructible]
        public TavernKeeper() : base("the Tavern Keeper")
        {
        }

        public override void InitSBInfo()
        {
            m_SBInfos.Add(new SBTavernKeeper());
            if (zuluStyleSell)
                m_SBInfos.Add(new SAll());
        }

        public override void InitOutfit()
        {
            base.InitOutfit();

            AddItem(new HalfApron());
        }

        [Constructible]
        public TavernKeeper(Serial serial) : base(serial)
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