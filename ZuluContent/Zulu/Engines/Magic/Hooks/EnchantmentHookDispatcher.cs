using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
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

        private static int GetActionPriority(ICustomAttributeProvider m)
        {
            return m.GetCustomAttributes(true)
                .OfType<CallPriorityAttribute>()
                .OrderBy(x => x.Priority)
                .FirstOrDefault()?.Priority ?? int.MaxValue;
        }
        
        private static IOrderedEnumerable<IEnchantmentHook> OrderByPriority(
            IEnumerable<IEnchantmentHook> values,
            string methodName
        )
        {
            return values.OrderBy(kv =>
                HookPriorities[methodName].TryGetValue(kv.GetType(), out var priority)
                    ? priority
                    : -1
            );
        }

        public EnchantmentHookDispatcher() : base()
        {
            
        }
        
        private static readonly Dictionary<string, Dictionary<Type, bool>> HookImplementedMap = new();

        private static bool IsOverridden(MethodInfo targetMethod, Type targetType)
        {
            if (!HookImplementedMap.ContainsKey(targetMethod.Name))
                HookImplementedMap.TryAdd(targetMethod.Name, new Dictionary<Type, bool>());

            var dict = HookImplementedMap[targetMethod.Name];
            
            if (dict.TryGetValue(targetType, out var implemented))
            {
                return implemented;
            }

            implemented = targetType.GetMethod(targetMethod.Name)?.DeclaringType == targetType;

            dict.Add(targetType, implemented);
            return implemented;
        }

        public static void Dispatch(IEnumerable<IEnchantmentHook> values, Action<IEnchantmentHook> action)
        {
            var dispatcher = Pool.Get();
            dispatcher.Values = values;
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
                var values = OrderByPriority(Values, targetMethod.Name);
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