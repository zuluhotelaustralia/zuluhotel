using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    public class Fisherman : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new();
        protected override List<SBInfo> SBInfos => m_SBInfos;

        public override NpcGuild NpcGuild => NpcGuild.FishermensGuild;


        [Constructible]
        public Fisherman() : base("the Fisher")
        {
            SetSkill(SkillName.Fishing, 120.0);
            SetSkill(SkillName.Cooking, 100.0);
        }

        public override void InitSBInfo()
        {
            m_SBInfos.Add(new SBFisherman());
            if (zuluStyleSell)
                m_SBInfos.Add(new SAll());
        }

        public override void InitOutfit()
        {
            base.InitOutfit();

            AddItem(new FishingPole());
        }

        [Constructible]
        public Fisherman(Serial serial) : base(serial)
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