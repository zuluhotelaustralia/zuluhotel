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
                Value == 0 ? ElementalProtectionLevel.None : ElementalProtectionLevel.Bane, Cursed
            );

        [Key(1)]
        public int Value { get; set; } = 0;

        public override void OnPoison(Mobile attacker, Mobile defender, Poison poison, ref ApplyPoisonResult result)
        {
            if (Value == 0)
            {
                defender.PrivateOverheadMessage(
                    MessageType.Regular,
                    defender.SpeechHue,
                    true,
                    "Your poison protection items are out of charges!",
                    defender.NetState
                );
                return;
            }

            if (Value >= poison.Level)
            {
                result = ApplyPoisonResult.Immune;
                Value--;
                defender.PrivateOverheadMessage(
                    MessageType.Regular,
                    defender.SpeechHue,
                    true,
                    "Your items protected you from the poison!",
                    defender.NetState
                );
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