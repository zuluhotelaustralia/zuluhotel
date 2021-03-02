using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Server;
using ZuluContent.Zulu.Engines.Magic.Enums;
using ZuluContent.Zulu.Engines.Magic.Hooks;
using ZuluContent.Zulu.Utilities;

namespace ZuluContent.Zulu.Engines.Magic
{
    public class EnchantmentHookDispatcher : DispatchProxy
    {
        // ReSharper disable once SuspiciousTypeConversion.Global
        private static readonly ObjectPool<EnchantmentHookDispatcher> Pool = new(() =>
            (EnchantmentHookDispatcher) Create<IEnchantmentHook, EnchantmentHookDispatcher>());

        private static readonly Dictionary<string, Dictionary<Type, int>> HookPriorities;
        private IEnumerable<IEnchantmentHook> Values { get; set; }
        
        static EnchantmentHookDispatcher()
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
                        .Select(m => 
                            (
                                InterfaceMethod: map.InterfaceMethods[Array.IndexOf(map.TargetMethods, m)],
                                HookMethod: m
                            )
                        );
                })
                .GroupBy(x => x.InterfaceMethod).ToList();
            
            HookPriorities = mapped
                .ToDictionary(
                    x => x.Key.Name,
                    x => x.Select(y => (T: y.HookMethod?.ReflectedType, P: GetActionPriority(y.HookMethod)))
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
        
        public EnchantmentHookDispatcher() : base()
        {
            
        }

        private static int GetActionPriority(ICustomAttributeProvider m) => m.GetCustomAttributes(true)
                .OfType<CallPriorityAttribute>()
                .OrderBy(x => x.Priority)
                .FirstOrDefault()?.Priority ?? int.MaxValue;

        private static IOrderedEnumerable<IEnchantmentHook> DistinctOrderByPriority(
            IEnumerable<IEnchantmentHook> values,
            string methodName
        )
        {
            // Remove duplicate distinct enchantments and recombine
            return values
                .OfType<IDistinctEnchantment>()
                .GroupBy(e => e.GetType())
                .Select(g => g.OrderByDescending(x => x).First())
                .Cast<IEnchantmentValue>()
                .Concat(values.Where(e => !(e is IDistinctEnchantment)))
                .OrderBy(kv =>
                    HookPriorities[methodName].TryGetValue(kv.GetType(), out var priority)
                        ? priority
                        : -1
                );
        }
        
        public static void Dispatch(IEnumerable<IEnchantmentHook> values, Action<IEnchantmentHook> action)
        {
            var dispatcher = Pool.Get();
            dispatcher.Values = values;
            // ReSharper disable once SuspiciousTypeConversion.Global
            action(dispatcher as IEnchantmentHook);
            dispatcher.Values = null;
            Pool.Return(dispatcher);
        }
        
        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            if (Values == null)
                return null;
            
            try
            {
                var values = DistinctOrderByPriority(Values, targetMethod.Name);
                foreach (var target in values)
                {
                    targetMethod.Invoke(target, args);
                }
            }
            catch (TargetInvocationException exc)
            {
                throw exc.InnerException ?? exc;
            }
            return null;
        }
    }
}