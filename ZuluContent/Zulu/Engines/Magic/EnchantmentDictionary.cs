using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MessagePack;
using Server;
using Server.Engines.Magic;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic.Enchantments;
using ZuluContent.Zulu.Items;

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

        [Key(0)] public Dictionary<int, IEnchantmentValue> Values { get; set; } = new();

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

        public T Set<T>(T value) where T : class, IEnchantmentValue
        {
            Values.TryAdd(EnchantUnionMap[typeof(T)], value);
            return value;
        }

        public bool Remove(IEnchantmentValue value)
        {
            if (Values.ContainsKey(EnchantUnionMap[value.GetType()]))
            {
                Values.Remove(EnchantUnionMap[value.GetType()]);
                return true;
            }

            return false;
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
        
        public void SetFromResourceType(Type enchantmentType, int value)
        {
            if (!typeof(IEnchantmentValue).IsAssignableFrom(enchantmentType))
            {
                Console.WriteLine(
                    $"{nameof(SetFromResourceType)}: Attempted to set an enchantment of type {enchantmentType.Name} " +
                    $"but that type is not an instance of {nameof(IEnchantmentValue)}"
                );
                return;
            }
            
            if (enchantmentType == typeof(DexBonus))
            {
                Set((DexBonus e) => e.Value += value);
            }
            else if (enchantmentType == typeof(MagicEfficiencyPenalty))
            {
                Set((MagicEfficiencyPenalty e) => e.Value += value);
            }
            else if (enchantmentType == typeof(MagicImmunity))
            {
                Set((MagicImmunity e) => e.Value = value);
            }
            else if (enchantmentType == typeof(AirProtection))
            {
                Set((AirProtection e) => e.Value = value);
            }
            else if (enchantmentType == typeof(EarthProtection))
            {
                Set((EarthProtection e) => e.Value = value);
            }
            else if (enchantmentType == typeof(FireProtection))
            {
                Set((FireProtection e) => e.Value = value);
            }
            else if (enchantmentType == typeof(NecroProtection))
            {
                Set((NecroProtection e) => e.Value = value);
            }
            else if (enchantmentType == typeof(WaterProtection))
            {
                Set((WaterProtection e) => e.Value = value);
            }
        }

        public void OnIdentified(Item item)
        {
            foreach (var value in Values.Values)
            {
                if (item is not IGMItem && value.GetShouldDye())
                    item.Hue = value.Info.Hue;

                value.OnIdentified(item);
            }
        }

        public static EnchantmentDictionary Deserialize(IGenericReader reader)
        {
            var bytes = new byte[reader.ReadInt()];
            reader.Read(bytes);

            return MessagePackSerializer.Deserialize<EnchantmentDictionary>((ReadOnlyMemory<byte>) bytes);
        }

        public void Serialize(IGenericWriter writer)
        {
            var bytes = MessagePackSerializer.Serialize(this);
            writer.Write(bytes.Length);
            writer.Write(bytes);
        }
    }
}