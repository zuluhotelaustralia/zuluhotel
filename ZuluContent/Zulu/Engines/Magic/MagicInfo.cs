using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Engines.Magic;
using Server;
using Server.Engines.Magic;
using Server.Items;

namespace ZuluContent.Zulu.Engines.Magic
{
    public class MagicInfo
    {
        private static readonly string[,] DefaultSkillNames =
        {
            {string.Empty, string.Empty},
            {"Apprentice", "Novice"},
            {"Journeyman", "Neophyte"},
            {"Expert", "Inept"},
            {"Adept", "Incompetent"},
            {"Master", "Failed"},
            {"Grandmaster", "Blundering"},
        };

        private static readonly string[,] DefaultElementalProtectionNames =
        {
            {string.Empty, string.Empty},
            {"Bane", "Weakness"},
            {"Warding", "Vulnerability"},
            {"Protection", "Amplification"},
            {"Immunity", "Defenselessness"},
            {"Attunement", "Exposure"},
            {"Absorbsion", "Endangerment"},
        };

        public string Description { get; }
        public EnchantNameType Place { get; }
        public string[,] Names { get; }
        public int Hue { get; }
        public int CursedHue { get; }

        private MagicInfo(string description, EnchantNameType place, string[,] names, int hue = 0, int cursedHue = 0)
        {
            Place = place;
            Names = names;
            Description = description;
            Hue = hue;
            CursedHue = cursedHue;
        }

        public string GetName(int index, bool cursed = false)
        {
            return index < Names.GetLength(0) ? Names[index, cursed ? 1 : 0] : string.Empty;
        }

        public string GetName(Enum target, bool cursed = false)
        {
            return GetName((int) (object) target, cursed);
        }

        private static MagicInfo CreateForSkill(string profession)
        {
            return new MagicInfo(profession, EnchantNameType.Prefix, MakeNames(profession, DefaultSkillNames), 0, 0);
        }

        private static string[,] MakeNames(string profession, string[,] defaults)
        {
            var result = new string[defaults.GetLength(0), 2];
            for (var i = 0; i < defaults.GetLength(0); i++)
            {
                result[i, 0] = $"{defaults[i, 0]} {profession}'s";
                result[i, 1] = $"{defaults[i, 1]} {profession}'s";
            }

            return result;
        }

