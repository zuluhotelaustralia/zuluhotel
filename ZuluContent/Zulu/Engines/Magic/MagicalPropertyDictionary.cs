using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Engines.Magic;
using Server;
using Server.Engines.Magic;
using Server.Items;

namespace ZuluContent.Zulu.Engines.Magic
{

    /**
     * Upcasting magical property dictionary 
     */
    public abstract class MagicalPropertyDictionary
    {
        public Item Parent { get; }
        
        private static int GetEnumLength<T>() where T : Enum => Enum.GetValues(typeof(T)).Cast<int>().Last() + 1;
        private static readonly int AttributesLength = GetEnumLength<MagicProp>();
        private static readonly int SkillModsLength = GetEnumLength<SkillName>();
        private static readonly int StatModsLength = GetEnumLength<StatType>();
        private static readonly int ResistModsLength = GetEnumLength<ElementalType>();

        private readonly IMagicValue[] m_Attributes =
            new IMagicValue[AttributesLength];

        private readonly IMagicMod<SkillName>[] m_SkillMods =
            new IMagicMod<SkillName>[SkillModsLength];

        private readonly IMagicMod<StatType>[] m_StatMods =
            new IMagicMod<StatType>[StatModsLength];

        private readonly IMagicMod<ElementalType>[] m_ResistMods =
            new IMagicMod<ElementalType>[ResistModsLength];

        public static readonly IReadOnlyDictionary<Enum, MagicProp> EnumToMagicMappings =
            new Dictionary<Enum, MagicProp>
            {
                [default(WeaponDurabilityLevel)] = MagicProp.Durability,
                [default(WeaponAccuracyLevel)] = MagicProp.Accuracy,
                [default(WeaponDamageLevel)] = MagicProp.Damage,
                [default(WeaponQuality)] = MagicProp.Quality,
                [default(SlayerName)] = MagicProp.Slayer,
                [default(ClothingQuality)] = MagicProp.Quality,
                [default(CraftResource)] = MagicProp.CraftResource,
                [default(ArmorQuality)] = MagicProp.Quality,
                [default(ArmorDurabilityLevel)] = MagicProp.Durability,
                [default(ArmorProtectionLevel)] = MagicProp.ArmorProtection,
                [default(ArmorMeditationAllowance)] = MagicProp.MeditationAllowance
            };

        protected MagicalPropertyDictionary(Item parent)
        {
            Parent = parent;
        }

        public IMagicValue this[MagicProp prop]
        {
            get => m_Attributes[(int) prop];
            set => m_Attributes[(int) prop] = value;
        }

        private MagicProp ToPropType<T>() where T : unmanaged, Enum
        {
            if (EnumToMagicMappings.TryGetValue(default(T), out var prop))
                return prop;

            throw new ArgumentOutOfRangeException($"Enum value type of {typeof(T)} has no valid mapping entry.");
        }
        
        public bool HasAttr(MagicProp magicProp) => this[magicProp] != null;

        public bool HasAttr<TKey>() where TKey : unmanaged, Enum => HasAttr(ToPropType<TKey>());

        public TOutput GetAttr<TOutput>(MagicProp propertyType, TOutput defaultValue = default) where TOutput : unmanaged =>
            this[propertyType] is MagicAttribute<TOutput> instanceMod ? instanceMod.Target : defaultValue;

        public void SetAttr<TValue>(MagicProp magicalProp, TValue value) where TValue : unmanaged
        {
            if (this[magicalProp] is MagicAttribute<TValue> attr)
                attr.Target = value;
            else
                this[magicalProp] = new MagicAttribute<TValue>(magicalProp, value);
        }

        public T GetAttr<T>(T defaultValue = default) where T : unmanaged, Enum =>
            GetAttr(ToPropType<T>(), defaultValue);

        public void SetAttr<T>(T value) where T : unmanaged, Enum =>
            SetAttr(ToPropType<T>(), value);

        public TValue GetAttr<TKey, TValue>(TValue defaultValue = default) where TKey : unmanaged, Enum where TValue : unmanaged =>
            GetAttr(ToPropType<TKey>(), defaultValue);

        public void SetAttr<TKey, TValue>(TValue value) where TKey : unmanaged, Enum where TValue : unmanaged =>
            SetAttr(ToPropType<TKey>(), value);

        public IList<IMagicMod<T>> GetModsOfType<T>() where T : unmanaged, Enum =>
            GetModStorage<T>().Where(x => x != null).ToList();

        private IMagicMod<T>[] GetModStorage<T>() where T : unmanaged, Enum
        {
            var storage = default(T) switch
            {
                SkillName _ => m_SkillMods as IMagicMod<T>[],
                StatType _ => m_StatMods as IMagicMod<T>[],
                ElementalType _ => m_ResistMods as IMagicMod<T>[],
                _ => null
            };

            if (storage == null)
                throw new ArgumentOutOfRangeException($"Parameter type {typeof(T).Name} is not valid.");

            return storage;
        }


        public void AddMod<T>(IMagicMod<T> mod) where T : unmanaged, Enum
        {
            if (TryGetMod(mod.Target, out IMagicMod<T> existing))
                RemoveMod(existing);

            var storage = GetModStorage<T>();
            storage[(int) (object) mod.Target] = mod;
        }

        public void RemoveMod<T>(IMagicMod<T> mod) where T : unmanaged, Enum
        {
            var storage = GetModStorage<T>();
            mod.Remove();
            storage[(int) (object) mod.Target] = null;
        }

        public void OnMobileEquip(Mobile parent)
        {
            foreach (var value in GetAllValues())
                value.AddTo(parent);
        }

        public void OnMobileRemoved(Mobile parent)
        {
            foreach (var value in GetAllValues())
                value.Remove();
        }

        public bool TryGetMod<TInput, TOutput>(TInput target, out TOutput mod)
            where TInput : unmanaged, Enum where TOutput : IMagicMod<TInput>
        {
            var storage = GetModStorage<TInput>();
            if (storage[(int) (object) target] is TOutput value)
            {
                mod = value;
                return true;
            }

            mod = default;
            return false;
        }

        public IReadOnlyList<IMagicValue> GetAllValues()
        {
            // ReSharper disable CoVariantArrayConversion
            return ((IEnumerable<IMagicValue>[]) new[]
                {
                    m_Attributes,
                    m_SkillMods,
                    m_StatMods
                })
                .Aggregate((acc, list) => acc.Concat(list))
                .Where(m => m != null)
                .ToList();
            // ReSharper restore CoVariantArrayConversion
        }

        public static bool IsNullOrEmpty(MagicalProperties props)
        {
            return props == null || props.GetAllValues().Count == 0;
        }

        public void Serialize(IGenericWriter writer)
        {
            var values = GetAllValues();

            const byte version = 1;
            writer.Write(version);
            writer.Write(values.Count);

            foreach (var value in values)
                value.Serialize(writer);
        }

        protected static MagicalPropertyDictionary Deserialize(IGenericReader reader, MagicalPropertyDictionary mp)
        {
            var version = reader.ReadByte();
            var length = reader.ReadInt();
            for (var i = 0; i < length; i++)
            {
                var value = IMagicValue.Deserialize(reader, mp.Parent);

                switch (value)
                {
                    case IMagicMod<SkillName> skillMod:
                        mp.m_SkillMods[(int) (object) skillMod.Target] = skillMod;
                        break;
                    case IMagicMod<StatType> statMod:
                        mp.m_StatMods[(int) (object) statMod.Target] = statMod;
                        break;
                    default:
                        mp.m_Attributes[(int) value.Prop] = value;
                        break;
                }
            }

            return mp;
        }
    }
}