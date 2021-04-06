using System;
using System.Collections.Generic;
using System.Linq;
using Server;
using Server.Engines.Magic;
using Server.Mobiles;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic.Enchantments;
using ZuluContent.Zulu.Engines.Magic.Enums;
using ZuluContent.Zulu.Engines.Magic.Hooks;
using ZuluContent.Zulu.Items;

namespace ZuluContent.Zulu.Engines.Magic
{
    public static class EnchantmentExtensions
    {
        public static List<IEnchantmentValue> GetAllEnchantments(this Mobile mobile, bool includeEquipped = true) =>
            GetAllEnchantmentsOfType<IEnchantmentValue>(mobile, includeEquipped);

        public static List<T> GetAllEnchantmentsOfType<T>(this Mobile mobile, bool includeEquipped = true)
        {
            var values = new List<T>();

            if (includeEquipped)
            {
                values.AddRange(
                    mobile.Items
                        .OfType<BaseEquippableItem>()
                        .SelectMany(e => e.Enchantments.Values.Values)
                        .OfType<T>()
                );
            }

            if (mobile is IEnchanted enchanted)
                values.AddRange(enchanted.Enchantments.Values.Values.OfType<T>());

            return values;
        }

        public static T GetDistinctEnchantment<T>(this IEnchanted enchanted, bool includeEquipped = true)
            where T : IEnchantmentValue, IEnchantmentHook, IDistinctEnchantment
        {
            var enchantments = enchanted is Mobile mobile && includeEquipped
                ? GetAllEnchantmentsOfType<T>(mobile)
                : enchanted.Enchantments.Values.Values.OfType<T>();

            return enchantments.OrderByDescending(x => x).FirstOrDefault();
        }

        public static T GetDistinctEnchantment<T>(this Mobile mobile, bool includeEquipped = true)
            where T : IEnchantmentValue, IEnchantmentHook, IDistinctEnchantment =>
            mobile is IEnchanted enchanted ? enchanted.GetDistinctEnchantment<T>(includeEquipped) : default;

        public static T GetDistinctEnchantment<T, TU>(this TU mobile, bool includeEquipped = true)
            where T : IEnchantmentValue, IEnchantmentHook, IDistinctEnchantment
            where TU : Mobile, IEnchanted =>
            ((IEnchanted) mobile).GetDistinctEnchantment<T>(includeEquipped);

        public static bool TrySetResist(this IEnchanted enchanted, ElementalType type, int value)
        {
            switch (type)
            {
                case ElementalType.Water:
                    enchanted.Enchantments.Set((WaterProtection e) => e.Value = value);
                    break;
                case ElementalType.Air:
                    enchanted.Enchantments.Set((AirProtection e) => e.Value = value);
                    break;
                case ElementalType.Physical:
                    enchanted.Enchantments.Set((PhysicalProtection e) => e.Value = value);
                    break;
                case ElementalType.Fire:
                    enchanted.Enchantments.Set((FireProtection e) => e.Value = value);
                    break;
                case ElementalType.Earth:
                    enchanted.Enchantments.Set((EarthProtection e) => e.Value = value);
                    break;
                case ElementalType.Necro:
                    enchanted.Enchantments.Set((NecroProtection e) => e.Value = value);
                    break;
                case ElementalType.Paralysis:
                    enchanted.Enchantments.Set((ParalysisProtection e) => e.Value = value);
                    break;
                case ElementalType.HealingBonus:
                    enchanted.Enchantments.Set((HealingBonus e) => e.Value = value);
                    break;
                case ElementalType.Poison:
                    enchanted.Enchantments.Set((PoisonProtection e) => e.Value = (PoisonLevel) value);
                    break;
                case ElementalType.MagicImmunity:
                    enchanted.Enchantments.Set((MagicImmunity e) => e.Value = (SpellCircle) value);
                    break;
                case ElementalType.MagicReflection:
                    enchanted.Enchantments.Set((MagicReflection e) => e.Value = (SpellCircle) value);
                    break;
                case ElementalType.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            return false;
        }

        public static bool TrySetResist(this IEnchanted enchanted, ElementalType type,
            ElementalProtectionLevel level) =>
            enchanted.TrySetResist(type,
                (type == ElementalType.Poison || type == ElementalType.MagicImmunity ||
                 type == ElementalType.MagicReflection)
                    ? (int) level
                    : IElementalResistible.GetResistForProtectionLevel(level));

        public static bool TrySetResist(this Mobile mobile, ElementalType type, ElementalProtectionLevel level) =>
            mobile is IEnchanted enchanted && TrySetResist(enchanted, type, level);

        public static bool TrySetResist<T>(this T mobile, ElementalType type, ElementalProtectionLevel level)
            where T : Mobile, IEnchanted => TrySetResist((IEnchanted) mobile, type, level);

        public static int GetResist(this IEnchanted ench, ElementalType type) => type switch
        {
            ElementalType.Water => ench.GetDistinctEnchantment<WaterProtection>()?.Value ?? 0,
            ElementalType.Air => ench.GetDistinctEnchantment<AirProtection>()?.Value ?? 0,
            ElementalType.Physical => ench.GetDistinctEnchantment<PhysicalProtection>()?.Value ?? 0,
            ElementalType.Fire => ench.GetDistinctEnchantment<FireProtection>()?.Value ?? 0,
            ElementalType.Earth => ench.GetDistinctEnchantment<EarthProtection>()?.Value ?? 0,
            ElementalType.Necro => ench.GetDistinctEnchantment<NecroProtection>()?.Value ?? 0,
            ElementalType.Paralysis => ench.GetDistinctEnchantment<ParalysisProtection>()?.Value ?? 0,
            ElementalType.HealingBonus => ench.GetDistinctEnchantment<HealingBonus>()?.Value ?? 0,
            ElementalType.Poison => (int) (ench.GetDistinctEnchantment<PoisonProtection>()?.Value ?? 0),
            ElementalType.MagicImmunity => (int) (ench.GetDistinctEnchantment<MagicImmunity>()?.Value ?? 0),
            ElementalType.MagicReflection => (int) (ench.GetDistinctEnchantment<MagicReflection>()?.Value ?? 0),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

        public static int GetResist(this Mobile mobile, ElementalType type) =>
            mobile is IEnchanted enchanted ? GetResist(enchanted, type) : 0;

        public static int GetResist<T>(this T creature, ElementalType type) where T : Mobile, IEnchanted
            => GetResist((IEnchanted) creature, type);

        public static int SetResistCharges(this IEnchanted enchanted, ElementalType type, int value) => type switch
        {
            ElementalType.Poison => enchanted.Enchantments.Set((PoisonProtection e) => e.Charges = value),
            ElementalType.MagicImmunity => enchanted.Enchantments.Set((MagicImmunity e) => e.Charges = value),
            ElementalType.MagicReflection => enchanted.Enchantments.Set((MagicReflection e) => e.Charges = value),
            ElementalType.None => 0,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

        public static int GetResistCharges(this IEnchanted enchanted, ElementalType type) => type switch
        {
            ElementalType.Poison => enchanted.Enchantments.Get((PoisonProtection e) => e.Charges),
            ElementalType.MagicImmunity => enchanted.Enchantments.Get((MagicImmunity e) => e.Charges),
            ElementalType.MagicReflection => enchanted.Enchantments.Get((MagicReflection e) => e.Charges),
            ElementalType.None => 0,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}