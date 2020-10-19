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

        public static readonly Dictionary<string, Delegate> CachedActions =
            new Dictionary<string, Delegate>();

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

        private static IOrderedEnumerable<IEnchantmentValue> OrderByPriority(this IEnumerable<IEnchantmentValue> values,
            string methodName)
        {
            return values.OrderBy(kv =>
                HookPriorities.ContainsKey(methodName) &&
                HookPriorities[methodName].TryGetValue(kv.GetType(), out var priority)
                    ? priority
                    : -1
            );
        }

        private static T GetDelegate<T>(Expression<T> expr) where T : Delegate
        {
            return expr.Compile();
            // TODO: expr.ToString() should be an comparer using a hashing function
            // var exprKey = expr.ToString();
            //
            // if (CachedActions.ContainsKey(exprKey))
            //     return (T) CachedActions[exprKey];
            //
            // var func = expr.Compile();
            // CachedActions.Add(expr.ToString(), func);
            // return func;
        }


        public static void FireHook(this Mobile m, Expression<Action<IEnchantmentHook>> action)
        {
            var values = m.Items
                .OfType<BaseEquippableItem>()
                .SelectMany(e => e.Enchantments.Values.Values)
                .ToList();

            if (m is IEnchanted enchanted) 
                values.AddRange(enchanted.Enchantments.Values.Values);

            Fire(values, action);
        }
        
        public static void FireHook(this IEnchanted enchanted, Expression<Action<IEnchantmentHook>> action)
        {
            Fire(enchanted.Enchantments.Values.Values, action);
        }

        public static void FireHook(this EnchantmentDictionary dictionary, Expression<Action<IEnchantmentHook>> action)
        {
            Fire(dictionary.Values.Values, action);
        }

        private static void Fire(IEnumerable<IEnchantmentValue> values, Expression<Action<IEnchantmentHook>> expr)
        {
            var action = GetDelegate(expr);
            var methodCallExpr = (MethodCallExpression) expr.Body;

            values
                .OrderByPriority(methodCallExpr.Method.Name)
                .ToList()
                .ForEach(action);
        }

        public static IEnumerable<TResult> FireHook<TResult>(
            this Mobile m,
            Expression<Func<IEnchantmentHook, TResult>> func
        )
            where TResult : unmanaged, IComparable, IEquatable<TResult>
        {
            return Fire(
                m.Items
                    .OfType<BaseEquippableItem>()
                    .SelectMany(e => e.Enchantments.Values.Values),
                func
            );
        }

        public static IEnumerable<TResult> FireHook<TResult>(
            this EnchantmentDictionary dictionary,
            Expression<Func<IEnchantmentHook, TResult>> action
        )
            where TResult : unmanaged, IComparable, IEquatable<TResult>
        {
            return Fire(dictionary.Values.Values, action);
        }

        private static IEnumerable<TResult> Fire<TResult>(
            IEnumerable<IEnchantmentValue> values,
            Expression<Func<IEnchantmentHook, TResult>> expr
        )
            where TResult : unmanaged, IComparable, IEquatable<TResult>
        {
            var func = GetDelegate(expr);
            var methodCallExpr = (MethodCallExpression) expr.Body;

            var enchantments = values
                .OrderByPriority(methodCallExpr.Method.Name)
                .ToList();

            return enchantments.Select(func);
        }
    }
}