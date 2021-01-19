using MessagePack;
using Server.Engines.Magic;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments
{
    [MessagePackObject]
    public class PermPoisonProtection : Enchantment<PermPoisonProtectionInfo>
    {
        [IgnoreMember]
        public override string AffixName =>
            EnchantmentInfo.GetName(IElementalResistible.GetProtectionLevelForResist(Value), Cursed);

        [Key(1)]
        public int Value { get; set; } = 0;
    }
    
    public class PermPoisonProtectionInfo : EnchantmentInfo
    {
        public override string Description { get; protected set; } = "Elemental Poison Protection";
        public override EnchantNameType Place { get; protected set; } = EnchantNameType.Suffix;

        public override string[,] Names { get; protected set; } = {
            {string.Empty, string.Empty},
            {"Lesser Poison Protection", "Adder's Venom"},
            {"Medium Poison Protection", "Cobra's Venom"},
            {"Greater Poison Protection", "Giant Serpent's Venom"},
            {"Deadly Poison Protection", "Silver Serpent's Venom"},
            {"the Snake Handler", "Spider's Venom"},
            {"Poison Absorbsion", "Dread Spider's Venom"},
        };

        public override int Hue { get; protected set; } = 783;
        public override int CursedHue { get; protected set; } = 783;
    }
}