using MessagePack;
using Server;
using Server.Network;
using Server.Engines.Magic;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments
{
    [MessagePackObject]
    public class PoisonProtection : Enchantment<PoisonProtectionInfo>
    {
        [IgnoreMember]
        public override string AffixName =>
            EnchantmentInfo.GetName(
                Value == 0 ? ElementalProtectionLevel.None : ElementalProtectionLevel.Bane, Cursed, CurseLevel
            );

        [Key(1)]
        public int Value { get; set; } = 0;

        public override void OnPoison(Mobile attacker, Mobile defender, Poison poison, ref bool immune)
        {
            if (Value == 0)
            {
                NotifyMobile(defender, "Your poison protection items are out of charges!");
                return;
            }

            if (Value >= poison.Level)
            {
                Value -= poison.Level;
                if (Value < 0)
                    Value = 0;

                if (!Cursed)
                {
                    immune = true;
                    NotifyMobile(defender, "Your items protected you from the poison!");
                }
                else
                    NotifyMobile(defender, "Your items prevent all poison protection!");
            }
        }
    }
    
    public class PoisonProtectionInfo : EnchantmentInfo
    {
        public override string Description { get; protected set; } = "Poison Protection With Charges";
        public override EnchantNameType Place { get; protected set; } = EnchantNameType.Suffix;

        public override string[,] Names { get; protected set; } = {
            {string.Empty, string.Empty},
            {"Antidotes", "Burning Poison"}
        };

        public override int Hue { get; protected set; } = 0;
        public override int CursedHue { get; protected set; } = 0;
    }
}