using System;
using MessagePack;
using Server;
using Server.Spells;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs
{
    public class Strength : StrBonus, IBuff
    {
        [IgnoreMember] public BuffIcon Icon => Value > 0 ? BuffIcon.Strength : BuffIcon.Weaken;
        [IgnoreMember] public int TitleCliloc => Value > 0 ? 1075845 : 1075837; // Strength : Weaken
        [IgnoreMember] public int SecondaryCliloc { get; } = IBuff.BlankCliloc;
        [IgnoreMember] public TextDefinition Args => Value.ToString("+0;-#") + " strength";
        [IgnoreMember] public bool RetainThroughDeath { get; } = false;
        [IgnoreMember] public bool Dispellable { get; } = true;
        [IgnoreMember] public TimeSpan Duration { get; init; }
        [IgnoreMember] public DateTime Start { get; init; } = DateTime.UtcNow;
        
        public void OnBuffAdded(Mobile parent)
        {
            (parent as IEnchanted)?.Enchantments.Set(this);
            OnAdded(parent);
        }

        public void OnBuffRemoved(Mobile parent)
        {
            OnRemoved(parent, parent);
            (parent as IEnchanted)?.Enchantments.Remove(this);
        }
    }
}