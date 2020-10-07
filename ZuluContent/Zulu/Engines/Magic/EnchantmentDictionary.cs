using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MessagePack;
using Scripts.Zulu.Utilities;
using Server;
using Server.Engines.Magic;
using ZuluContent.Zulu.Engines.Magic.Enchantments;
using ZuluContent.Zulu.Engines.Magic.Hooks;

namespace ZuluContent.Zulu.Engines.Magic.Enums
{
    [MessagePackObject]
    public class EnchantmentDictionary: IOnEquip
    {
        private static readonly Dictionary<Type, int> EnchantUnionMap = new Dictionary<Type, int>();

        static EnchantmentDictionary()
        {
            var unionAttributes =
                (UnionAttribute[]) Attribute.GetCustomAttributes(typeof(IEnchantmentValue), typeof(UnionAttribute));

            foreach (var attr in unionAttributes) 
                EnchantUnionMap.Add(attr.SubType, attr.Key);
        }
        
        [Key(0)] public Dictionary<int, IEnchantmentValue> Values { get; set; } = new Dictionary<int, IEnchantmentValue>();
        [Key(1)] public bool Identified { get; set; } = true;

        public bool HasValue<T>() where T : class, IEnchantmentValue
        {
            return Values.ContainsKey(EnchantUnionMap[typeof(T)]);
        }

        private T Get<T>() where T : class, IEnchantmentValue
        {
            if (Values.TryGetValue(EnchantUnionMap[typeof(T)], out var value) && value is T result)
                return result;

            return null;
        }

        public T Set<T>(T value) where T : class, IEnchantmentValue
        {
            Values.TryAdd(EnchantUnionMap[typeof(T)], value);
            return value;
        }

        public static EnchantmentDictionary Deserialize(IGenericReader reader)
        {
            var dict = MessagePackSerializer.Deserialize<EnchantmentDictionary>(reader.GetBaseStream());
            return dict;
        }

        public void Serialize(IGenericWriter writer)
        {
            writer.Write(MessagePackSerializer.Serialize(this));
        }

        public TResult Get<TSource, TResult>(Func<TSource, TResult> selector, TResult @default = default) where TSource : class, IEnchantmentValue
        {
            return HasValue<TSource>() ? selector(Get<TSource>()) : @default;
        }
        
        public void Set<T>(Action<T> expr) where T : class, IEnchantmentValue, new()
        {
            expr(Get<T>() ?? Set(new T()));
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
        
        #region Hooks

        public void OnEquip(Item item, Mobile mobile)
        {
            foreach (var value in Values.Values.OfType<IOnEquip>()) 
                value.OnEquip(item, mobile);
        }

        public void OnRemoved(Item item, Mobile mobile)
        {
            foreach (var value in Values.Values.OfType<IOnEquip>()) 
                value.OnRemoved(item, mobile);
        }
        
        #endregion
    }
}