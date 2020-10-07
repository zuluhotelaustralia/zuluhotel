using System;
using System.Collections.Generic;
using MessagePack;
using Server;
using ZuluContent.Zulu.Engines.Magic.Enums;
using ZuluContent.Zulu.Engines.Magic.Hooks;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments
{

    [MessagePackObject]
    public class FirstSkillBonus : SkillBonus
    {
        
    }
    
    [MessagePackObject]
    public class SecondSkillBonus : SkillBonus
    {
        
    }
    
    
    public abstract class SkillBonus : Enchantment<SkillBonusInfo>, IOnEquip
    {
        [IgnoreMember] public override string AffixName => SkillBonusInfo.GetName(Skill, Value, Cursed);
        [Key(1)] public SkillName Skill { get; set; } = SkillName.Alchemy;
        [Key(2)] public double Value { get; set; } = 0;
        
        private SkillMod m_Mod;

        public void OnEquip(Item item, Mobile mobile)
        {
            m_Mod = new DefaultSkillMod(Skill, true, Value);
            
            mobile.AddSkillMod(m_Mod);
        }

        public void OnRemoved(Item item, Mobile mobile)
        {
            mobile.RemoveSkillMod(m_Mod);
        }
    }

    public class SkillBonusInfo : EnchantmentInfo
    {
        public override string Description { get; protected set; } = "Skill Mod";
        public override EnchantNameType Place { get; protected set; } = EnchantNameType.Prefix;
        public override int Hue { get; protected set; } = 0;
        public override int CursedHue { get; protected set; } = 0;
        public override string[,] Names { get; protected set; } = DefaultSkillNames;
        
        public static string GetName(SkillName name, double value, bool cursed)
        {
            var n = value > 5 ? (int) value / 5 : (int) value;
            return SkillSpecificNames[name][n, cursed ? 1 : 0];
        }

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

        private static readonly IReadOnlyDictionary<SkillName, string[,]> SkillSpecificNames =
            new Dictionary<SkillName, string[,]>
            {
                [SkillName.Hiding] = new[,]
                {
                    {string.Empty, string.Empty},
                    {"Concealing", "Shiny"},
                    {"Camouflaged", "Gleaming"},
                    {"Shadowed's", "Sparkling"},
                    {"Undetectable", "Brilliant"},
                    {"Obscuring", "Illuminating"},
                    {"Obfuscating", "Dazzling"},
                },
                [SkillName.MagicResist] = new[,]
                {
                    {string.Empty, string.Empty},
                    {"Shielded", "Conducting"},
                    {"Warded", "Sensitive"},
                    {"Sanctified", "Focusing"},
                    {"Defiant", "Channeling"},
                    {"Guardian's", "Translucent"},
                    {"Deflecting", "Amplifying"},
                },
                [SkillName.Tactics] = new[,]
                {
                    {string.Empty, string.Empty},
                    {"Fine", "Poor"},
                    {"Superior", "Dull"},
                    {"Superb", "Inferior"},
                    {"Magnificent", "Tainted"},
                    {"Elegant", "Pitiful"},
                    {"Peerless", "Worthless"},
                },
                [SkillName.Archery] = new[,]
                {
                    {string.Empty, string.Empty},
                    {"Large", "Water Damaged"},
                    {"Great", "Crooked"},
                    {"Composite", "Frayed"},
                    {"Archer's", "Warped"},
                    {"Ranger's", "Decaying"},
                    {"Marksman's", "Unstrung"},
                },
                [SkillName.Swords] = new[,]
                {
                    {string.Empty, string.Empty},
                    {"Competitor's", "Unbalanced"},
                    {"Duelist's", "Fragile"},
                    {"Gladiator's", "Rusted"},
                    {"Knight's", "Cracked"},
                    {"Noble's", "Decaying"},
                    {"Arms Master's", "Misshapen"},
                },
                [SkillName.Macing] = new[,]
                {
                    {string.Empty, string.Empty},
                    {"Competitor's", "Unbalanced"},
                    {"Duelist's", "Fragile"},
                    {"Gladiator's", "Rusted"},
                    {"Knight's", "Cracked"},
                    {"Noble's", "Decaying"},
                    {"Arms Master's", "Misshapen"},
                },
                [SkillName.Fencing] = new[,]
                {
                    {string.Empty, string.Empty},
                    {"Competitor's", "Unbalanced"},
                    {"Duelist's", "Fragile"},
                    {"Gladiator's", "Rusted"},
                    {"Knight's", "Cracked"},
                    {"Noble's", "Decaying"},
                    {"Arms Master's", "Misshapen"},
                },
                [SkillName.Wrestling] = new[,]
                {
                    {string.Empty, string.Empty},
                    {"Competitor's", "Unbalanced"},
                    {"Duelist's", "Fragile"},
                    {"Gladiator's", "Rusted"},
                    {"Knight's", "Cracked"},
                    {"Noble's", "Decaying"},
                    {"Arms Master's", "Misshapen"},
                },
                [SkillName.Alchemy] = MakeSkillNames("Alchemist", DefaultSkillNames),
                [SkillName.Anatomy] = MakeSkillNames("Physician", DefaultSkillNames),
                [SkillName.AnimalLore] = MakeSkillNames("Naturalist", DefaultSkillNames),
                [SkillName.ItemID] = MakeSkillNames("Merchant", DefaultSkillNames),
                [SkillName.ArmsLore] = MakeSkillNames("Arms Dealer", DefaultSkillNames),
                [SkillName.Parry] = MakeSkillNames("Shield Fighter", DefaultSkillNames),
                [SkillName.Begging] = MakeSkillNames("Beggar", DefaultSkillNames),
                [SkillName.Blacksmith] = MakeSkillNames("Blacksmith", DefaultSkillNames),
                [SkillName.Fletching] = MakeSkillNames("Fletcher", DefaultSkillNames),
                [SkillName.Peacemaking] = MakeSkillNames("Peacemaker", DefaultSkillNames),
                [SkillName.Camping] = MakeSkillNames("Camper", DefaultSkillNames),
                [SkillName.Carpentry] = MakeSkillNames("Carpenter", DefaultSkillNames),
                [SkillName.Cartography] = MakeSkillNames("Cartographer", DefaultSkillNames),
                [SkillName.Cooking] = MakeSkillNames("Chef", DefaultSkillNames),
                [SkillName.DetectHidden] = MakeSkillNames("Scout", DefaultSkillNames),
                [SkillName.Discordance] = MakeSkillNames("Commander", DefaultSkillNames),
                [SkillName.EvalInt] = MakeSkillNames("Scholar", DefaultSkillNames),
                [SkillName.Healing] = MakeSkillNames("Healer", DefaultSkillNames),
                [SkillName.Fishing] = MakeSkillNames("Fisherman", DefaultSkillNames),
                [SkillName.Forensics] = MakeSkillNames("Coroner", DefaultSkillNames),
                [SkillName.Herding] = MakeSkillNames("Shepherd", DefaultSkillNames),
                [SkillName.Lumberjacking] = MakeSkillNames("Lumberjack", DefaultSkillNames),
                [SkillName.Mining] = MakeSkillNames("Miner", DefaultSkillNames),
                [SkillName.Meditation] = MakeSkillNames("Stoic", DefaultSkillNames),
                [SkillName.Stealth] = MakeSkillNames("Spy", DefaultSkillNames),
                [SkillName.RemoveTrap] = MakeSkillNames("Trap Remover", DefaultSkillNames),
                [SkillName.Necromancy] = MakeSkillNames("Necromancer", DefaultSkillNames),
                [SkillName.SpiritSpeak] = MakeSkillNames("Channeler", DefaultSkillNames),
                [SkillName.Stealing] = MakeSkillNames("Thief", DefaultSkillNames),
                [SkillName.Tailoring] = MakeSkillNames("Tailor", DefaultSkillNames),
                [SkillName.AnimalTaming] = MakeSkillNames("Tamer", DefaultSkillNames),
                [SkillName.TasteID] = MakeSkillNames("Taste Tester", DefaultSkillNames),
                [SkillName.Tinkering] = MakeSkillNames("Tinker", DefaultSkillNames),
                [SkillName.Tracking] = MakeSkillNames("Ranger", DefaultSkillNames),
                [SkillName.Veterinary] = MakeSkillNames("Veterinarian", DefaultSkillNames),
                [SkillName.Provocation] = MakeSkillNames("Provoker", DefaultSkillNames),
                [SkillName.Inscribe] = MakeSkillNames("Scribe", DefaultSkillNames),
                [SkillName.Lockpicking] = MakeSkillNames("Locksmith", DefaultSkillNames),
                [SkillName.Magery] = MakeSkillNames("Mage", DefaultSkillNames),
                [SkillName.Snooping] = MakeSkillNames("Pickpocket", DefaultSkillNames),
                [SkillName.Musicianship] = MakeSkillNames("Bard", DefaultSkillNames),
                [SkillName.Poisoning] = MakeSkillNames("Assassin", DefaultSkillNames),
            };
    }
}