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
        private string m_Description;
        private int m_Value;
        [IgnoreMember] public override string AffixName => string.Empty;

        [Key(1)]
        public int Value
        {
            get => m_Value;
            set
            {
                m_Description = $"Remaining charges: {Value}";
                m_Value = value;
            }
        }

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
                if (Value <= 0)
                    buffable.BuffManager.RemoveBuff(this);
                else
                    buffable.BuffManager.ResendBuff(this);
            }
        }

        #region IBuff

        [IgnoreMember] public BuffIcon Icon { get; init; } = BuffIcon.ReactiveArmor;
        [IgnoreMember] public string Title { get; init; } = "Reactive Armor";
        [IgnoreMember]
        public string Description
        {
            get => m_Description;
            init => m_Description = value;
        }

        [IgnoreMember] public string[] Details { get; init; }
        [IgnoreMember] public bool ExpireOnDeath { get; init; } = true;
        [IgnoreMember] public bool Dispellable { get; init; } = true;
        [IgnoreMember] public TimeSpan Duration { get; init; } = TimeSpan.Zero;
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