using System;
using MessagePack;
using Server;
using Server.Engines.Magic;
using ZuluContent.Zulu.Engines.Magic.Enums;
using static Server.Engines.Magic.IElementalResistible;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments
{
    [MessagePackObject]
    public class PermPoisonProtection : Enchantment<PermPoisonProtectionInfo>, IDistinctEnchantment
    {
        [IgnoreMember]
        public override string AffixName =>
            EnchantmentInfo.GetName(IElementalResistible.GetProtectionLevelForResist(Value), Cursed);

        [Key(1)]
        public int Value { get; set; } = 0;

        public override void OnCheckPoisonImmunity(Mobile attacker, Mobile defender, Poison poison, ref bool immune)
        {
            var poisonProtectionLevel = GetProtectionLevelForResist(Value);

            if (Cursed > CurseType.None)
            {
                NotifyMobile(defender, "Your items prevent all poison protection!");
                return;
            }

            if ((int)poisonProtectionLevel >= poison.Level)
            {
                immune = true;
                NotifyMobile(defender, "Your items protected you from the poison!");
            }
        }
        
        public int CompareTo(object obj) => obj switch
        {
            PermPoisonProtection other => ReferenceEquals(this, other) ? 0 : Value.CompareTo(other.Value),
            null => 1,
            _ => throw new ArgumentException($"Object must be of type {GetType().FullName}")
        };
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