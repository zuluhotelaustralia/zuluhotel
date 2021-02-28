using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Server;
using ZuluContent.Zulu.Engines.Magic.Enums;
using ZuluContent.Zulu.Engines.Magic.Hooks;

namespace ZuluContent.Zulu.Engines.Magic
{
    public class EnchantmentHookDispatcher : DispatchProxy
    {
        public static readonly Dictionary<string, Dictionary<Type, int>> HookPriorities;
        
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
                        .Select(m => (InterfaceMethod: map.InterfaceMethods[Array.IndexOf(map.TargetMethods, m)],
                            HookMethod: m));
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
        
        private IEnumerable<IEnchantmentHook> Values { get; set; }
        
        [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        public static IEnchantmentHook Create(IEnumerable<IEnchantmentHook> values = null)
        {
            var proxy = (EnchantmentHookDispatcher) Create<IEnchantmentHook, EnchantmentHookDispatcher>();
            
            proxy.Values = values ?? new List<IEnchantmentHook>();

            return proxy as IEnchantmentHook;
        }
        
        private static IOrderedEnumerable<IEnchantmentHook> OrderByPriority(IEnumerable<IEnchantmentHook> values,
            string methodName)
        {
            return values.OrderBy(kv =>
                HookPriorities.ContainsKey(methodName) &&
                HookPriorities[methodName].TryGetValue(kv.GetType(), out var priority)
                    ? priority
                    : -1
            );
        }
        
        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            try
            {
                foreach (var target in OrderByPriority(Values, targetMethod.Name)) 
                    targetMethod.Invoke(target, args);
            }
            catch (TargetInvocationException exc)
            {
                throw exc.InnerException ?? exc;
            }
            return null;
        }
    }
}