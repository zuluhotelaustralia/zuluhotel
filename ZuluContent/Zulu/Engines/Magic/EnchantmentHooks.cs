using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Server;
using ZuluContent.Zulu.Engines.Magic.Enums;
using ZuluContent.Zulu.Engines.Magic.Hooks;
using ZuluContent.Zulu.Items;

namespace ZuluContent.Zulu.Engines.Magic
{
    public static class EnchantmentHooks
    {
        /**
         * 
         */
        public static readonly Dictionary<string, Dictionary<Type, int>> HookPriorities;

        public static readonly Dictionary<string, Action<IEnchantmentHook>> CachedActions =
            new Dictionary<string, Action<IEnchantmentHook>>();

        static EnchantmentHooks()
        {
            HookPriorities = EnchantmentDictionary.EnchantmentInfoMap.Keys
                .SelectMany(t =>
                {
                    var map = t.GetInterfaceMap(typeof(IEnchantmentHook));
                    return map
                        .TargetMethods
                        .Where(m => !(
                                m.DeclaringType?.IsGenericType == true &&
                                m.DeclaringType.GetGenericTypeDefinition() == typeof(Enchantment<>)
                            )
                        )
                        .Select(m => (InterfaceMethod: map.InterfaceMethods[Array.IndexOf(map.TargetMethods, m)],
                            HookMethod: m));
                })
                .GroupBy(x => x.InterfaceMethod)
                .ToDictionary(
                    x => x.Key.Name,
                    x => x.Select(y => (T: y.HookMethod?.ReflectedType, P: y.HookMethod.GetActionPriority()))
                        .OrderBy(kv => kv.P)
                        .ToDictionary(k => k.T, v => v.P)
                );

            typeof(Enchantment<>)
                .GetInterfaceMap(typeof(IEnchantmentHook))
                .InterfaceMethods
                .Where(m => !HookPriorities.ContainsKey(m.Name))
                .ToList()
                .ForEach(m => HookPriorities.Add(m.Name, new Dictionary<Type, int>()));
        }

        private static int GetActionPriority(this ICustomAttributeProvider m)
        {
            return m.GetCustomAttributes(true)
                .OfType<CallPriorityAttribute>()
                .OrderBy(x => x.Priority)
                .FirstOrDefault()?.Priority ?? int.MaxValue;
        }


        public static void FireHook(
            this Mobile m,
            Expression<Action<IEnchantmentHook>> func
        )
        {
            Fire(
                m.Items
                    .OfType<BaseEquippableItem>()
                    .SelectMany(e => e.Enchantments.Values.Values),
                func
            );
        }


        public static void FireHook(
            this EnchantmentDictionary dictionary,
            Expression<Action<IEnchantmentHook>> action
        )
        {
            Fire(dictionary.Values.Values, action);
        }

        private static void Fire(
            IEnumerable<IEnchantmentHook> values,
            Expression<Action<IEnchantmentHook>> expr
        )
        {

            // TODO: expr.ToString() should be an comparer using a hashing function
            if (!CachedActions.TryGetValue(expr.ToString(), out var action)) 
                CachedActions.Add(expr.ToString(), action = expr.Compile());

            var methodCallExpr = (MethodCallExpression) expr.Body;

            values.OrderBy(kv =>
                    HookPriorities.ContainsKey(methodCallExpr.Method.Name) &&
                    HookPriorities[methodCallExpr.Method.Name].TryGetValue(kv.GetType(), out var priority)
                        ? priority
                        : -1
                )
                .ToList()
                .ForEach(action);
        }
    }
}