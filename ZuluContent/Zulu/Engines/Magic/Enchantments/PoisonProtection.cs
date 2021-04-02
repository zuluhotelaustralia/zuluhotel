using System;
using MessagePack;
using Server;
using Server.Engines.Magic;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments
{
    [MessagePackObject]
    public class PoisonProtection : Enchantment<PoisonProtectionInfo>, IDistinctEnchantment
    {
        [IgnoreMember] public override string AffixName => Info.GetName(Value, Cursed);

        [IgnoreMember] public override EnchantmentInfo Info => Charges != int.MaxValue
            ? PoisonProtectionInfo.ChargedPoisonProtection
            : PoisonProtectionInfo.PermPoisonProtection;

        [Key(1)] public PoisonLevel Value { get; set; } = 0;

        [Key(2)] public int Charges { get; set; } = int.MaxValue;

        public override void OnCheckPoisonImmunity(Mobile attacker, Mobile defender, Poison poison, ref bool immune)
        {
            if (Cursed > CurseType.None)
            {
                NotifyMobile(defender, "Your items prevent all poison protection!");
                return;
            }

            if (Charges != int.MaxValue && (int)Value >= poison.Level || Charges >= poison.Level)
            {
                if (Charges != int.MaxValue)
                {
                    Charges -= poison.Level;
                    if(Charges <= 0)
                        NotifyMobile(defender, "One of your poison protection items has run out of charges!");
                }
                
                // Mobile.OnPoisonImmunity will send the cliloc: * The poison seems to have no effect. *
                immune = true;
            }
        }
        
        public int CompareTo(object obj) => obj switch
        {
            PoisonProtection other => ReferenceEquals(this, other) ? 0 : Value.CompareTo(other.Value),
            null => 1,
            _ => throw new ArgumentException($"Object must be of type {GetType().FullName}")
        };
    }
    
    public class PoisonProtectionInfo : EnchantmentInfo
    {
        public static readonly PoisonProtectionInfo PermPoisonProtection = new()
        {
            Description = "Elemental Poison Protection",
            Hue = 783,
            CursedHue = 783,
            Names = new[,] {
                {string.Empty, string.Empty},
                {"Lesser Poison Protection", "Adder's Venom"},
                {"Medium Poison Protection", "Cobra's Venom"},
                {"Greater Poison Protection", "Giant Serpent's Venom"},
                {"Deadly Poison Protection", "Silver Serpent's Venom"},
                {"the Snake Handler", "Spider's Venom"},
                {"Poison Absorbsion", "Dread Spider's Venom"},
            },
        };
        
        public static readonly PoisonProtectionInfo ChargedPoisonProtection = new()
        {
            Description = "Poison Protection With Charges",
            Hue = 0,
            CursedHue = 0,
            Names = new[,] {
                {"Antidotes", "Burning Poison"}
            }
        };
        
        public override string Description { get; protected set; }
        public override EnchantNameType Place { get; protected set; } = EnchantNameType.Suffix;
        public override string[,] Names { get; protected set; }
        public override int Hue { get; protected set; }
        public override int CursedHue { get; protected set; }
    }
}