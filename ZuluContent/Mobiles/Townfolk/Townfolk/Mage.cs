using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    public class Mage : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new();
        protected override List<SBInfo> SBInfos => m_SBInfos;

        public override NpcGuild NpcGuild => NpcGuild.MagesGuild;


        [Constructible]
        public Mage() : base("the Mage")
        {
            SetSkill(SkillName.EvalInt, 50.0);
            SetSkill(SkillName.Inscribe, 50.0);
            SetSkill(SkillName.Magery, 90.0);
            SetSkill(SkillName.Meditation, 90.0);
            SetSkill(SkillName.MagicResist, 70.0);
            SetSkill(SkillName.Tactics, 50.0);
            SetSkill(SkillName.Stealth, 60.0);
        }

        public override void InitSBInfo()
        {
            m_SBInfos.Add(new SBMage());
        }

        public override VendorShoeType ShoeType => Utility.RandomBool() ? VendorShoeType.Shoes : VendorShoeType.Sandals;

        public override void InitOutfit()
        {
            base.InitOutfit();

            AddItem(new Robe(Utility.RandomBlueHue()));
        }

        [Constructible]
        public Mage(Serial serial) : base(serial)
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