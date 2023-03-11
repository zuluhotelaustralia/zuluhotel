using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    public class Weaponsmith : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new();
        protected override List<SBInfo> SBInfos => m_SBInfos;


        [Constructible]
        public Weaponsmith() : base("the Weaponsmith")
        {
            SetSkill(SkillName.ArmsLore, 78.0);
            SetSkill(SkillName.Blacksmith, 90.0);
            SetSkill(SkillName.Fencing, 100.0);
            SetSkill(SkillName.Macing, 100.0);
            SetSkill(SkillName.Swords, 120.0);
            SetSkill(SkillName.Tactics, 100.0);
        }

        public override void InitSBInfo()
        {
            m_SBInfos.Add(new SBWeaponSmith());
            if (zuluStyleSell)
                m_SBInfos.Add(new SAll());
        }

        public override VendorShoeType ShoeType =>
            Utility.RandomBool() ? VendorShoeType.Boots : VendorShoeType.ThighBoots;

        public override int GetShoeHue()
        {
            return 0;
        }

        public override void InitOutfit()
        {
            base.InitOutfit();

            AddItem(new HalfApron());
        }

        [Constructible]
        public Weaponsmith(Serial serial) : base(serial)
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