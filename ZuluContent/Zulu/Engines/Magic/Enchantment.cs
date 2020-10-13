using System;
using MessagePack;
using Server;
using ZuluContent.Zulu.Engines.Magic.Enchantments;
using ZuluContent.Zulu.Engines.Magic.Hooks;

namespace ZuluContent.Zulu.Engines.Magic
{
    public abstract class Enchantment<TEnchantmentInfo> : IEnchantmentValue
        where TEnchantmentInfo : EnchantmentInfo, new()
    {
        public static readonly TEnchantmentInfo EnchantmentInfo = new TEnchantmentInfo();
        [IgnoreMember] public virtual EnchantmentInfo Info => EnchantmentInfo;

        [IgnoreMember] public abstract string AffixName { get; }
        [Key(0)] public bool Cursed { get; set; }

        public virtual bool GetShouldDye() => Info.Hue != 0;

        protected Enchantment(bool cursed = false)
        {
            Cursed = cursed;
        }

        public virtual void OnIdentified(Item item) { }

        public virtual void OnAdded(Item item, Mobile mobile) { }

        public virtual void OnRemoved(Item item, Mobile mobile) { }

        public virtual void OnBeforeSwing(Mobile attacker, Mobile defender) { }

        public virtual void OnSwing(Mobile attacker, Mobile defender, ref double damageBonus, ref TimeSpan result) { }

        public virtual void OnGetDelay(ref TimeSpan delay, Mobile m) { }

        public virtual void OnCheckHit(ref bool result, Mobile attacker, Mobile defender) { }

        public virtual void OnGotMeleeAttack(ref double damage, Item item, Mobile mobile) { }

        public virtual void OnGaveMeleeAttack(ref double damage, Item item, Mobile mobile) { }
    }
}