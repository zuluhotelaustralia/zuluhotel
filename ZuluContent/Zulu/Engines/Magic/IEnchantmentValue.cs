using System;
using MessagePack;
using ZuluContent.Zulu.Engines.Magic.Enchantments;
using ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs;
using ZuluContent.Zulu.Engines.Magic.Enums;
using ZuluContent.Zulu.Engines.Magic.Hooks;

namespace ZuluContent.Zulu.Engines.Magic
{
    [Union(0, typeof(AirProtection))]
    [Union(1, typeof(ArmorBonus))]
    [Union(2, typeof(ArmorProtection))]
    [Union(3, typeof(DurabilityBonus))]
    [Union(4, typeof(EarthProtection))]
    [Union(5, typeof(FireProtection))]
    [Union(6, typeof(HealingBonus))]
    [Union(7, typeof(ItemQuality))]
    [Union(8, typeof(MagicalWeapon))]
    [Union(9, typeof(MeditationAllowance))]
    [Union(10, typeof(NecroProtection))]
    [Union(11, typeof(ParalysisProtection))]
    [Union(12, typeof(MagicImmunity))]
    [Union(13, typeof(PhysicalProtection))]
    [Union(14, typeof(PoisonProtection))]
    [Union(15, typeof(SkillBonus))]
    [Union(16, typeof(IntBonus))]
    [Union(17, typeof(DexBonus))]
    [Union(18, typeof(StrBonus))]
    [Union(19, typeof(WaterProtection))]
    [Union(20, typeof(WeaponAccuracyBonus))]
    [Union(21, typeof(WeaponDamageBonus))]
    [Union(22, typeof(FirstSkillBonus))]
    [Union(23, typeof(SecondSkillBonus))]
    [Union(24, typeof(SpellHit))]
    [Union(25, typeof(SlayerHit))]
    [Union(26, typeof(EffectHit))]
    [Union(27, typeof(PoisonHit))]
    [Union(28, typeof(MagicReflection))]
    [Union(29, typeof(MagicEfficiencyPenalty))]
    [Union(30, typeof(HarvestBonus))]
    [Union(31, typeof(ItemMark))]
    [Union(32, typeof(ItemFortification))]
    [Union(33, typeof(ReactiveArmor))]
    [Union(34, typeof(StatBuff))]
    [Union(35, typeof(ArmorBuff))]
    [Union(36, typeof(MagicReflectionBuff))]
    [Union(37, typeof(Incognito))]
    [Union(38, typeof(Invisibility))]
    [Union(39, typeof(Polymorph))]
    [Union(40, typeof(PoisonImmunity))]
    [Union(41, typeof(NightSight))]
    [Union(42, typeof(WraithForm))]
    [Union(43, typeof(DeathPardon))]
    public interface IEnchantmentValue : IEnchantmentHook
    {
        public EnchantmentInfo Info { get; }
        public string AffixName { get; }
        public CurseType Cursed { get; set; }
        bool GetShouldDye();
    }
}