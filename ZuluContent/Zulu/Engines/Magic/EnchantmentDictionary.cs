using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MessagePack;
using Scripts.Zulu.Utilities;
using Server;
using Server.Engines.Magic;
using ZuluContent.Zulu.Engines.Magic.Enchantments;

namespace ZuluContent.Zulu.Engines.Magic.Enums
{
    [MessagePackObject]
    public class EnchantmentDictionary
    {
        private static readonly Dictionary<Type, int> EnchantUnionMap;
        public static readonly Dictionary<Type, Type> EnchantmentInfoMap;

        static EnchantmentDictionary()
        {
            EnchantUnionMap =
                Attribute.GetCustomAttributes(typeof(IEnchantmentValue), typeof(UnionAttribute))
                    .OfType<UnionAttribute>()
                    .ToDictionary(x => x.SubType, y => y.Key);

            EnchantmentInfoMap = EnchantUnionMap.Keys
                .Select(t =>
                {
                    var type = t.BaseType;
                    while (type?.IsGenericType != true || type?.GetGenericTypeDefinition() != typeof(Enchantment<>))
                        type = type?.BaseType;

                    return (enchantmentType: t, infoType: type?.GenericTypeArguments.FirstOrDefault());
                })
                .ToDictionary(k => k.enchantmentType, v => v.infoType);
        }

        [Key(0)]
        public Dictionary<int, IEnchantmentValue> Values { get; set; } = new Dictionary<int, IEnchantmentValue>();

        [Key(1)] public bool Identified { get; set; } = true;

        public bool HasValue<T>() where T : class, IEnchantmentValue
        {
            return Values.ContainsKey(EnchantUnionMap[typeof(T)]);
        }

        private T Get<T>() where T : class, IEnchantmentValue
        {
            if (!EnchantUnionMap.ContainsKey(typeof(T)))
            {
                Console.WriteLine(
                    $"{nameof(EnchantmentDictionary)}: Attempted to access enchantment property of " +
                    $"{typeof(T).Name}, but that type does not have a Union mapping on {nameof(IEnchantmentValue)}"
                );

                return null;
            }

            if (Values.TryGetValue(EnchantUnionMap[typeof(T)], out var value) && value is T result)
                return result;


            return null;
        }

        private T Set<T>(T value) where T : class, IEnchantmentValue
        {
            Values.TryAdd(EnchantUnionMap[typeof(T)], value);
            return value;
        }

        public TResult Get<TSource, TResult>(Func<TSource, TResult> selector, TResult @default = default)
            where TSource : class, IEnchantmentValue
        {
            return HasValue<TSource>() ? selector(Get<TSource>()) : @default;
        }

        public TResult Set<T, TResult>(Func<T, TResult> expr, [CallerMemberName] string callerName = "")
            where TResult : IComparable where T : class, IEnchantmentValue, new()
        {
            var target = Get<T>();

            if (target != null)
                return expr(target);

            target = new T();
            var result = expr(target);
            
            // This is to guard against creating unnecessary enchantments from default values
            if (result != null && Comparer<TResult>.Default.Compare(result, default) != 0) 
                Set(target);

            return result;
        }

        public void SetResist(ElementalType protectionType, int value)
        {
            switch (protectionType)
            {
                case ElementalType.Water:
                    Set((WaterProtection e) => e.Value = value);
                    break;
                case ElementalType.Air:
                    Set((AirProtection e) => e.Value = value);
                    break;
                case ElementalType.Physical:
                    Set((PhysicalProtection e) => e.Value = value);
                    break;
                case ElementalType.Fire:
                    Set((FireProtection e) => e.Value = value);
                    break;
                case ElementalType.Poison:
                    Set((PoisonProtection e) => e.Value = value);
                    break;
                case ElementalType.Earth:
                    Set((EarthProtection e) => e.Value = value);
                    break;
                case ElementalType.Necro:
                    Set((NecroProtection e) => e.Value = value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(protectionType), protectionType, null);
            }
        }

        public int GetResist(ElementalType protectionType)
        {
            return protectionType switch
            {
                ElementalType.Water => Get((WaterProtection e) => e.Value),
                ElementalType.Air => Get((AirProtection e) => e.Value),
                ElementalType.Physical => Get((PhysicalProtection e) => e.Value),
                ElementalType.Fire => Get((FireProtection e) => e.Value),
                ElementalType.Poison => Get((PoisonProtection e) => e.Value),
                ElementalType.Earth => Get((EarthProtection e) => e.Value),
                ElementalType.Necro => Get((NecroProtection e) => e.Value),
                _ => throw new ArgumentOutOfRangeException(nameof(protectionType), protectionType, null)
            };
        }
        
        public void OnIdentified(Item item)
        {
            foreach (var value in Values.Values)
            {
                if (value.GetShouldDye())
                    item.Hue = value.Info.Hue;
        
                value.OnIdentified(item);
            }
        }
        
        public static EnchantmentDictionary Deserialize(IGenericReader reader)
        {
            return MessagePackSerializer.Deserialize<EnchantmentDictionary>(reader.GetBaseStream());
        }

        public void Serialize(IGenericWriter writer)
        {
            writer.Write(MessagePackSerializer.Serialize(this));
        }
    }
}