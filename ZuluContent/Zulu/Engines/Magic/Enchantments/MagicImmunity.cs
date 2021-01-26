using MessagePack;
using Server;
using Server.Engines.Magic;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments
{
    [MessagePackObject]
    public class MagicImmunity : Enchantment<MagicImmunityInfo>
    {
        [IgnoreMember]
        public override string AffixName => EnchantmentInfo.GetName(Value, Cursed, CurseLevel);

        [Key(1)] 
        public int Value { get; set; } = 0;

        public override void OnSpellDamage(Mobile attacker, Mobile defender, Spell spell, ElementalType damageType,
            ref int damage)
        {
            if (Value == 0)
            {
                NotifyMobile(defender, "Your magic protection items are out of charges!");
                return;
            }

            if (Value >= (int) spell.Circle)
            {
                Value -= (int) spell.Circle;
                if (Value < 0)
                    Value = 0;

                if (!Cursed)
                {
                    damage = 0;
                    NotifyMobile(defender, "Your items protected you from the magic!");
                    NotifyMobile(defender, attacker, $"The spell dissipates upon contact with {defender.Name}'s magical barrier!");
                }
                else
                    NotifyMobile(defender, "Your items prevent all reflections!");
            }
        }
    }
    
    public class MagicImmunityInfo : EnchantmentInfo
    {

        public override string Description { get; protected set; } = "Magic Protection With Charges";
        public override EnchantNameType Place { get; protected set; } = EnchantNameType.Suffix;
        public override int Hue { get; protected set; } = 0;
        public override int CursedHue { get; protected set; } = 0;

        public override string[,] Names { get; protected set; } = {
            {string.Empty, string.Empty},
            {"Raw Moonstone", "Chipped Moonstone"},
            {"Cut Moonstone", "Cracked Moonstone"},
            {"Refined Moonstone", "Flawed Moonstone"},
            {"Prepared Moonstone", "Inferior Moonstone"},
            {"Enchanted Moonstone", "Chaotic Moonstone"},
            {"Flawless Moonstone", "Corrupted Moonstone"},
        };
    }
}