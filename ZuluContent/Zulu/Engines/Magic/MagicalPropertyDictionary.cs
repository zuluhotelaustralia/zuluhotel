using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Scripts.Engines.Magic;
using Scripts.Zulu.Utilities;
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
        protected bool IsDirty = true; // Starts off dirty for the first save
        protected BufferedFileWriter SaveBuffer { get; }

        private static int GetEnumLength<T>() where T : Enum => Enum.GetValues(typeof(T)).Cast<int>().Last() + 1;
        private static readonly int AttributesLength = GetEnumLength<MagicProp>();
        private static readonly int SkillModsLength = GetEnumLength<SkillName>();
        private static readonly int StatModsLength = GetEnumLength<StatType>();
        private static readonly int ResistModsLength = GetEnumLength<ElementalType>();

        protected readonly IMagicValue[] Attributes =
            new IMagicValue[AttributesLength];

        protected readonly IMagicMod<SkillName>[] SkillMods =
            new IMagicMod<SkillName>[SkillModsLength];

        protected readonly IMagicMod<StatType>[] StatMods =
            new IMagicMod<StatType>[StatModsLength];

        protected readonly IMagicMod<ElementalType>[] ResistMods =
            new IMagicMod<ElementalType>[ResistModsLength];

        public static readonly IReadOnlyDictionary<Type, MagicProp> EnumToMagicMappings =
            new Dictionary<Type, MagicProp>
            {
                [typeof(WeaponDurabilityLevel)] = MagicProp.Durability,
                [typeof(WeaponAccuracyLevel)] = MagicProp.Accuracy,
                [typeof(WeaponDamageLevel)] = MagicProp.Damage,
                [typeof(WeaponQuality)] = MagicProp.Quality,
                [typeof(SlayerName)] = MagicProp.Slayer,
                [typeof(ClothingQuality)] = MagicProp.Quality,
                [typeof(CraftResource)] = MagicProp.CraftResource,
                [typeof(ArmorQuality)] = MagicProp.Quality,
                [typeof(ArmorDurabilityLevel)] = MagicProp.Durability,
                [typeof(ArmorProtectionLevel)] = MagicProp.ArmorProtection,
                [typeof(ArmorMeditationAllowance)] = MagicProp.MeditationAllowance,
                [typeof(ArmorBonus)] = MagicProp.ArmorBonus,
            };

        protected MagicalPropertyDictionary(Item parent)
        {
            Parent = parent;
            SaveBuffer = new BufferedFileWriter(true);
        }

        public IMagicValue this[MagicProp prop]
        {
            get => Attributes[(int) prop];
            set
            {
                IsDirty = true;
                Attributes[(int) prop] = value;
            }
        }

        private MagicProp ToPropType<T>() where T : unmanaged
        {
            if (EnumToMagicMappings.TryGetValue(typeof(T), out var prop))
                return prop;

            throw new ArgumentOutOfRangeException($"Enum value type of {typeof(T)} has no valid mapping entry.");
        }

        public bool HasAttr(MagicProp magicProp) => this[magicProp] != null;

        public bool HasAttr<TKey>() where TKey : unmanaged, Enum => HasAttr(ToPropType<TKey>());

        public TOutput GetAttr<TOutput>(MagicProp propertyType, TOutput defaultValue = default)
            where TOutput : unmanaged
        {
            // Converts deserialized <int> attributes into their enum representations on first access
            if (this[propertyType] is MagicAttribute<int> intMod && ToPropType<TOutput>() == propertyType)
                Attributes[(int) propertyType] = intMod.IntToEnumAttr<TOutput>();

            if(this[propertyType] is MagicAttribute<TOutput> instanceMod)
                return instanceMod.Target;

            return defaultValue;
        }

        public void SetAttr<TValue>(MagicProp magicalProp, TValue value) where TValue : unmanaged
        {
            IsDirty = true;

            if (this[magicalProp] is MagicAttribute<TValue> attr)
                attr.Target = value;
            else
                this[magicalProp] = new MagicAttribute<TValue>(magicalProp, value);
        }

        public T GetAttr<T>(T defaultValue = default) where T : unmanaged, Enum =>
            GetAttr(ToPropType<T>(), defaultValue);

        public void SetAttr<T>(T value) where T : unmanaged, Enum =>
            SetAttr(ToPropType<T>(), value);

        public TValue GetAttr<TKey, TValue>(TValue defaultValue = default)
            where TKey : unmanaged, Enum where TValue : unmanaged =>
            GetAttr(ToPropType<TKey>(), defaultValue);

        public void SetAttr<TKey, TValue>(TValue value) where TKey : unmanaged, Enum where TValue : unmanaged =>
            SetAttr(ToPropType<TKey>(), value);

        public IList<IMagicMod<T>> GetModsOfType<T>() where T : unmanaged, Enum =>
            GetModStorage<T>().Where(x => x != null).ToList();

        private IMagicMod<T>[] GetModStorage<T>() where T : unmanaged, Enum
        {
            var storage = default(T) switch
            {
                SkillName _ => SkillMods as IMagicMod<T>[],
                StatType _ => StatMods as IMagicMod<T>[],
                ElementalType _ => ResistMods as IMagicMod<T>[],
                _ => null
            };

            if (storage == null)
                throw new ArgumentOutOfRangeException($"Parameter type {typeof(T).Name} is not valid.");

            return storage;
        }

        public int GetResist(ElementalType type)
        {
            return ResistMods[(int) type] is MagicResistMod mod ? mod.Value : 0;
        }
        
        public void SetResist(ElementalType type, int value)
        {
            IsDirty = true;
            
            if (ResistMods[(int) type] is MagicResistMod mod)
                mod.Value = value;
            else
                ResistMods[(int) type] = new MagicResistMod(type, value);
        }

        public void AddMod<T>(IMagicMod<T> mod) where T : unmanaged, Enum
        {
            IsDirty = true;
            
            if (TryGetMod(mod.Target, out IMagicMod<T> existing))
                RemoveMod(existing);

            var storage = GetModStorage<T>();
            storage[(int) (object) mod.Target] = mod;

            OnMobileEquip();
        }

        public void RemoveMod<T>(IMagicMod<T> mod) where T : unmanaged, Enum
        {
            var storage = GetModStorage<T>();
            mod.Remove();
            storage[(int) (object) mod.Target] = null;
        }

        public void OnMobileEquip()
        {
            if (Parent?.Parent is Mobile m) 
                foreach (var value in GetAllValues())
                    value.AddTo(m);
        }

        public void OnMobileRemoved()
        {
            foreach (var value in GetAllValues())
                value.Remove();
        }

        public bool HasMod<T>(T target) where T : unmanaged, Enum
        {
            var storage = GetModStorage<T>();
            return storage[(int) (object) target] != null;
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
                    Attributes,
                    SkillMods,
                    StatMods,
                    ResistMods
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
            if (IsDirty)
            {
                SaveBuffer.Flush();
                var values = GetAllValues();

                const byte version = 1;
                SaveBuffer.Write(version);
                SaveBuffer.Write(values.Count);

                foreach (var value in values)
                    value.Serialize(SaveBuffer);

                IsDirty = false;
            }
            
            SaveBuffer.WriteTo(writer);
        }

        protected static T Deserialize<T>(IGenericReader reader, T mp) where T : MagicalPropertyDictionary
        {
            var version = reader.ReadByte();
            var length = reader.ReadInt();
            for (var i = 0; i < length; i++)
            {
                var value = IMagicValue.Deserialize(reader, mp.Parent);

                switch (value)
                {
                    case IMagicMod<SkillName> skillMod:
                        mp.SkillMods[(int) (object) skillMod.Target] = skillMod;
                        break;
                    case IMagicMod<StatType> statMod:
                        mp.StatMods[(int) (object) statMod.Target] = statMod;
                        break;
                    case IMagicMod<ElementalType> resistMod:
                        mp.ResistMods[(int) (object) resistMod.Target] = resistMod;
                        break;
                    default:
                        mp.Attributes[(int) value.Prop] = value;
                        break;
                }
            }

            return mp;
        }
    }
}