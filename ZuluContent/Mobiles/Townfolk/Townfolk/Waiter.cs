using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    public class Waiter : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new();
        protected override List<SBInfo> SBInfos => m_SBInfos;


        [Constructible]
        public Waiter() : base("the Waiter")
        {
            SetSkill(SkillName.ItemID, 80.0);
            SetSkill(SkillName.ArmsLore, 95.0);
            SetSkill(SkillName.Tactics, 50.0);
        }

        public override void InitSBInfo()
        {
            m_SBInfos.Add(new SBWaiter());
            if (zuluStyleSell)
                m_SBInfos.Add(new SAll());
        }

        public override void InitOutfit()
        {
            base.InitOutfit();

            AddItem(new HalfApron());
        }

        [Constructible]
        public Waiter(Serial serial) : base(serial)
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