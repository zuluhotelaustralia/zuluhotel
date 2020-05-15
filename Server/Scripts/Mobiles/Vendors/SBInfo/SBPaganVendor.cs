using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    public class SBPaganVendor : SBInfo
    {
        private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
        private IShopSellInfo m_SellInfo = new InternalSellInfo();

        public SBPaganVendor()
        {
        }

        public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
        public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

        public class InternalBuyInfo : List<GenericBuyInfo>
        {
            public InternalBuyInfo()
            {
                Add(new GenericBuyInfo(typeof(Spellbook), 18, 10, 0xEFA, 0));

                if (Core.BetaTest)
                {
                    Add(new GenericBuyInfo(typeof(NecromancerSpellbook), 115, 10, 0x2253, 0));
                    Add(new GenericBuyInfo(typeof(SpellweavingBook), 120, 10, 0x2D50, 0));
                }

                Add(new GenericBuyInfo(typeof(BlackPearl), 5, 99999, 0xF7A, 0));
                Add(new GenericBuyInfo(typeof(Bloodmoss), 5, 99999, 0xF7B, 0));
                Add(new GenericBuyInfo(typeof(Garlic), 3, 99999, 0xF84, 0));
                Add(new GenericBuyInfo(typeof(Ginseng), 3, 99999, 0xF85, 0));
                Add(new GenericBuyInfo(typeof(MandrakeRoot), 3, 99999, 0xF86, 0));
                Add(new GenericBuyInfo(typeof(Nightshade), 3, 99999, 0xF88, 0));
                Add(new GenericBuyInfo(typeof(SpidersSilk), 3, 99999, 0xF8D, 0));
                Add(new GenericBuyInfo(typeof(SulfurousAsh), 3, 99999, 0xF8C, 0));
                Add(new GenericBuyInfo(typeof(BatWing), 3, 99999, 0xF78, 0));
                Add(new GenericBuyInfo(typeof(PigIron), 5, 99999, 0xF8A, 0));
                Add(new GenericBuyInfo(typeof(Blackmoor), 5, 99999, 0xF79, 0));
                Add(new GenericBuyInfo(typeof(Bloodspawn), 5, 99999, 0xF7C, 0));
                Add(new GenericBuyInfo(typeof(Brimstone), 5, 99999, 0xF7F, 0));
                Add(new GenericBuyInfo(typeof(DaemonBone), 5, 99999, 0xF80, 0));
                Add(new GenericBuyInfo(typeof(DragonsBlood), 5, 99999, 0xF82, 0));
                Add(new GenericBuyInfo(typeof(EyeOfNewt), 5, 99999, 0xF87, 0));
                Add(new GenericBuyInfo(typeof(Obsidian), 5, 99999, 0xF89, 0));
                Add(new GenericBuyInfo(typeof(Pumice), 5, 99999, 0xF8B, 0));
                Add(new GenericBuyInfo(typeof(NoxCrystal), 5, 99999, 0xF8E, 0));
                Add(new GenericBuyInfo(typeof(VialOfBlood), 5, 99999, 0xF7D, 0));
                Add(new GenericBuyInfo(typeof(VolcanicAsh), 5, 99999, 0xF8F, 0));
                Add(new GenericBuyInfo(typeof(WyrmsHeart), 5, 99999, 0xF91, 0));
                Add(new GenericBuyInfo(typeof(ExecutionersCap), 5, 99999, 0xF83, 0));
                Add(new GenericBuyInfo(typeof(Bone), 5, 99999, 0xF7E, 0));
                Add(new GenericBuyInfo(typeof(DeadWood), 5, 99999, 0xF90, 0));
                Add(new GenericBuyInfo(typeof(FertileDirt), 5, 99999, 0xF81, 0));

                Type[] types = Loot.RegularScrollTypes;

                int circles = 3;

                for (int i = 0; i < circles * 8 && i < types.Length; ++i)
                {
                    int itemID = 0x1F2E + i;

                    if (i == 6)
                        itemID = 0x1F2D;
                    else if (i > 6)
                        --itemID;

                    Add(new GenericBuyInfo(types[i], 12 + ((i / 8) * 10), 20, itemID, 0));
                }
            }
        }

        public class InternalSellInfo : GenericSellInfo
        {
            public InternalSellInfo()
            {
                Add(typeof(WizardsHat), 15);
                Add(typeof(BlackPearl), 3);
                Add(typeof(Bloodmoss), 4);
                Add(typeof(MandrakeRoot), 2);
                Add(typeof(Garlic), 2);
                Add(typeof(Ginseng), 2);
                Add(typeof(Nightshade), 2);
                Add(typeof(SpidersSilk), 2);
                Add(typeof(SulfurousAsh), 2);

                if (Core.AOS)
                {
                    Add(typeof(BatWing), 1);
                    Add(typeof(DaemonBlood), 3);
                    Add(typeof(PigIron), 2);
                    Add(typeof(NoxCrystal), 3);
                    //Add( typeof( GraveDust ), 1 );
                }

                Add(typeof(RecallRune), 13);
                Add(typeof(Spellbook), 25);

                Type[] types = Loot.RegularScrollTypes;

                for (int i = 0; i < types.Length; ++i)
                    Add(types[i], ((i / 8) + 2) * 2);

                if (Core.SE)
                {
                    // Add( typeof( ExorcismScroll ), 3 );
                    // Add( typeof( AnimateDeadScroll ), 8 );
                    // Add( typeof( BloodOathScroll ), 8 );
                    // Add( typeof( CorpseSkinScroll ), 8 );
                    // Add( typeof( CurseWeaponScroll ), 8 );
                    // Add( typeof( EvilOmenScroll ), 8 );
                    // Add( typeof( PainSpikeScroll ), 8 );
                    // Add( typeof( SummonFamiliarScroll ), 8 );
                    // Add( typeof( HorrificBeastScroll ), 8 );
                    // Add( typeof( MindRotScroll ), 10 );
                    // Add( typeof( PoisonStrikeScroll ), 10 );
                    // Add( typeof( WraithFormScroll ), 15 );
                    // Add( typeof( LichFormScroll ), 16 );
                    // Add( typeof( StrangleScroll ), 16 );
                    // Add( typeof( WitherScroll ), 16 );
                    // Add( typeof( VampiricEmbraceScroll ), 20 );
                    // Add( typeof( VengefulSpiritScroll ), 20 );
                }
            }
        }
    }
}
