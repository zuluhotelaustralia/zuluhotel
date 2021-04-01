using System;
using MessagePack;
using Server;
using Server.Spells;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs
{
    [MessagePackObject]
    public class MagicReflectionBuff : MagicReflection, IBuff
    {
        // We should use this first before checking item enchantments
        [CallPriority(1)]
        public override void OnCheckMagicReflection(Mobile parent, Spell spell, ref bool reflected)
        {
            if (parent != spell?.Caster && spell is ITargetableAsyncSpell<Mobile> && !reflected)
            {
                Charges--;
                reflected = true;
            }

            if (Charges <= 0)
                (parent as IBuffable)?.BuffManager.RemoveBuff(this);
        }

        #region IBuff
        [IgnoreMember] public BuffIcon Icon { get; init; } = BuffIcon.MagicReflection;
        [IgnoreMember] public string Title { get; init; } = "Magic Reflection";
        [IgnoreMember] public string Description { get; init; } = "Reflects the next harmful spell";
        [IgnoreMember] public string[] Details { get; init; }
        [IgnoreMember] public bool ExpireOnDeath { get; init; } = true;
        [IgnoreMember] public bool Dispellable { get; init; } = false;
        [IgnoreMember] public TimeSpan Duration { get; init; }
        [IgnoreMember] public DateTime Start { get; init; }

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
}