        public static readonly IReadOnlyDictionary<Enum, MagicInfo> MagicInfoMap = new Dictionary<Enum, MagicInfo>
        {
            [StatType.Str] = new MagicInfo(
                "Strength",
                EnchantNameType.Prefix, new[,]
                {
                    {string.Empty, string.Empty},
                    {"Warrior's", "Weakling's"},
                    {"Veteran's", "Enfeebling"},
                    {"Champion's", "Powerless"},
                    {"Hero's", "Frail"},
                    {"Warlord's", "Diseased"},
                    {"King's", "Leper's"},
                }),
            [StatType.Int] = new MagicInfo(
                "Intelligence",
                EnchantNameType.Prefix, new[,]
                {
                    {string.Empty, string.Empty},
                    {"Apprentice's", "Fool's"},
                    {"Adept's", "Simpleton's"},
                    {"Wizard's", "Infantile"},
                    {"Archmage's", "Senile"},
                    {"Magister's", "Demented"},
                    {"Oracle's", "Madman's"},
                }),
            [StatType.Dex] = new MagicInfo(
                "Dexterity",
                EnchantNameType.Prefix, new[,]
                {
                    {string.Empty, string.Empty},
                    {"Cutpurse's", "Heavy"},
                    {"Thief's", "Leaden"},
                    {"Cat Burglar's", "Encumbering"},
                    {"Tumbler's", "Binding"},
                    {"Acrobat's", "Fumbling"},
                    {"Escape Artist's", "Blundering"},
                }),
            [SkillName.Alchemy] = CreateForSkill("Alchemist"),
            [SkillName.Anatomy] = CreateForSkill("Physician"),
            [SkillName.AnimalLore] = CreateForSkill("Naturalist"),
            [SkillName.ItemID] = CreateForSkill("Merchant"),
            [SkillName.ArmsLore] = CreateForSkill("Arms Dealer"),
            [SkillName.Parry] = CreateForSkill("Shield Fighter"),
            [SkillName.Begging] = CreateForSkill("Beggar"),
            [SkillName.Blacksmith] = CreateForSkill("Blacksmith"),
            [SkillName.Fletching] = CreateForSkill("Fletcher"),
            [SkillName.Peacemaking] = CreateForSkill("Peacemaker"),
            [SkillName.Camping] = CreateForSkill("Camper"),
            [SkillName.Carpentry] = CreateForSkill("Carpenter"),
            [SkillName.Cartography] = CreateForSkill("Cartographer"),
            [SkillName.Cooking] = CreateForSkill("Chef"),
            [SkillName.DetectHidden] = CreateForSkill("Scout"),
            [SkillName.Discordance] = CreateForSkill("Commander"),
            [SkillName.EvalInt] = CreateForSkill("Scholar"),
            [SkillName.Healing] = CreateForSkill("Healer"),
            [SkillName.Fishing] = CreateForSkill("Fisherman"),
            [SkillName.Forensics] = CreateForSkill("Coroner"),
            [SkillName.Herding] = CreateForSkill("Shepherd"),
            [SkillName.Lumberjacking] = CreateForSkill("Lumberjack"),
            [SkillName.Mining] = CreateForSkill("Miner"),
            [SkillName.Meditation] = CreateForSkill("Stoic"),
            [SkillName.Stealth] = CreateForSkill("Spy"),
            [SkillName.RemoveTrap] = CreateForSkill("Trap Remover"),
            [SkillName.Necromancy] = CreateForSkill("Necromancer"),
            [SkillName.SpiritSpeak] = CreateForSkill("Channeler"),
            [SkillName.Stealing] = CreateForSkill("Thief"),
            [SkillName.Tailoring] = CreateForSkill("Tailor"),
            [SkillName.AnimalTaming] = CreateForSkill("Tamer"),
            [SkillName.TasteID] = CreateForSkill("Taste Tester"),
            [SkillName.Tinkering] = CreateForSkill("Tinker"),
            [SkillName.Tracking] = CreateForSkill("Ranger"),
            [SkillName.Veterinary] = CreateForSkill("Veterinarian"),
            [SkillName.Provocation] = CreateForSkill("Provoker"),
            [SkillName.Inscribe] = CreateForSkill("Scribe"),
            [SkillName.Lockpicking] = CreateForSkill("Locksmith"),
            [SkillName.Magery] = CreateForSkill("Mage"),
            [SkillName.Snooping] = CreateForSkill("Pickpocket"),
            [SkillName.Musicianship] = CreateForSkill("Bard"),
            [SkillName.Poisoning] = CreateForSkill("Assassin"),
            [SkillName.Hiding] = new MagicInfo(
                nameof(SkillName.Hiding),
                EnchantNameType.Prefix,
                new[,]
                {
                    {string.Empty, string.Empty},
                    {"Concealing", "Shiny"},
                    {"Camouflaged", "Gleaming"},
                    {"Shadowed's", "Sparkling"},
                    {"Undetectable", "Brilliant"},
                    {"Obscuring", "Illuminating"},
                    {"Obfuscating", "Dazzling"},
                }),
            [SkillName.MagicResist] = new MagicInfo(
                "Magical Resitance",
                EnchantNameType.Prefix,
                new[,]
                {
                    {string.Empty, string.Empty},
                    {"Shielded", "Conducting"},
                    {"Warded", "Sensitive"},
                    {"Sanctified", "Focusing"},
                    {"Defiant", "Channeling"},
                    {"Guardian's", "Translucent"},
                    {"Deflecting", "Amplifying"},
                }),
            [SkillName.Tactics] = new MagicInfo(
                nameof(SkillName.Tactics),
                EnchantNameType.Prefix,
                new[,]
                {
                    {string.Empty, string.Empty},
                    {"Fine", "Poor"},
                    {"Superior", "Dull"},
                    {"Superb", "Inferior"},
                    {"Magnificent", "Tainted"},
                    {"Elegant", "Pitiful"},
                    {"Peerless", "Worthless"},
                }),
            [SkillName.Archery] = new MagicInfo(
                nameof(SkillName.Archery),
                EnchantNameType.Prefix,
                new[,]
                {
                    {string.Empty, string.Empty},
                    {"Large", "Water Damaged"},
                    {"Great", "Crooked"},
                    {"Composite", "Frayed"},
                    {"Archer's", "Warped"},
                    {"Ranger's", "Decaying"},
                    {"Marksman's", "Unstrung"},
                }),
            [SkillName.Swords] = new MagicInfo(
                nameof(SkillName.Swords),
                EnchantNameType.Prefix,
                new[,]
                {
                    {string.Empty, string.Empty},
                    {"Competitor's", "Unbalanced"},
                    {"Duelist's", "Fragile"},
                    {"Gladiator's", "Rusted"},
                    {"Knight's", "Cracked"},
                    {"Noble's", "Decaying"},
                    {"Arms Master's", "Misshapen"},
                }),
            [SkillName.Macing] = new MagicInfo(
                nameof(SkillName.Macing),
                EnchantNameType.Prefix,
                new[,]
                {
                    {string.Empty, string.Empty},
                    {"Competitor's", "Unbalanced"},
                    {"Duelist's", "Fragile"},
                    {"Gladiator's", "Rusted"},
                    {"Knight's", "Cracked"},
                    {"Noble's", "Decaying"},
                    {"Arms Master's", "Misshapen"},
                }),
            [SkillName.Fencing] = new MagicInfo(
                nameof(SkillName.Fencing),
                EnchantNameType.Prefix,
                new[,]
                {
                    {string.Empty, string.Empty},
                    {"Competitor's", "Unbalanced"},
                    {"Duelist's", "Fragile"},
                    {"Gladiator's", "Rusted"},
                    {"Knight's", "Cracked"},
                    {"Noble's", "Decaying"},
                    {"Arms Master's", "Misshapen"},
                }),
            [SkillName.Wrestling] = new MagicInfo(
                nameof(SkillName.Wrestling),
                EnchantNameType.Prefix,
                new[,]
                {
                    {string.Empty, string.Empty},
                    {"Competitor's", "Unbalanced"},
                    {"Duelist's", "Fragile"},
                    {"Gladiator's", "Rusted"},
                    {"Knight's", "Cracked"},
                    {"Noble's", "Decaying"},
                    {"Arms Master's", "Misshapen"},
                }),
            [MagicProp.Damage] = new MagicInfo(
                "Weapon Damage Increase",
                EnchantNameType.Prefix,
                new[,]
                {
                    {string.Empty, string.Empty},
                    {"Ruin", "Gentle Touch"},
                    {"Might", "Feather's Touch"},
                    {"Force", "Calmness"},
                    {"Power", "Even Temper"},
                    {"Vanquishing", "Tranquility"},
                    {"Devastation", "Pacifism"},
                }),
            [MagicProp.Durability] = new MagicInfo(
                "Item Durability",
                EnchantNameType.Prefix,
                new[,]
                {
                    {string.Empty, string.Empty},
                    {"Durable", "Durable"},
                    {"Substantial", "Flimsy"},
                    {"Massive", "Broken"},
                    {"Fortified", "Crumbling"},
                    {"Tempered", "Rotten"},
                    {"Indestructible", "Decomposing"}
                }),
            [MagicProp.ArmorProtection] = new MagicInfo(
                "Armor Protection Increase",
                EnchantNameType.Suffix,
                new[,]
                {
                    {string.Empty, string.Empty},
                    {"Defense", "Penetration"},
                    {"Guarding", "Vulnerability"},
                    {"Hardening", "Disharmony"},
                    {"Fortification", "Distress"},
                    {"Invulnerability", "Disaster"},
                    {"Invincibility", "Catastrophe"},
                }),
            [ElementalType.Poison] = new MagicInfo(
                "Poison Protection",
                EnchantNameType.Suffix,
                new[,]
                {
                    {string.Empty, string.Empty},
                    {"Lesser Poison Protection", "Adder's Venom"},
                    {"Medium Poison Protection", "Cobra's Venom"},
                    {"Greater Poison Protection", "Giant Serpent's Venom"},
                    {"Deadly Poison Protection", "Silver Serpent's Venom"},
                    {"the Snake Handler", "Spider's Venom"},
                    {"Poison Absorbsion", "Dread Spider's Venom"},
                }),
            [MagicProp.PermMagicReflect] = new MagicInfo(
                "Permanent Magic Immunity",
                EnchantNameType.Suffix,
                new[,]
                {
                    {string.Empty, string.Empty},
                    {"Raw Moonstone", "Chipped Moonstone"},
                    {"Cut Moonstone", "Cracked Moonstone"},
                    {"Refined Moonstone", "Flawed Moonstone"},
                    {"Prepared Moonstone", "Inferior Moonstone"},
                    {"Enchanted Moonstone", "Chaotic Moonstone"},
                    {"Flawless Moonstone", "Corrupted Moonstone"},
                }),
            [MagicProp.Quality] = new MagicInfo(
                "Item Quality",
                EnchantNameType.Prefix,
                new[,]
                {
                    {"Makeshift", "Makeshift"},
                    {string.Empty, string.Empty}, // Regular
                    {"Exceptional", "Exceptional"}
                },
                590
            ),
            [MagicProp.ParalysisProtection] = new MagicInfo(
                "Paralysis Protection",
                EnchantNameType.Suffix,
                new[,]
                {
                    {string.Empty, string.Empty},
                    {"Free Action", "Prisoners"},
                },
                590
            ),
            [ElementalType.Water] = new MagicInfo(
                "Elemental Water Protection",
                EnchantNameType.Suffix,
                MakeNames("Water", DefaultElementalProtectionNames),
                206
            ),
            [ElementalType.Air] = new MagicInfo(
                "Elemental Air Protection",
                EnchantNameType.Suffix,
                MakeNames("Air", DefaultElementalProtectionNames),
                1001
            ),
            [ElementalType.Fire] = new MagicInfo(
                "Elemental Fire Protection",
                EnchantNameType.Suffix,
                MakeNames("Fire", DefaultElementalProtectionNames),
                240
            ),
            [ElementalType.Earth] = new MagicInfo(
                "Elemental Earth Protection",
                EnchantNameType.Suffix,
                MakeNames("Earth", DefaultElementalProtectionNames),
                343
            ),
            [ElementalType.Physical] = new MagicInfo(
                "Physical Damage Protection",
                EnchantNameType.Suffix,
                new[,]
                {
                    {string.Empty, string.Empty},
                    {"Protection", "Pain"},
                    {"Stoneskin", "Bleeding"},
                    {"Unmovable Stone", "Rending"},
                    {"Adamantine Shielding", "Tearing"},
                    {"Mystical Cloaks", "Shredding"},
                    {"Holy Auras", "Peril"},
                },
                1160
            ),
            [ElementalType.Necro] = new MagicInfo(
                "Necromancy Protection",
                EnchantNameType.Suffix,
                new[,]
                {
                    {string.Empty, string.Empty},
                    {"Mystic Barrier", "Evil Influence"},
                    {"Divine Shielding", "Liche's Laughter"},
                    {"Heavenly Sanctuary", "Daemonic Temptation"},
                    {"Angelic Protection", "Dark Channeling"},
                    {"Arch-Angel's Guidance", "Shadow's Touch"},
                    {"Seraphim's Warding", "Guardian's Blessing"},
                },
                1170
            ),
            [MagicProp.Healing] = new MagicInfo(
                "Healing Bonus",
                EnchantNameType.Suffix,
                new[,]
                {
                    {string.Empty, string.Empty},
                    {"Relief", "Wounds"},
                    {"Respite", "Bruises"},
                    {"Rest", "Deterioration"},
                    {"Regeneration", "Festering"},
                    {"Healing", "Atrophy"},
                    {"Nature's Blessing", "Blight"}
                },
                1182
            ),
            [MagicProp.ArmorBonus] = new MagicInfo(
                "Armor Bonus",
                EnchantNameType.Prefix,
                new[,]
                {
                    {string.Empty, string.Empty}, // None
                    {"Iron", "Glass"},
                    {"Steel", "Rusty"},
                    {"Meteoric Steel", "Aluminum"},
                    {"Obsidian", "Pitted"},
                    {"Onyx", "Dirty"},
                    {"Adamantium", "Tarnished"}
                },
                1109
            )
        };
    }
}