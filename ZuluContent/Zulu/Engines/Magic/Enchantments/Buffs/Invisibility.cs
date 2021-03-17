using System;
using MessagePack;
using Server;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs
{
    [MessagePackObject]
    public class Invisibility : Enchantment<InvisibilityInfo>, IBuff
    {
        private string m_Description;
        private int m_Steps = 0;
        [IgnoreMember] public override string AffixName => string.Empty;

        [Key(1)]
        public int Steps
        {
            get => m_Steps;
            set
            {
                m_Description = $"Remaining steps: {value}";
                m_Steps = value;
            }
        }

        #region IBuff

        [IgnoreMember] public BuffIcon Icon { get; init; } = BuffIcon.HidingAndOrStealth;
        [IgnoreMember] public string Title { get; init; } = "Hidden";

        [IgnoreMember]
        public string Description
        {
            get => m_Description ??= Steps > 0 ? $"Remaining steps: {Steps}" : "Cloaked in the shadows";
            init => m_Description = value;
        }

        [IgnoreMember] public string[] Details { get; init; }
        [IgnoreMember] public bool ExpireOnDeath { get; init; } = true;
        [IgnoreMember] public bool Dispellable { get; init; } = false;
        [IgnoreMember] public TimeSpan Duration { get; init; }
        [IgnoreMember] public DateTime Start { get; init; }

        public override void OnMove(Mobile mobile, Direction direction, ref bool canMove)
        {
            // Mobile.OnMove will decrement AllowedStealthSteps
            Steps = mobile.AllowedStealthSteps;
            
            if (mobile is IBuffable buffable)
            {
                if (Steps <= 0)
                    buffable.BuffManager.RemoveBuff(this);
                else
                    buffable.BuffManager.ResendBuff(this);
            }
        }

        public override void OnHiddenChanged(Mobile mobile)
        {
            if (!mobile.Hidden)
                (mobile as IBuffable)?.BuffManager.RemoveBuff(this);
        }

        public void OnBuffAdded(Mobile parent)
        {
            parent.Hidden = true;
            parent.AllowedStealthSteps = Steps;
            (parent as IEnchanted)?.Enchantments.Set(this);
        }

        public void OnBuffRemoved(Mobile parent)
        {
            parent.Hidden = false;
            if (parent is IEnchanted enchanted)
                enchanted.Enchantments.Remove(this);
        }

        #endregion
    }

    #region EnchantmentInfo

    public class InvisibilityInfo : EnchantmentInfo
    {
        public override string Description { get; protected set; } = "Invisibility";
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