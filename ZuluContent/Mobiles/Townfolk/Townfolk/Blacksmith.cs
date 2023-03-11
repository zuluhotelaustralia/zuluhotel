using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    public class Blacksmith : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new();
        protected override List<SBInfo> SBInfos => m_SBInfos;

        public override NpcGuild NpcGuild => NpcGuild.BlacksmithsGuild;


        [Constructible]
        public Blacksmith() : base("the Blacksmith")
        {
            SetSkill(SkillName.Blacksmith, 90.0);
        }

        public override void InitSBInfo()
        {
            m_SBInfos.Add(new SBBlacksmith());
            if(zuluStyleSell)
                m_SBInfos.Add(new SAll());
        }

        public override VendorShoeType ShoeType => VendorShoeType.None;

        public override void InitOutfit()
        {
            base.InitOutfit();

            Item item = Utility.RandomBool() ? null : new RingmailChest();

            if (item != null && !EquipItem(item))
            {
                item.Delete();
                item = null;
            }

            if (item == null)
                AddItem(new FullApron());

            AddItem(new Bascinet());
            AddItem(new SmithHammer());
        }

        [Constructible]
        public Blacksmith(Serial serial) : base(serial)
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