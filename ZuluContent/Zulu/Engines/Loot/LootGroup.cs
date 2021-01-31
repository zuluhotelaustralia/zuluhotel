using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Server.Items;

namespace Server.Scripts.Engines.Loot
{
    public partial class LootGroup : IEnumerable
    {
        public List<LootEntryItem> Items { get; } = new();

        public void Add(LootEntryItem item) => Items.Add(item);

        public void Add(Type itemType, double chance = 1)
            => Items.Add(new LootEntryItem(itemType, 1, 1, chance));

        public void Add(Type itemType, int quantity, double chance)
            => Items.Add(new LootEntryItem(itemType, quantity, quantity, chance));

        public void Add(Type itemType, int minQuantity, int maxQuantity, double chance)
            => Items.Add(new LootEntryItem(itemType, minQuantity, maxQuantity, chance));

        public void Add(IEnumerable<Type> types)
        {
            foreach (var t in types) 
                Add(t);
        }
        
        public IEnumerator GetEnumerator()
        {
            return Items.GetEnumerator();
        }
        
        private void Add(LootGroup subGroup)
        {
            foreach (var i in subGroup.Items) 
                Add(i);
        }

        public List<LootItem> Roll(int quantity)
        {
            var items = new List<LootItem>();

            if (Items.Count == 0)
                return items;

            for (var i = 0; i < quantity; i++)
            {
                var item = Items[Utility.Random(Items.Count)];
                var itemQuantity = Utility.Random(item.MinQuantity, item.MaxQuantity);
                if (Utility.RandomDouble() < item.Chance)
                    items.Add(new LootItem(item.Value) {Quantity = itemQuantity});
            }

            return items;
        }
    }


    /**
     * Loot Group definitions
     * (Split into a partial for separation from the class members)
     * 
     */
    public partial class LootGroup
    {
        public static readonly LootGroup Ores = new()
        {
            {typeof(IronOre), 3, 9, 0.91},
            {typeof(SpikeOre), 3, 6, 0.88},
            {typeof(FruityOre), 3, 6, 0.85},
            {typeof(BlackDwarfOre), 3, 6, 0.82},
            {typeof(BronzeOre), 3, 6, 0.79},
            {typeof(DarkPaganOre), 3, 6, 0.76},
            {typeof(SilverRockOre), 2, 6, 0.73},
            {typeof(PlatinumOre), 2, 6, 0.70},
            {typeof(DullCopperOre), 2, 6, 0.67},
            {typeof(MysticOre), 2, 6, 0.64},
            {typeof(CopperOre), 2, 6, 0.61},
            {typeof(SpectralOre), 1, 4, 0.58},
            {typeof(OnyxOre), 1, 4, 0.55},
            {typeof(OldBritainOre), 1, 4, 0.52},
            {typeof(RedElvenOre), 1, 4, 0.49},
            {typeof(UndeadOre), 1, 4, 0.46},
            {typeof(PyriteOre), 1, 3, 0.43},
            {typeof(VirginityOre), 1, 3, 0.40},
            {typeof(MalachiteOre), 1, 3, 0.37},
            {typeof(LavarockOre), 1, 3, 0.34},
            {typeof(AzuriteOre), 1, 3, 0.31},
            {typeof(IceRockOre), 1, 2, 0.28},
            {typeof(DripstoneOre), 1, 2, 0.25},
            {typeof(ExecutorOre), 1, 2, 0.22},
            {typeof(PeachblueOre), 1, 2, 0.19},
            {typeof(DestructionOre), 1, 2, 0.16},
            {typeof(AnraOre), 1, 0.13},
            {typeof(CrystalOre), 1, 0.10},
            {typeof(DoomOre), 1, 0.7},
            {typeof(GoddessOre), 1, 0.4},
            {typeof(NewZuluOre), 1, 0.1},
        };

        public static readonly LootGroup Gems = new()
        {
            typeof(Amber), typeof(Amethyst), typeof(Citrine),
            typeof(Diamond), typeof(Emerald), typeof(Ruby),
            typeof(Sapphire), typeof(StarSapphire), typeof(Tourmaline)
        };

        public static readonly LootGroup Reagents = new()
        {
            typeof(BlackPearl), typeof(Bloodmoss), typeof(Garlic),
            typeof(Ginseng), typeof(MandrakeRoot), typeof(Nightshade),
            typeof(SulfurousAsh), typeof(SpidersSilk)
        };

