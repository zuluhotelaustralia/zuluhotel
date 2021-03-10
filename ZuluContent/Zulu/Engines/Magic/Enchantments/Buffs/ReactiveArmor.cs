using System;
using MessagePack;
using Server;
using Server.Items;
using Server.Spells;
using Server.Spells.First;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs
{
    [MessagePackObject]
    public class ReactiveArmor : Enchantment<ReactiveArmorInfo>, IBuff
    {
        [IgnoreMember] public override string AffixName => string.Empty;

        [Key(1)]
        public int Value { get; set; } = 0;

        public override void OnAbsorbMeleeDamage(Mobile attacker, Mobile defender, BaseWeapon weapon, ref int damage)
        {
            if (!attacker.Alive || !defender.Alive || Value <= 0 || damage <= 0)
                return;

            var reflected = damage / 2.0;
            attacker.Damage((int) reflected, defender);

            attacker.FixedEffect(0x3749, 10, 10);
            attacker.PlaySound(0x1F1);

            Value--;

            if (defender is IBuffable buffable)
            {
                if(Value <= 0)
                    buffable.BuffManager.RemoveBuff(this);
                else
                    buffable.BuffManager.ResendBuff(this);  
            }
        }

        #region IBuff
        [IgnoreMember] public BuffIcon Icon { get; } = BuffIcon.ReactiveArmor;
        [IgnoreMember] public int TitleCliloc { get; } = 1075812; // Reactive Armor
        [IgnoreMember] public int SecondaryCliloc { get; } = 1076207; // Remaining Charges: ~1_val~
        [IgnoreMember] public TextDefinition Args => $"{Value}";
        [IgnoreMember] public bool RetainThroughDeath { get; } = false;
        [IgnoreMember] public bool Dispellable { get; } = true;
        [IgnoreMember] public TimeSpan Duration { get; init; }
        [IgnoreMember] public DateTime Start { get; init; } = DateTime.UtcNow;
        
        public void OnBuffAdded(Mobile parent)
        {
            (parent as IEnchanted)?.Enchantments.Set(this);
        }

        public void OnBuffRemoved(Mobile parent)
        {
            if (parent is IEnchanted enchanted)
            {
                enchanted.Enchantments.Remove(this);
                parent.SendLocalizedMessage(1005556); // Your reactive armor spell has been nullified.
            }
        }
        
        #endregion
    }
    
    #region EnchantmentInfo
    public class ReactiveArmorInfo : EnchantmentInfo
    {
        public override string Description { get; protected set; } = "Reactive Armor";
        public override EnchantNameType Place { get; protected set; } = EnchantNameType.Prefix;
        public override int Hue { get; protected set; } = 0;
        public override int CursedHue { get; protected set; } = 0;
        public override string[,] Names { get; protected set; } = { };

        public override string GetName(int index, CurseType curse = CurseType.None)
        {
            return string.Empty;
        }
    }
    #endregion
}