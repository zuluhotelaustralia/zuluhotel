using System;
using MessagePack;
using Server;
using Server.Spells;
using Server.Spells.Second;
using ZuluContent.Zulu.Engines.Magic.Enums;
using static Server.Engines.Magic.IElementalResistible;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments
{
    [MessagePackObject]
    public class HealingBonus : Enchantment<HealingBonusInfo>, IDistinctEnchantment
    {
        [IgnoreMember] 
        public override string AffixName => EnchantmentInfo.GetName(Value, Cursed);
        [Key(1)] 
        public int Value { get; set; } = 0;
        
        [CallPriority(1)]
        public override bool GetShouldDye() => Value > 0;

        public override void OnHeal(Mobile healer, Mobile patient, object source, ref double healAmount)
        {
            var healingBonusLevel = GetProtectionLevelForResist(Value);
            var healDelta = healAmount * (int) healingBonusLevel * 0.1;
            if (Cursed > CurseType.None)
                healAmount -= healDelta;
            else
                healAmount += healDelta;
        }

        public override void OnCure(Mobile caster, Mobile target, Poison poison, object source, ref double difficulty)
        {
            if(source is CureSpell)
                difficulty -= Value * 10;
        }

        public int CompareTo(object obj) => obj switch
        {
            HealingBonus other => ReferenceEquals(this, other) ? 0 : Value.CompareTo(other.Value),
            null => 1,
            _ => throw new ArgumentException($"Object must be of type {GetType().FullName}")
        };
    }
    public class HealingBonusInfo : EnchantmentInfo
    {

        public override string Description { get; protected set; } = "Healing Bonus";
        public override EnchantNameType Place { get; protected set; } = EnchantNameType.Suffix;
        public override int Hue { get; protected set; } = 1182;
        public override int CursedHue { get; protected set; } = 0;

        public override string[,] Names { get; protected set; } = {
            {string.Empty, string.Empty},
            {"Relief", "Wounds"},
            {"Respite", "Bruises"},
            {"Rest", "Deterioration"},
            {"Regeneration", "Festering"},
            {"Healing", "Atrophy"},
            {"Nature's Blessing", "Blight"}
        };
    }
}