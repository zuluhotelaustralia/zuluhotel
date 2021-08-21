using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    public class Ranger : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new();

        protected override List<SBInfo> SBInfos => m_SBInfos;


        [Constructible]
        public Ranger() : base("the Ranger")
        {
            SetSkill(SkillName.Fletching, 90.0);
            SetSkill(SkillName.Archery, 100.0);
            SetSkill(SkillName.Tracking, 100.0);
            SetSkill(SkillName.Tactics, 50.0);
            SetSkill(SkillName.Lumberjacking, 100.0);
            SetSkill(SkillName.AnimalTaming, 100.0);
        }

        public override void InitSBInfo()
        {
            m_SBInfos.Add(new SBRanger());
        }

        public override void InitOutfit()
        {
            base.InitOutfit();

            AddItem(new Shirt(Utility.RandomNeutralHue()));
            AddItem(new LongPants(Utility.RandomNeutralHue()));
            AddItem(new Bow());
            AddItem(new ThighBoots());
        }

        [Constructible]
        public Ranger(Serial serial) : base(serial)
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