using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Server;
using ZuluContent.Zulu.Engines.Magic;

namespace Scripts.Engines.Magic
{
    public class MagicSkillMod : SkillMod, IMagicMod<SkillName>
    {
        public int Color { get; } = 0;
        public int CursedColor { get; } = 0;
        public MagicProp Prop { get; } = MagicProp.Skill;
        public EnchantNameType Place { get; } = EnchantNameType.Prefix;

        public string[] NormalNames
        {
            get
            {
                IMagicMod<SkillName>.NormalToCursedMap.TryGetValue(Target, out var dict);
                return dict?.Keys.ToArray();
            }
        }
        
        public string[] CursedNames
        {
            get
            {
                IMagicMod<SkillName>.NormalToCursedMap.TryGetValue(Target, out var dict);
                return dict?.Values.ToArray();
            }
        }

        public SkillName Target => Skill;
        public bool Cursed => Value < 0;

        public MagicSkillMod(SkillName skill, double value) : base(skill, true, value)
        {
        }

        public void AddTo(Mobile mobile)
        {
            mobile.AddSkillMod(this);
        }

        public override bool CheckCondition() => true;

        private static Dictionary<string, string> MakeNames(string profession)
        {
            return DefaultNormalToCursedMap
                .ToDictionary(
                    kv => $"{kv.Key} {profession}'s",
                    kv => $"{kv.Value} {profession}'s"
                );
        }

        private static readonly Dictionary<string, string> DefaultNormalToCursedMap = new Dictionary<string, string>
        {
            {"Apprentice", "Novice"},
            {"Journeyman", "Neophyte"},
            {"Expert", "Inept"},
            {"Adept", "Incompetent"},
            {"Master", "Failed"},
            {"Grandmaster", "Blundering"},
        };

        static MagicSkillMod()
        {
            IMagicMod<SkillName>.NormalToCursedMap = new Dictionary<SkillName, Dictionary<string, string>>
            {
                [SkillName.Alchemy] = MakeNames("Alchemist"),
                [SkillName.Anatomy] = MakeNames("Physician"),
                [SkillName.AnimalLore] = MakeNames("Naturalist"),
                [SkillName.ItemID] = MakeNames("Merchant"),
                [SkillName.ArmsLore] = MakeNames("Arms Dealer"),
                [SkillName.Parry] = MakeNames("Shield Fighter"),
                [SkillName.Begging] = MakeNames("Beggar"),
                [SkillName.Blacksmith] = MakeNames("Blacksmith"),
                [SkillName.Fletching] = MakeNames("Fletcher"),
                [SkillName.Peacemaking] = MakeNames("Peacemaker"),
                [SkillName.Camping] = MakeNames("Camper"),
                [SkillName.Carpentry] = MakeNames("Carpenter"),
                [SkillName.Cartography] = MakeNames("Cartographer"),
                [SkillName.Cooking] = MakeNames("Chef"),
                [SkillName.DetectHidden] = MakeNames("Scout"),
                [SkillName.Discordance] = MakeNames("Commander"),
                [SkillName.EvalInt] = MakeNames("Scholar"),
                [SkillName.Healing] = MakeNames("Healer"),
                [SkillName.Fishing] = MakeNames("Fisherman"),
                [SkillName.Forensics] = MakeNames("Coroner"),
                [SkillName.Herding] = MakeNames("Shepherd"),
                [SkillName.Lumberjacking] = MakeNames("Lumberjack"),
                [SkillName.Mining] = MakeNames("Miner"),
                [SkillName.Meditation] = MakeNames("Stoic"),
                [SkillName.Stealth] = MakeNames("Spy"),
                [SkillName.RemoveTrap] = MakeNames("Trap Remover"),
                [SkillName.Necromancy] = MakeNames("Necromancer"),
                [SkillName.SpiritSpeak] = MakeNames("Channeler"),
                [SkillName.Stealing] = MakeNames("Thief"),
                [SkillName.Tailoring] = MakeNames("Tailor"),
                [SkillName.AnimalTaming] = MakeNames("Tamer"),
                [SkillName.TasteID] = MakeNames("Taste Tester"),
                [SkillName.Tinkering] = MakeNames("Tinker"),
                [SkillName.Tracking] = MakeNames("Ranger"),
                [SkillName.Veterinary] = MakeNames("Veterinarian"),
                [SkillName.Provocation] = MakeNames("Provoker"),
                [SkillName.Inscribe] = MakeNames("Scribe"),
                [SkillName.Lockpicking] = MakeNames("Locksmith"),
                [SkillName.Magery] = MakeNames("Mage"),
                [SkillName.Snooping] = MakeNames("Pickpocket"),
                [SkillName.Musicianship] = MakeNames("Bard"),
                [SkillName.Poisoning] = MakeNames("Assassin"),
                [SkillName.Hiding] = new Dictionary<string, string>
                {
                    {"Concealing", "Shiny"},
                    {"Camouflaged", "Gleaming"},
                    {"Shadowed's", "Sparkling"},
                    {"Undetectable", "Brilliant"},
                    {"Obscuring", "Illuminating"},
                    {"Obfuscating", "Dazzling"},
                },
                [SkillName.MagicResist] = new Dictionary<string, string>
                {
                    {"Shielded", "Conducting"},
                    {"Warded", "Sensitive"},
                    {"Sanctified", "Focusing"},
                    {"Defiant", "Channeling"},
                    {"Guardian's", "Translucent"},
                    {"Deflecting", "Amplifying"},
                },
                [SkillName.Tactics] = new Dictionary<string, string>
                {
                    {"Fine", "Poor"},
                    {"Superior", "Dull"},
                    {"Superb", "Inferior"},
                    {"Magnificent", "Tainted"},
                    {"Elegant", "Pitiful"},
                    {"Peerless", "Worthless"},
                },
                [SkillName.Archery] = new Dictionary<string, string>
                {
                    {"Large", "Water Damaged"},
                    {"Great", "Crooked"},
                    {"Composite", "Frayed"},
                    {"Archer's", "Warped"},
                    {"Ranger's", "Decaying"},
                    {"Marksman's", "Unstrung"},
                },
                [SkillName.Swords] = new Dictionary<string, string>
                {
                    {"Competitor's", "Unbalanced"},
                    {"Duelist's", "Fragile"},
                    {"Gladiator's", "Rusted"},
                    {"Knight's", "Cracked"},
                    {"Noble's", "Decaying"},
                    {"Arms Master's", "Misshapen"},
                },
                [SkillName.Macing] = new Dictionary<string, string>
                {
                    {"Competitor's", "Unbalanced"},
                    {"Duelist's", "Fragile"},
                    {"Gladiator's", "Rusted"},
                    {"Knight's", "Cracked"},
                    {"Noble's", "Decaying"},
                    {"Arms Master's", "Misshapen"},
                },
                [SkillName.Fencing] = new Dictionary<string, string>
                {
                    {"Competitor's", "Unbalanced"},
                    {"Duelist's", "Fragile"},
                    {"Gladiator's", "Rusted"},
                    {"Knight's", "Cracked"},
                    {"Noble's", "Decaying"},
                    {"Arms Master's", "Misshapen"},
                },
                [SkillName.Wrestling] = new Dictionary<string, string>
                {
                    {"Competitor's", "Unbalanced"},
                    {"Duelist's", "Fragile"},
                    {"Gladiator's", "Rusted"},
                    {"Knight's", "Cracked"},
                    {"Noble's", "Decaying"},
                    {"Arms Master's", "Misshapen"},
                },
            };
        }
    }
}