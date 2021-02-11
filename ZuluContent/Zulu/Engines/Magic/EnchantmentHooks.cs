using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Scripts.Zulu.Engines.Classes;
using Server;
using ZuluContent.Zulu.Engines.Magic.Enchantments;
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
            var mapped = EnchantmentDictionary.EnchantmentInfoMap.Keys
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
                .GroupBy(x => x.InterfaceMethod).ToList();
            HookPriorities = mapped
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

        private static IOrderedEnumerable<IEnchantmentHook> OrderByPriority(this IEnumerable<IEnchantmentHook> values,
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


        public static void FireHook(this Mobile m, Expression<Action<IEnchantmentHook>> action,
            bool zuluClassOnly = false)
        {
            List<IEnchantmentHook> hooks = new();

            if (!zuluClassOnly)
            {
                hooks.AddRange(m.Items
                    .OfType<BaseEquippableItem>()
                    .SelectMany(e => e.Enchantments.Values.Values)
                    .ToList());
            }

            if (m is IEnchanted enchanted)
                hooks.AddRange(enchanted.Enchantments.Values.Values);

            if (m is IZuluClassed {ZuluClass: { }} classed)
                hooks.Add(classed.ZuluClass);

            Fire(hooks, action);
        }

        public static void FireHook(this IEnchanted enchanted, Expression<Action<IEnchantmentHook>> action)
        {
            Fire(enchanted.Enchantments.Values.Values, action);
        }

        public static void FireHook(this EnchantmentDictionary dictionary, Expression<Action<IEnchantmentHook>> action)
        {
            Fire(dictionary.Values.Values, action);
        }

        private static void Fire(IEnumerable<IEnchantmentHook> values, Expression<Action<IEnchantmentHook>> expr)
        {
            var action = GetDelegate(expr);
            var methodCallExpr = (MethodCallExpression) expr.Body;

            var ordered = values
                .OrderByPriority(methodCallExpr.Method.Name);

            ordered
                .Where(x => !(x is MagicImmunity || x is PoisonProtection || x is SpellReflect))
                .ToList()
                .ForEach(action);

            var magicImmunityHook = ordered
                .Where(x => x is MagicImmunity {Value: > 0})
                .Cast<MagicImmunity>()
                .OrderByDescending(x => x.Value)
                .FirstOrDefault();

            var poisonProtectionHook = ordered
                .Where(x => x is PoisonProtection {Value: > 0})
                .Cast<PoisonProtection>()
                .OrderByDescending(x => x.Value)
                .FirstOrDefault();

            var spellReflectHook = ordered
                .Where(x => x is SpellReflect {Value: > 0})
                .Cast<SpellReflect>()
                .OrderByDescending(x => x.Value)
                .FirstOrDefault();

            if (magicImmunityHook != null)
                action(magicImmunityHook);

            if (poisonProtectionHook != null)
                action(poisonProtectionHook);

            if (spellReflectHook != null)
                action(spellReflectHook);
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