using System;
using MessagePack;
using Server;
using Server.Spells;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs
{
    public class Agility : DexBonus, IBuff
    {
        [IgnoreMember] public BuffIcon Icon => Value > 0 ? BuffIcon.Agility : BuffIcon.Clumsy;
        [IgnoreMember] public int TitleCliloc => Value > 0 ? 1075841 : 1075831; // Agility : Clumsy
        [IgnoreMember] public int SecondaryCliloc { get; } = IBuff.BlankCliloc;
        [IgnoreMember] public TextDefinition Args => Value.ToString("+0;-#") + " dexterity";
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