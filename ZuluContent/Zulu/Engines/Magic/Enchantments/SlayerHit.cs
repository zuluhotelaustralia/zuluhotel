using System;
using System.Collections.Generic;
using System.Linq;
using MessagePack;
using Scripts.Zulu.Spells.Earth;
using Server;
using Server.Engines.Magic;
using Server.Engines.Magic.HitScripts;
using Server.Items;
using Server.Mobiles;
using Server.Spells;
using Server.Spells.Eighth;
using Server.Spells.Fifth;
using Server.Spells.First;
using Server.Spells.Fourth;
using Server.Spells.Second;
using Server.Spells.Seventh;
using Server.Spells.Sixth;
using Server.Spells.Third;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments
{
    [MessagePackObject]
    public class SlayerHit : Enchantment<SlayerHitInfo>
    {
        [IgnoreMember]
        public override string AffixName => Cursed
            ? SlayerHitInfo.SlayerEntries[Type].CursedName
            : SlayerHitInfo.SlayerEntries[Type].Name;

        [Key(1)] public CreatureType Type { get; set; } = CreatureType.None;

        [Key(2)] public double Chance { get; set; } = 0.0;

        public override void OnArmorHit(Mobile attacker, Mobile defender, BaseWeapon weapon, BaseArmor armor, ref int damage)
        {
            if (Chance > Utility.RandomDouble() && attacker is BaseCreature creature && creature.CreatureType == Type) 
                damage /= 2;
        }

        public override void OnMeleeHit(Mobile attacker, Mobile defender, BaseWeapon weapon, ref int damage)
        {
            if (Chance > Utility.RandomDouble() && defender is BaseCreature creature && creature.CreatureType == Type) 
                damage *= 2;
        }
    }

    public record SlayerEntry
    {
        public string Name { get; init; }
        public string CursedName { get; init; }
        public int Hue { get; init; }
        public int CursedHue { get; init; }
    }

    public class SlayerHitInfo : EnchantmentInfo
    {
        public override string Description { get; protected set; } = "Creature Slayer";
        public override EnchantNameType Place { get; protected set; } = EnchantNameType.Suffix;
        
        public override int Hue { get; protected set; } = 0;
        public override int CursedHue { get; protected set; } = 0;

        public static readonly IReadOnlyDictionary<CreatureType, SlayerEntry> SlayerEntries =
            new Dictionary<CreatureType, SlayerEntry>
            {
                [CreatureType.Slime] = new SlayerEntry
                {
                    Name = "Slime Slayer",
                    CursedName = "Slime Protector",
                    Hue = 0,
                    CursedHue = 0
                },
                [CreatureType.Ratkin] = new SlayerEntry
                {
                    Name = "Ratkin Slayer",
                    CursedName = "Ratkin Protector",
                    Hue = 0,
                    CursedHue = 0
                },
                [CreatureType.Plant] = new SlayerEntry
                {
                    Name = "Plant Slayer",
                    CursedName = "Plant Protector",
                    Hue = 0,
                    CursedHue = 0
                },
                [CreatureType.Animal] = new SlayerEntry
                {
                    Name = "Animal Slayer",
                    CursedName = "Animal Protector",
                    Hue = 871,
                    CursedHue = 867
                },
                [CreatureType.Beholder] = new SlayerEntry
                {
                    Name = "Beholder Slayer",
                    CursedName = "Beholder Protector",
                    Hue = 0,
                    CursedHue = 0
                },
                [CreatureType.Orc] = new SlayerEntry
                {
                    Name = "Orc Slayer",
                    CursedName = "Orc Protector",
                    Hue = 845,
                    CursedHue = 842
                },
                [CreatureType.Terathan] = new SlayerEntry
                {
                    Name = "Terathan Slayer",
                    CursedName = "Terathan Protector",
                    Hue = 0,
                    CursedHue = 0
                },
                [CreatureType.Ophidian] = new SlayerEntry
                {
                    Name = "Ophidian Slayer",
                    CursedName = "Ophidian Protector",
                    Hue = 0,
                    CursedHue = 0
                },
                [CreatureType.Animated] = new SlayerEntry
                {
                    Name = "Bewitched Slayer",
                    CursedName = "Bewitched Protector",
                    Hue = 0,
                    CursedHue = 0
                },
                [CreatureType.Gargoyle] = new SlayerEntry
                {
                    Name = "Gargoyle Slayer",
                    CursedName = "Gargoyle Protector",
                    Hue = 0,
                    CursedHue = 0
                },
                [CreatureType.Troll] = new SlayerEntry
                {
                    Name = "Troll Slayer",
                    CursedName = "Troll Protector",
                    Hue = 816,
                    CursedHue = 812
                },
                [CreatureType.Giantkin] = new SlayerEntry
                {
                    Name = "Giant Slayer",
                    CursedName = "Giant Protector",
                    Hue = 0,
                    CursedHue = 0
                },
                [CreatureType.Elemental] = new SlayerEntry
                {
                    Name = "Elemental Slayer",
                    CursedName = "Elemental Protector",
                    Hue = 901,
                    CursedHue = 897
                },
                [CreatureType.Undead] = new SlayerEntry
                {
                    Name = "Silver",
                    CursedName = "Undead Protector",
                    Hue = 856,
                    CursedHue = 852
                },
                [CreatureType.Daemon] = new SlayerEntry
                {
                    Name = "Holy",
                    CursedName = "Unholy",
                    Hue = 0,
                    CursedHue = 0
                },
                [CreatureType.Dragonkin] = new SlayerEntry
                {
                    Name = "Dragon Slayer",
                    CursedName = "Dragon Protector",
                    Hue = 641,
                    CursedHue = 631
                },
                [CreatureType.Human] = new SlayerEntry
                {
                    Name = "Assassin's",
                    CursedName = "Peacekeeping",
                    Hue = 1002,
                    CursedHue = 1020
                },
            };

        // When LINQ goes wrong...
        public override string[,] Names { get; protected set; } = SlayerEntries.Values.Aggregate(
            (array: new string[SlayerEntries.Count, 2], index: 0),
            (acc, cur) =>
            {
                var (array, index) = acc;
                array[index, 0] = cur.Name;
                array[index, 1] = cur.CursedName;

                index++;
                return acc;
            }).array;
    }
}