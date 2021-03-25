using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Zulu.Engines.Classes;
using Server;
using ZuluContent.Zulu.Engines.Magic.Enums;
using ZuluContent.Zulu.Engines.Magic.Hooks;
using ZuluContent.Zulu.Items;

// ReSharper disable ClassNeverInstantiated.Global
namespace ZuluContent.Zulu.Engines.Magic
{
    public static class EnchantmentHookExtensions
    {
        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
        public static Action<IEnumerable<IEnchantmentHook>, Action<IEnchantmentHook>> Dispatcher { get; set; } =
            EnchantmentHookDispatcher.Dispatch;

        public static void FireHook(this Mobile m, Action<IEnchantmentHook> action)
        {
            var hooks = m.Items
                .OfType<BaseEquippableItem>()
                .SelectMany(e => e.Enchantments.Values.Values)
                .OfType<IEnchantmentHook>()
                .ToList();

            if (m is IEnchanted enchanted)
                hooks.AddRange(enchanted.Enchantments.Values.Values);

            if (m is IZuluClassed {ZuluClass: { }} classed) 
                hooks.Add(classed.ZuluClass);

            Dispatcher(hooks, action);
        }
        
        public static void FireHook<T>(this T enchanted, Action<IEnchantmentHook> action) where T : Mobile, IEnchanted
        {
            (enchanted as Mobile).FireHook(action);
        }

        public static void FireHook(this IEnchanted enchanted, Action<IEnchantmentHook> action)
        {
            Dispatcher(enchanted.Enchantments.Values.Values, action);
        }

        public static void FireHook(this EnchantmentDictionary dictionary, Action<IEnchantmentHook> action)
        {
            Dispatcher(dictionary.Values.Values, action);
        }

        public static void FireHook(this IEnchantmentHook hook, Action<IEnchantmentHook> action)
        {
            Dispatcher(new[] {hook}, action);
        }


    }
}