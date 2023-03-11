using System.Collections.Generic;

namespace Server.Mobiles
{
    public class Provisioner : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new();
        protected override List<SBInfo> SBInfos => m_SBInfos;


        [Constructible]
        public Provisioner() : base("the Provisioner")
        {
            SetSkill(SkillName.Hiding, 40.0);
            SetSkill(SkillName.Swords, 50.0);
            SetSkill(SkillName.Mining, 30.0);
            SetSkill(SkillName.Healing, 60.0);
            SetSkill(SkillName.Tactics, 50.0);
            SetSkill(SkillName.ItemID, 60.0);
        }

        public override void InitSBInfo()
        {
            m_SBInfos.Add(new SBProvisioner());
            if (zuluStyleSell)
                m_SBInfos.Add(new SAll());
        }

        [Constructible]
        public Provisioner(Serial serial) : base(serial)
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