        public static readonly LootGroup PaganReagents = new()
        {
            typeof(BatWing), typeof(Bone), typeof(DeadWood),
            typeof(ExecutionersCap), typeof(FertileDirt), typeof(NoxCrystal),
            typeof(Blackmoor), typeof(Brimstone), typeof(EyeOfNewt),
            typeof(Pumice), typeof(VolcanicAsh), typeof(Obsidian),
            typeof(PigIron), typeof(VialOfBlood), typeof(WyrmsHeart),
            typeof(Bloodspawn), typeof(DaemonBone), typeof(DragonsBlood),
        };

        public static readonly LootGroup MeleeWeapons = new()
        {
            typeof(Axe), typeof(BattleAxe), typeof(DoubleAxe),
            typeof(ExecutionersAxe), typeof(Hatchet), typeof(LargeBattleAxe),
            typeof(TwoHandedAxe), typeof(WarAxe), typeof(Club),
            typeof(Mace), typeof(Maul), typeof(WarHammer),
            typeof(WarMace), typeof(Bardiche), typeof(Halberd),
            typeof(Spear), typeof(ShortSpear), typeof(Pitchfork),
            typeof(WarFork), typeof(BlackStaff), typeof(GnarledStaff),
            typeof(QuarterStaff), typeof(Broadsword), typeof(Cutlass),
            typeof(Katana), typeof(Kryss), typeof(Longsword),
            typeof(Scimitar), typeof(VikingSword), typeof(Pickaxe),
            typeof(HammerPick), typeof(ButcherKnife), typeof(Cleaver),
            typeof(Dagger), typeof(SkinningKnife), typeof(ShepherdsCrook),
        };
        
        public static readonly LootGroup RangedWeapons = new()
        {
            typeof(Bow), typeof(Crossbow), typeof(HeavyCrossbow)
        };
        
        public static readonly LootGroup Weapons = new()
        {
            MeleeWeapons, RangedWeapons
        };

        public static readonly LootGroup Armor = new()
        {
            typeof(BoneArms), typeof(BoneChest), typeof(BoneGloves),
            typeof(BoneLegs), typeof(BoneHelm), typeof(ChainChest),
            typeof(ChainLegs), typeof(ChainCoif), typeof(Bascinet),
            typeof(CloseHelm), typeof(Helmet), typeof(NorseHelm),
            typeof(OrcHelm), typeof(FemaleLeatherChest), typeof(LeatherArms),
            typeof(LeatherBustierArms), typeof(LeatherChest), typeof(LeatherGloves),
            typeof(LeatherGorget), typeof(LeatherLegs), typeof(LeatherShorts),
            typeof(LeatherSkirt), typeof(LeatherCap), typeof(FemalePlateChest),
            typeof(PlateArms), typeof(PlateChest), typeof(PlateGloves),
            typeof(PlateGorget), typeof(PlateHelm), typeof(PlateLegs),
            typeof(RingmailArms), typeof(RingmailChest), typeof(RingmailGloves),
            typeof(RingmailLegs), typeof(FemaleStuddedChest), typeof(StuddedArms),
            typeof(StuddedBustierArms), typeof(StuddedChest), typeof(StuddedGloves),
            typeof(StuddedGorget), typeof(StuddedLegs)
        };

        public static readonly LootGroup Shields = new()
        {
            typeof(BronzeShield), typeof(Buckler), typeof(HeaterShield),
            typeof(MetalShield), typeof(MetalKiteShield), typeof(WoodenKiteShield),
            typeof(WoodenShield), typeof(ChaosShield), typeof(OrderShield)
        };

        public static readonly LootGroup Jewelry = new()
        {
            typeof(GoldRing), typeof(GoldBracelet),
            typeof(SilverRing), typeof(SilverBracelet)
        };
        
        public static readonly LootGroup Clothing = new()
        {
            typeof(Cloak),
            typeof(Bonnet), typeof(Cap), typeof(FeatheredHat),
            typeof(FloppyHat), typeof(JesterHat), typeof(Surcoat),
            typeof(SkullCap), typeof(StrawHat), typeof(TallStrawHat),
            typeof(TricorneHat), typeof(WideBrimHat), typeof(WizardsHat),
            typeof(BodySash), typeof(Doublet), typeof(Boots),
            typeof(FullApron), typeof(JesterSuit), typeof(Sandals),
            typeof(Tunic), typeof(Shoes), typeof(Shirt),
            typeof(Kilt), typeof(Skirt), typeof(FancyShirt),
            typeof(FancyDress), typeof(ThighBoots), typeof(LongPants),
            typeof(PlainDress), typeof(Robe), typeof(ShortPants),
            typeof(HalfApron)
        };
    }
}