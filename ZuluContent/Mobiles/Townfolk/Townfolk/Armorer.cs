using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    public class Armorer : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new();
        protected override List<SBInfo> SBInfos => m_SBInfos;


        [Constructible]
        public Armorer() : base("the Armorer")
        {
            SetSkill(SkillName.ArmsLore, 78.0);
            SetSkill(SkillName.Blacksmith, 90.0);
            SetSkill(SkillName.Swords, 120.0);
            SetSkill(SkillName.Tactics, 100.0);
            SetSkill(SkillName.ItemID, 90.0);
        }

        public override void InitSBInfo()
        {
            switch (Utility.Random(4))
            {
                case 0:
                {
                    m_SBInfos.Add(new SBLeatherArmor());
                    m_SBInfos.Add(new SBStuddedArmor());
                    m_SBInfos.Add(new SBMetalShields());
                    m_SBInfos.Add(new SBPlateArmor());
                    m_SBInfos.Add(new SBHelmetArmor());
                    m_SBInfos.Add(new SBChainmailArmor());
                    m_SBInfos.Add(new SBRingmailArmor());
                    break;
                }
                case 1:
                {
                    m_SBInfos.Add(new SBStuddedArmor());
                    m_SBInfos.Add(new SBLeatherArmor());
                    m_SBInfos.Add(new SBMetalShields());
                    m_SBInfos.Add(new SBHelmetArmor());
                    break;
                }
                case 2:
                {
                    m_SBInfos.Add(new SBMetalShields());
                    m_SBInfos.Add(new SBPlateArmor());
                    m_SBInfos.Add(new SBHelmetArmor());
                    m_SBInfos.Add(new SBChainmailArmor());
                    m_SBInfos.Add(new SBRingmailArmor());
                    break;
                }
                case 3:
                {
                    m_SBInfos.Add(new SBMetalShields());
                    m_SBInfos.Add(new SBHelmetArmor());
                    break;
                }
            }
        }

        public override VendorShoeType ShoeType => VendorShoeType.Boots;

        public override void InitOutfit()
        {
            base.InitOutfit();

            AddItem(new HalfApron(Utility.RandomYellowHue()));
            AddItem(new Bascinet());
        }

        [Constructible]
        public Armorer(Serial serial) : base(serial)
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