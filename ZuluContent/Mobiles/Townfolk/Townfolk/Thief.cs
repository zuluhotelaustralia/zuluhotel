using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    public class Thief : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new();

        protected override List<SBInfo> SBInfos => m_SBInfos;


        [Constructible]
        public Thief() : base("the Thief")
        {
            SetSkill(SkillName.Tactics, 50.0);
            SetSkill(SkillName.Lockpicking, 80.0);
            SetSkill(SkillName.Snooping, 80.0);
            SetSkill(SkillName.DetectHidden, 60.0);
            SetSkill(SkillName.Hiding, 50.0);
            SetSkill(SkillName.Poisoning, 60.0);
            SetSkill(SkillName.Stealing, 60.0);
            SetSkill(SkillName.TasteID, 40.0);
            SetSkill(SkillName.Stealth, 40.0);
            SetSkill(SkillName.RemoveTrap, 50.0);
        }

        public override void InitSBInfo()
        {
            m_SBInfos.Add(new SBThief());
        }

        public override void InitOutfit()
        {
            base.InitOutfit();

            AddItem(new Shirt(Utility.RandomNeutralHue()));
            AddItem(new LongPants(Utility.RandomNeutralHue()));
            AddItem(new Dagger());
            AddItem(new ThighBoots());
        }

        [Constructible]
        public Thief(Serial serial) : base(serial)
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