using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FastExpressionCompiler;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;
using Server;
using ZuluContent.Zulu.Engines.Magic.Enchantments;
using ZuluContent.Zulu.Engines.Magic.Enums;
using ZuluContent.Zulu.Engines.Magic.Hooks;
using ZuluContent.Zulu.Items;

// ReSharper disable ClassNeverInstantiated.Global
namespace ZuluContent.Zulu.Engines.Magic
{
    public static class EnchantmentHooks
    {
        public static void FireHook(this Mobile m, Action<IEnchantmentHook> action)
        {
            var hooks = new List<IEnchantmentHook>(
                m.Items
                    .OfType<BaseEquippableItem>()
                    .SelectMany(e => e.Enchantments.Values.Values)
                    .ToList()
            );

            if (m is IEnchanted enchanted)
                hooks.AddRange(enchanted.Enchantments.Values.Values);

            if (m is IZuluClassed {ZuluClass: { }} classed)
                hooks.Add(classed.ZuluClass);

            Fire(hooks, action);
        }
        
        public static void FireHook(this IEnchanted enchanted, Action<IEnchantmentHook> action)
        {
            Fire(enchanted.Enchantments.Values.Values, action);
        }

        public static void FireHook(this EnchantmentDictionary dictionary, Action<IEnchantmentHook> action)
        {
            Fire(dictionary.Values.Values, action);
        }

        private static void Fire(IEnumerable<IEnchantmentHook> values, Action<IEnchantmentHook> action)
        {
            var filtered = values
                .Where(x => !(x is MagicImmunity || x is PoisonProtection || x is SpellReflect))
                .ToList();


            var magicImmunityHook = values
                .Where(x => x is MagicImmunity {Value: > 0})
                .Cast<MagicImmunity>()
                .OrderByDescending(x => x.Value)
                .FirstOrDefault();

            var poisonProtectionHook = values
                .Where(x => x is PoisonProtection {Value: > 0})
                .Cast<PoisonProtection>()
                .OrderByDescending(x => x.Value)
                .FirstOrDefault();

            var spellReflectHook = values
                .Where(x => x is SpellReflect {Value: > 0})
                .Cast<SpellReflect>()
                .OrderByDescending(x => x.Value)
                .FirstOrDefault();

            if (magicImmunityHook != null)
                filtered.Add(magicImmunityHook);

            if (poisonProtectionHook != null)
                filtered.Add(poisonProtectionHook);

            if (spellReflectHook != null)
                filtered.Add(spellReflectHook);
            
            action(EnchantmentHookDispatcher.Create(filtered));
        }
    }
}