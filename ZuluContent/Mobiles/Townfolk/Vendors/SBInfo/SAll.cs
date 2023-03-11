using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    public class SAll : SBInfo
    {
        private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
        private IShopSellInfo m_SellInfo = new InternalSellInfo();

        public SAll()
        {
        }

        public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
        public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

        public class InternalBuyInfo : List<GenericBuyInfo>
        {
            public InternalBuyInfo()
            {
            }
        }

        public class InternalSellInfo : GenericSellInfo
        {
            public InternalSellInfo()
            {
                //Armor
                {
                    Add(typeof(Buckler), 25);
                    Add(typeof(BronzeShield), 33);
                    Add(typeof(MetalShield), 60);
                    Add(typeof(MetalKiteShield), 62);
                    Add(typeof(HeaterShield), 115);
                    Add(typeof(WoodenKiteShield), 35);
                    Add(typeof(WoodenShield), 15);

                    Add(typeof(PlateArms), 94);
                    Add(typeof(PlateChest), 121);
                    Add(typeof(PlateGloves), 72);
                    Add(typeof(PlateGorget), 52);
                    Add(typeof(PlateLegs), 109);
                    Add(typeof(FemalePlateChest), 113);

                    Add(typeof(Bascinet), 9);
                    Add(typeof(CloseHelm), 9);
                    Add(typeof(Helmet), 9);
                    Add(typeof(NorseHelm), 9);
                    Add(typeof(PlateHelm), 10);

                    Add(typeof(ChainCoif), 6);
                    Add(typeof(ChainChest), 71);
                    Add(typeof(ChainLegs), 74);

                    Add(typeof(RingmailArms), 42);
                    Add(typeof(RingmailChest), 60);
                    Add(typeof(RingmailGloves), 26);
                    Add(typeof(RingmailLegs), 45);

                    Add(typeof(LeatherArms), 18);
                    Add(typeof(LeatherChest), 23);
                    Add(typeof(LeatherGloves), 15);
                    Add(typeof(LeatherGorget), 15);
                    Add(typeof(LeatherLegs), 18);
                    Add(typeof(LeatherCap), 5);

                    Add(typeof(StuddedArms), 43);
                    Add(typeof(StuddedChest), 37);
                    Add(typeof(StuddedGloves), 39);
                    Add(typeof(StuddedGorget), 22);
                    Add(typeof(StuddedLegs), 33);

                    Add(typeof(FemaleStuddedChest), 31);
                    Add(typeof(StuddedBustierArms), 23);
                    Add(typeof(FemaleLeatherChest), 18);
                    Add(typeof(LeatherBustierArms), 12);
                    Add(typeof(LeatherShorts), 14);
                    Add(typeof(LeatherSkirt), 12);
                }

                //Clothing
                {
                    Add(typeof(Shoes), 4);
                    Add(typeof(Boots), 5);
                    Add(typeof(ThighBoots), 7);
                    Add(typeof(Sandals), 2);

                    Add(typeof(JesterHat), 6);
                    Add(typeof(FloppyHat), 3);
                    Add(typeof(WideBrimHat), 4);
                    Add(typeof(Cap), 5);
                    Add(typeof(SkullCap), 3);
                    Add(typeof(Bandana), 3);
                    Add(typeof(TallStrawHat), 4);
                    Add(typeof(StrawHat), 4);
                    Add(typeof(WizardsHat), 5);
                    Add(typeof(Bonnet), 4);
                    Add(typeof(FeatheredHat), 5);
                    Add(typeof(TricorneHat), 4);

                    Add(typeof(FancyShirt), 10);
                    Add(typeof(Shirt), 6);

                    Add(typeof(ShortPants), 3);
                    Add(typeof(LongPants), 5);

                    Add(typeof(Cloak), 4);
                    Add(typeof(FancyDress), 12);
                    Add(typeof(Robe), 9);
                    Add(typeof(PlainDress), 7);

                    Add(typeof(Skirt), 5);
                    Add(typeof(Kilt), 5);

                    Add(typeof(Doublet), 7);
                    Add(typeof(Tunic), 9);
                    Add(typeof(JesterSuit), 13);

                    Add(typeof(FullApron), 5);
                    Add(typeof(HalfApron), 5);
                }

                //Farming
                {
                    Add(typeof(CabbageSeed), 1);
                    Add(typeof(CarrotSeed), 1);
                    Add(typeof(CottonSeed), 1);
                    Add(typeof(FlaxSeed), 1);
                    Add(typeof(LettuceSeed), 1);
                    Add(typeof(OnionSeed), 1);
                    Add(typeof(PumpkinSeed), 1);
                    Add(typeof(TurnipSeed), 1);
                    Add(typeof(WheatSeed), 1);
                    Add(typeof(GarlicSeed), 1);
                    Add(typeof(MandrakeSeed), 1);
                    Add(typeof(NightshadeSeed), 1);
                    Add(typeof(GinsengSeed), 1);
                    Add(typeof(Apple), 1);
                    Add(typeof(Banana), 1);
                    Add(typeof(Grapes), 1);
                    Add(typeof(Watermelon), 3);
                    Add(typeof(YellowGourd), 1);
                    Add(typeof(GreenGourd), 1);
                    Add(typeof(Pumpkin), 5);
                    Add(typeof(Onion), 1);
                    Add(typeof(Lettuce), 2);
                    Add(typeof(Squash), 1);
                    Add(typeof(Carrot), 1);
                    Add(typeof(HoneydewMelon), 3);
                    Add(typeof(Cantaloupe), 3);
                    Add(typeof(Cabbage), 2);
                    Add(typeof(Lemon), 1);
                    Add(typeof(Lime), 1);
                    Add(typeof(Peach), 1);
                    Add(typeof(Pear), 1);
                    Add(typeof(SheafOfHay), 1);
                }

                //Fish
                {
                    Add(typeof(RawFishSteak), 1);
                    Add(typeof(Fish), 1);
                    //TODO: Add( typeof( SmallFish ), 1 );
                    Add(typeof(FishingPole), 7);
                }

                //Food
                {
                    Add(typeof(CookedBird), 8);
                    Add(typeof(RoastPig), 53);
                    Add(typeof(ChickenLeg), 3);
                    Add(typeof(LambLeg), 4);
                    Add(typeof(BreadLoaf), 3);
                    Add(typeof(FrenchBread), 1);
                    Add(typeof(Cake), 5);
                    Add(typeof(Cookies), 3);
                    Add(typeof(Muffins), 2);
                    Add(typeof(CheesePizza), 4);
                    Add(typeof(ApplePie), 5);
                    Add(typeof(PeachCobbler), 5);
                    Add(typeof(Quiche), 6);
                    Add(typeof(Dough), 4);
                    Add(typeof(JarHoney), 1);
                    Add(typeof(SackFlour), 1);
                    Add(typeof(Eggs), 1);
                    Add(typeof(WoodenBowlOfCarrots), 1);
                    Add(typeof(WoodenBowlOfCorn), 1);
                    Add(typeof(WoodenBowlOfLettuce), 1);
                    Add(typeof(WoodenBowlOfPeas), 1);
                    Add(typeof(EmptyPewterBowl), 1);
                    Add(typeof(PewterBowlOfCorn), 1);
                    Add(typeof(PewterBowlOfLettuce), 1);
                    Add(typeof(PewterBowlOfPeas), 1);
                    Add(typeof(PewterBowlOfPotatos), 1);
                    Add(typeof(WoodenBowlOfStew), 1);
                    Add(typeof(WoodenBowlOfTomatoSoup), 1);
                    Add(typeof(BeverageBottle), 3);
                    Add(typeof(Jug), 6);
                    Add(typeof(Pitcher), 5);
                    Add(typeof(GlassMug), 1);
                    Add(typeof(BreadLoaf), 3);
                    Add(typeof(CheeseWheel), 12);
                    Add(typeof(Ribs), 6);
                    Add(typeof(RawRibs), 8);
                    Add(typeof(RawLambLeg), 4);
                    Add(typeof(RawChickenLeg), 3);
                    Add(typeof(RawBird), 4);
                    Add(typeof(Bacon), 3);
                    Add(typeof(Sausage), 9);
                    Add(typeof(Ham), 13);
                }

                //Furniture
                {
                    Add(typeof(WoodenBox), 7);
                    Add(typeof(SmallCrate), 5);
                    Add(typeof(MediumCrate), 6);
                    Add(typeof(LargeCrate), 7);
                    Add(typeof(WoodenChest), 15);

                    Add(typeof(LargeTable), 10);
                    Add(typeof(Nightstand), 7);
                    Add(typeof(YewWoodTable), 10);

                    Add(typeof(Throne), 24);
                    Add(typeof(WoodenThrone), 6);
                    Add(typeof(Stool), 6);
                    Add(typeof(FootStool), 6);

                    Add(typeof(FancyWoodenChairCushion), 12);
                    Add(typeof(WoodenChairCushion), 10);
                    Add(typeof(WoodenChair), 8);
                    Add(typeof(BambooChair), 6);
                    Add(typeof(WoodenBench), 6);
                }

                //Gems & Jewelry
                {
                    Add(typeof(Amber), 25);
                    Add(typeof(Amethyst), 50);
                    Add(typeof(Citrine), 25);
                    Add(typeof(Diamond), 100);
                    Add(typeof(Emerald), 50);
                    Add(typeof(Ruby), 37);
                    Add(typeof(Sapphire), 50);
                    Add(typeof(StarSapphire), 62);
                    Add(typeof(Tourmaline), 47);
                    Add(typeof(GoldRing), 13);
                    Add(typeof(SilverRing), 10);
                    Add(typeof(Necklace), 13);
                    Add(typeof(GoldNecklace), 13);
                    Add(typeof(GoldBeadNecklace), 13);
                    Add(typeof(SilverNecklace), 10);
                    Add(typeof(SilverBeadNecklace), 10);
                    Add(typeof(Beads), 13);
                    Add(typeof(GoldBracelet), 13);
                    Add(typeof(SilverBracelet), 10);
                    Add(typeof(GoldEarrings), 13);
                    Add(typeof(SilverEarrings), 10);
                }

                //House deeds
                {
                    /*Add( typeof( StonePlasterHouseDeed ), 43800 );
                    Add( typeof( FieldStoneHouseDeed ), 43800 );
                    Add( typeof( SmallBrickHouseDeed ), 43800 );
                    Add( typeof( WoodHouseDeed ), 43800 );
                    Add( typeof( WoodPlasterHouseDeed ), 43800 );
                    Add( typeof( ThatchedRoofCottageDeed ), 43800 );
                    Add( typeof( BrickHouseDeed ), 144500 );
                    Add( typeof( TwoStoryWoodPlasterHouseDeed ), 192400 );
                    Add( typeof( TowerDeed ), 433200 );
                    Add( typeof( KeepDeed ), 665200 );
                    Add( typeof( CastleDeed ), 1022800 );
                    Add( typeof( LargePatioDeed ), 152800 );
                    Add( typeof( LargeMarbleDeed ), 192800 );
                    Add( typeof( SmallTowerDeed ), 88500 );
                    Add( typeof( LogCabinDeed ), 97800 );
                    Add( typeof( SandstonePatioDeed ), 90900 );
                    Add( typeof( VillaDeed ), 136500 );
                    Add( typeof( StoneWorkshopDeed ), 60600 );
                    Add( typeof( MarbleWorkshopDeed ), 60300 );
                    Add( typeof( SmallBrickHouseDeed ), 43800 );*/
                }

                //Instruments
                {
                    Add(typeof(LapHarp), 10);
                    Add(typeof(Lute), 10);
                    Add(typeof(Drums), 10);
                    Add(typeof(Harp), 10);
                    Add(typeof(Tambourine), 10);
                }

                //Magic
                {
                    Add(typeof(Bottle), 3);
                    Add(typeof(MortarPestle), 4);

                    Add(typeof(RecallRune), 1);
                    Add(typeof(Spellbook), 9);

                    Add(typeof(ScribesPen), 4);
                    Add(typeof(BlankScroll), 1);

                    Type[] types = Loot.RegularScrollTypes;
                    for (int i = 0; i < types.Length; ++i)
                        Add(types[i], (i / 8 + 2) * 2);
                }

                //Maps
                {
                    Add(typeof(MapmakersPen), 4);
                    Add(typeof(BlankMap), 2);
                    Add(typeof(CityMap), 3);
                    Add(typeof(LocalMap), 3);
                    Add(typeof(WorldMap), 3);
                    Add(typeof(PresetMapEntry), 3);
                    //TODO: Buy back maps that the mapmaker sells!!!
                }

                //Misc
                {
                    Add(typeof(Torch), 3);
                    Add(typeof(Candle), 3);
                    Add(typeof(Chessboard), 1);
                    Add(typeof(CheckerBoard), 1);
                    Add(typeof(Backgammon), 1);
                    Add(typeof(Dices), 1);
                    Add(typeof(ContractOfEmployment), 626);
                    Add(typeof(JarHoney), 1);
                    Add(typeof(Beeswax), 1);
                    Add(typeof(Bandage), 1);
                    Add(typeof(Bag), 3);
                    Add(typeof(GuildDeed), 6225);
                    Add(typeof(Arrow), 1);
                    Add(typeof(Bolt), 2);
                    Add(typeof(Backpack), 7);
                    Add(typeof(Pouch), 3);
                    Add(typeof(Lockpick), 6);
                    Add(typeof(Bottle), 3);
                    Add(typeof(BrownBook), 7);
                    Add(typeof(RedBook), 7);
                    Add(typeof(BlueBook), 7);
                    Add(typeof(TanBook), 7);
                    Add(typeof(Kindling), 1);
                    Add(typeof(HairDye), 30);
                    Add(typeof(Key), 1);
                    Add(typeof(Clock), 11);
                    Add(typeof(ClockParts), 1);
                    Add(typeof(AxleGears), 1);
                    Add(typeof(Gears), 1);
                    Add(typeof(Hinge), 1);
                    Add(typeof(Sextant), 6);
                    Add(typeof(SextantParts), 2);
                    Add(typeof(Springs), 1);
                }

                //Potions
                {
                    Add(typeof(NightSightPotion), 7);
                    Add(typeof(AgilityPotion), 7);
                    Add(typeof(StrengthPotion), 7);
                    Add(typeof(RefreshPotion), 7);
                    Add(typeof(LesserCurePotion), 7);
                    Add(typeof(LesserHealPotion), 7);
                    Add(typeof(LesserPoisonPotion), 7);
                    Add(typeof(LesserExplosionPotion), 10);
                }

                //Reagents
                {
                    Add(typeof(BlackPearl), 3);
                    Add(typeof(Bloodmoss), 3);
                    Add(typeof(MandrakeRoot), 2);
                    Add(typeof(Garlic), 2);
                    Add(typeof(Ginseng), 2);
                    Add(typeof(Nightshade), 2);
                    Add(typeof(SpidersSilk), 2);
                    Add(typeof(SulfurousAsh), 2);
                    Add(typeof(HairDye), 19);
                }

                //Resources
                {
                    //Ores *CHECKED*
                    Add(typeof(IronOre), 3);
                    Add(typeof(BronzeOre), 4);
                    Add(typeof(DullCopperOre), 4);
                    Add(typeof(CopperOre), 4);
                    Add(typeof(OnyxOre), 4);
                    Add(typeof(PyriteOre), 4);
                    Add(typeof(MalachiteOre), 4);
                    Add(typeof(AzuriteOre), 4);
                    Add(typeof(PlatinumOre), 4);
                    Add(typeof(LavarockOre), 4);
                    Add(typeof(MysticOre), 4);
                    Add(typeof(NewZuluOre), 4);
                    Add(typeof(SpikeOre), 4);
                    Add(typeof(FruityOre), 4);
                    Add(typeof(IceRockOre), 4);
                    Add(typeof(SilverRockOre), 4);
                    Add(typeof(SpectralOre), 4);
                    Add(typeof(UndeadOre), 4);
                    Add(typeof(DarkPaganOre), 4);
                    Add(typeof(OldBritainOre), 4);
                    Add(typeof(VirginityOre), 4);
                    Add(typeof(BlackDwarfOre), 4);
                    Add(typeof(RedElvenOre), 4);
                    Add(typeof(DripstoneOre), 4);
                    Add(typeof(ExecutorOre), 4);
                    Add(typeof(PeachblueOre), 4);
                    Add(typeof(DestructionOre), 4);
                    Add(typeof(AnraOre), 4);
                    Add(typeof(GoddessOre), 4);
                    Add(typeof(CrystalOre), 4);
                    Add(typeof(DoomOre), 4);
                    Add(typeof(EbonTwilightSapphireOre), 4);
                    Add(typeof(DarkSableRubyOre), 4);
                    Add(typeof(RadiantNimbusDiamondOre), 4);

                    //Ingots *CHECKED*
                    Add(typeof(IronIngot), 3);
                    Add(typeof(BronzeIngot), 4);
                    Add(typeof(DullCopperIngot), 5);
                    Add(typeof(CopperIngot), 5);
                    Add(typeof(OnyxIngot), 5);
                    Add(typeof(PyriteIngot), 5);
                    Add(typeof(MalachiteIngot), 5);
                    Add(typeof(AzuriteIngot), 5);
                    Add(typeof(PlatinumIngot), 5);
                    Add(typeof(LavarockIngot), 5);
                    Add(typeof(MysticIngot), 5);
                    Add(typeof(NewZuluIngot), 5);
                    Add(typeof(SpikeIngot), 5);
                    Add(typeof(FruityIngot), 5);
                    Add(typeof(IceRockIngot), 5);
                    Add(typeof(SilverRockIngot), 5);
                    Add(typeof(SpectralIngot), 5);
                    Add(typeof(UndeadIngot), 5);
                    Add(typeof(DarkPaganIngot), 5);
                    Add(typeof(OldBritainIngot), 5);
                    Add(typeof(VirginityIngot), 5);
                    Add(typeof(BlackDwarfIngot), 5);
                    Add(typeof(RedElvenIngot), 5);
                    Add(typeof(DripstoneIngot), 5);
                    Add(typeof(ExecutorIngot), 5);
                    Add(typeof(PeachblueIngot), 5);
                    Add(typeof(DestructionIngot), 5);
                    Add(typeof(AnraIngot), 5);
                    Add(typeof(GoddessIngot), 5);
                    Add(typeof(CrystalIngot), 5);
                    Add(typeof(DoomIngot), 5);
                    Add(typeof(EbonTwilightSapphireIngot), 5);
                    Add(typeof(DarkSableRubyIngot), 5);
                    Add(typeof(RadiantNimbusDiamondIngot), 5);

                    //Logs *CHECKED*
                    Add(typeof(Log), 1);
                    Add(typeof(PinetreeLog), 4);
                    Add(typeof(CherryLog), 4);
                    Add(typeof(OakLog), 4);
                    Add(typeof(PurplePassionLog), 4);
                    Add(typeof(GoldenReflectionsLog), 4);
                    Add(typeof(JadeLog), 4);
                    Add(typeof(DarkwoodLog), 4);
                    Add(typeof(StonewoodLog), 4);
                    Add(typeof(SunLog), 4);
                    Add(typeof(SwampLog), 4);
                    Add(typeof(StardustLog), 4);
                    Add(typeof(SilverleafLog), 4);
                    Add(typeof(StormTealLog), 4);
                    Add(typeof(EmeraldLog), 4);
                    Add(typeof(BloodLog), 4);
                    Add(typeof(CrystalLog), 4);
                    Add(typeof(DoomLog), 4);
                    Add(typeof(YoungOakLog), 4);
                    Add(typeof(ZuluLog), 4);
                    Add(typeof(HardrangerLog), 4);
                    Add(typeof(GauntletLog), 4);
                    Add(typeof(BloodhorseLog), 4);
                    Add(typeof(DarknessLog), 4);
                    Add(typeof(ElvenLog), 4);

                    //Hides *CHECKED*
                    Add(typeof(Hide), 1);
                    Add(typeof(RatHide), 2);
                    Add(typeof(DragonHide), 2);
                    Add(typeof(TrollHide), 2);
                    Add(typeof(LizardHide), 2);
                    Add(typeof(SerpentHide), 2);
                    Add(typeof(OstardHide), 2);
                    Add(typeof(BalronHide), 2);
                    Add(typeof(LavaHide), 2);
                    Add(typeof(NecromancerHide), 2);
                    Add(typeof(LicheHide), 2);
                    Add(typeof(WyrmHide), 2);
                    Add(typeof(GoldenDragonHide), 2);
                    Add(typeof(WolfHide), 2);
                    Add(typeof(BearHide), 2);
                    Add(typeof(IceCrystalHide), 2);

                    //Fletcher
                    Add(typeof(Shaft), 1);
                    Add(typeof(Feather), 1);

                    //Tailor
                    Add(typeof(BoltOfCloth), 50);
                    Add(typeof(SpoolOfThread), 9);
                    Add(typeof(Flax), 51);
                    Add(typeof(Cotton), 51);
                    Add(typeof(Wool), 31);
                    Add(typeof(UncutCloth), 1);
                    Add(typeof(LightYarnUnraveled), 9);
                    Add(typeof(LightYarn), 9);
                    Add(typeof(DarkYarn), 9);

                    //Misc *CHECKED*
                    Add(typeof(Clay), 2);
                    Add(typeof(Glass), 1);
                    Add(typeof(Sand), 1);
                }

                //Tools
                {
                    Add(typeof(FletcherTools), 1);

                    Add(typeof(SmithHammer), 10);
                    Add(typeof(Tongs), 7);

                    Add(typeof(Saw), 9);
                    Add(typeof(Scorp), 6);
                    Add(typeof(SmoothingPlane), 6);
                    Add(typeof(DrawKnife), 6);
                    Add(typeof(Froe), 6);
                    Add(typeof(Hammer), 14);
                    Add(typeof(Inshave), 6);
                    Add(typeof(JointingPlane), 6);
                    Add(typeof(MouldingPlane), 6);
                    Add(typeof(DovetailSaw), 7);
                    Add(typeof(Axle), 1);

                    Add(typeof(Skillet), 1);
                    Add(typeof(FlourSifter), 1);
                    Add(typeof(RollingPin), 1);

                    Add(typeof(Pickaxe), 12);
                    Add(typeof(Shovel), 6);

                    Add(typeof(Scissors), 6);
                    Add(typeof(SewingKit), 1);
                    Add(typeof(Dyes), 4);
                    Add(typeof(DyeTub), 4);

                    Add(typeof(ThiefGloves), 35);

                    Add(typeof(TinkerTools), 3);
                }

                //Weapons
                {
                    Add(typeof(BattleAxe), 13);
                    Add(typeof(DoubleAxe), 26);
                    Add(typeof(ExecutionersAxe), 15);
                    Add(typeof(LargeBattleAxe), 16);
                    Add(typeof(Pickaxe), 11);
                    Add(typeof(TwoHandedAxe), 16);
                    Add(typeof(WarAxe), 14);
                    Add(typeof(Axe), 20);

                    Add(typeof(Bardiche), 30);
                    Add(typeof(Halberd), 21);

                    Add(typeof(ButcherKnife), 7);
                    Add(typeof(Cleaver), 7);
                    Add(typeof(Dagger), 10);
                    Add(typeof(SkinningKnife), 7);

                    Add(typeof(Club), 8);
                    Add(typeof(HammerPick), 13);
                    Add(typeof(Mace), 14);
                    Add(typeof(Maul), 10);
                    Add(typeof(WarHammer), 12);
                    Add(typeof(WarMace), 15);

                    Add(typeof(HeavyCrossbow), 27);
                    Add(typeof(Bow), 17);
                    Add(typeof(Crossbow), 23);

                    Add(typeof(Spear), 15);
                    Add(typeof(Pitchfork), 9);
                    Add(typeof(ShortSpear), 11);

                    Add(typeof(BlackStaff), 11);
                    Add(typeof(GnarledStaff), 8);
                    Add(typeof(QuarterStaff), 9);
                    Add(typeof(ShepherdsCrook), 10);

                    Add(typeof(Broadsword), 17);
                    Add(typeof(Cutlass), 12);
                    Add(typeof(Katana), 16);
                    Add(typeof(Kryss), 16);
                    Add(typeof(Longsword), 27);
                    Add(typeof(Scimitar), 18);
                    Add(typeof(VikingSword), 27);

                    Add(typeof(Hatchet), 13);
                    Add(typeof(WarFork), 16);
                }
            }
        }
    }
}