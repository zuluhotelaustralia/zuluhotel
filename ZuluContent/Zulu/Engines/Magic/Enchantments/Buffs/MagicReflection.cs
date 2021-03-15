using System;
using MessagePack;
using Server;
using Server.Items;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs
{
    [MessagePackObject]
    public class MagicReflection : Enchantment<MagicReflectionInfo>, IBuff
    {
        private string m_Description;
        [IgnoreMember] public override string AffixName => string.Empty;

        [Key(1)] public int Value { get; set; } = 1;

        #region IBuff

        [IgnoreMember] public BuffIcon Icon { get; init; } = BuffIcon.MagicReflection;
        [IgnoreMember] public string Title { get; init; } = "Magic Reflection";

        [IgnoreMember]
        public string Description
        {
            get => m_Description ??= "Reflects the next harmful spell";
            init => m_Description = value;
        }

        [IgnoreMember] public string[] Details { get; init; }
        [IgnoreMember] public bool ExpireOnDeath { get; init; } = true;
        [IgnoreMember] public bool Dispellable { get; init; } = false;
        [IgnoreMember] public TimeSpan Duration { get; init; }
        [IgnoreMember] public DateTime Start { get; init; }
        
        // We should use this first before checking item enchantments
        [CallPriority(1)]
        public override void OnCheckMagicReflection(Mobile parent, Spell spell, ref bool reflected)
        {
            if (parent != spell?.Caster && spell is ITargetableAsyncSpell<Mobile> && !reflected)
            {
                Value--;
                reflected = true;
            }
            
            if(Value <= 0)
                (parent as IBuffable)?.BuffManager.RemoveBuff(this);
        }

        public void OnBuffAdded(Mobile parent)
        {
            (parent as IEnchanted)?.Enchantments.Set(this);
        }

        public void OnBuffRemoved(Mobile parent)
        {
            if (parent is IEnchanted enchanted)
                enchanted.Enchantments.Remove(this);
        }

        #endregion
    }

    #region EnchantmentInfo

    public class MagicReflectionInfo : EnchantmentInfo
    {
        public override string Description { get; protected set; } = "Magic Reflection";
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