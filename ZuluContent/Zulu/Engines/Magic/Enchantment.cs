using System;
using MessagePack;
using Server;
using Server.Engines.Magic;
using Server.Items;
using Server.Spells;
using Server.Network;
using ZuluContent.Zulu.Engines.Magic.Enchantments;
using ZuluContent.Zulu.Engines.Magic.Hooks;
using ZuluContent.Zulu.Engines.Magic.Enums;
using static ZuluContent.Zulu.Items.SingleClick.SingleClickHandler;


namespace ZuluContent.Zulu.Engines.Magic
{
    public abstract class Enchantment<TEnchantmentInfo> : IEnchantmentValue
        where TEnchantmentInfo : EnchantmentInfo, new()
    {
        public static readonly TEnchantmentInfo EnchantmentInfo = new TEnchantmentInfo();

        [IgnoreMember]
        public virtual EnchantmentInfo Info => EnchantmentInfo;

        [IgnoreMember]
        public abstract string AffixName { get; }

        [Key(0)]
        public bool Cursed { get; set; }

        [Key(4)]
        public CurseLevelType CurseLevel { get; set; }

        public virtual bool GetShouldDye() => Info.Hue != 0;

        protected Enchantment(bool cursed = false)
        {
            Cursed = cursed;
            if (cursed)
                CurseLevel = CurseLevelType.Unrevealed;
            else
                CurseLevel = CurseLevelType.None;
        }

        protected virtual void NotifyMobile(Mobile above, string text)
        {
            NotifyMobile(above, above, text);
        }
        protected virtual void NotifyMobile(Mobile above, Mobile who, string text)
        {
            above.PrivateOverheadMessage(
                MessageType.Regular,
                who.SpeechHue,
                true,
                text,
                who.NetState
            );
        }

        public virtual void OnIdentified(IEntity entity)
        { 
        }

        public virtual void OnAdded(IEntity entity)
        {
            if (Cursed && CurseLevel == CurseLevelType.Unrevealed && entity is IMagicItem item && item.Parent is Mobile mobile)
            {
                CurseLevel = CurseLevelType.RevealedCantUnEquip;
                mobile.FixedParticles(0x374A, 10, 15, 5028, EffectLayer.Waist);
                mobile.PlaySound(0x1E1);
                mobile.SendAsciiMessage(33, "That item is cursed, and reveals itself to be a " + GetMagicItemName(item));
            }
        }

        public virtual void OnRemoved(IEntity entity, IEntity parent)
        {
        }

        public virtual void OnBeforeRemoved(IEntity entity, Mobile from, ref bool canRemove)
        {
            if (Cursed && CurseLevel == CurseLevelType.RevealedCantUnEquip && entity is Item item && item.Parent is Mobile parent && parent == from)
            {
                canRemove = false;
            }
        }

        public void OnSpellAreaCalculation(Mobile caster, Spell spell, ElementalType damageType, ref double area)
        {
            
        }

        public virtual void OnSpellDamage(Mobile attacker, Mobile defender, Spell spell, ElementalType damageType,
            ref int damage)
        {
        }

        public virtual void OnGetCastDelay(Mobile mobile, Spell spell, ref double delay)
        {
        }

        public virtual void OnParalysis(Mobile mobile, ref TimeSpan duration, ref bool paralyze)
        {
        }

        public virtual void OnPoison(Mobile attacker, Mobile defender, Poison poison, ref bool immune)
        {
        }

        public virtual void OnHeal(Mobile healer, Mobile patient, object source, ref double healAmount)
        {
        }

        public virtual void OnBeforeSwing(Mobile attacker, Mobile defender)
        {
        }

        public virtual void OnSwing(Mobile attacker, Mobile defender, ref double damageBonus, ref TimeSpan result)
        {
        }

        public virtual void OnGetSwingDelay(ref double delayInSeconds, Mobile m)
        {
        }

        public virtual void OnCheckHit(Mobile attacker, Mobile defender, ref bool result)
        {
        }

        public virtual void OnMeleeHit(Mobile attacker, Mobile defender, BaseWeapon weapon, ref int damage)
        {
        }

        public virtual void OnAbsorbMeleeDamage(Mobile attacker, Mobile defender, BaseWeapon weapon, ref int damage)
        {
        }

        public virtual void OnShieldHit(Mobile attacker, Mobile defender, BaseWeapon weapon, BaseShield shield, ref int damage)
        {
        }

        public virtual void OnArmorHit(Mobile attacker, Mobile defender, BaseWeapon weapon, BaseArmor armor, ref int damage)
        {
        }
    }
}