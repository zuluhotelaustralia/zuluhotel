using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Server;
using Server.Engines.Magic;
using ZuluContent.Zulu.Engines.Magic;

namespace Scripts.Engines.Magic
{
    public enum EnchantNameType
    {
        Prefix = 1,
        Suffix = 2
    }

    public interface IMagicValue
    {
        public MagicProp Prop { get; }
        public EnchantNameType Place { get; }
        public string[] NormalNames { get; } 
        public string[] CursedNames { get; }
        public int Color { get; }
        public int CursedColor { get; }
        public bool Cursed { get; }
        public void Remove();
        public void AddTo(Mobile mobile);

        public void Serialize(IGenericWriter stream);

        public static IMagicValue Deserialize(IGenericReader reader, IEntity parentEntity)
        {
            var prop = (MagicProp) reader.ReadInt();

            return prop switch
            {
                MagicProp.Skill => new MagicSkillMod((SkillName) reader.ReadInt(), reader.ReadDouble()),
                MagicProp.Stat => new MagicStatMod((StatType) reader.ReadInt(), reader.ReadInt(), parentEntity),
                MagicProp.ElementalResist => new MagicResistMod((ElementalType) reader.ReadInt(), reader.ReadInt()),

                _ => (TypeCode)reader.ReadInt() switch
                {
                    TypeCode.SByte => new MagicAttribute<sbyte>(prop, reader.ReadSByte()),
                    TypeCode.Byte => new MagicAttribute<byte>(prop, reader.ReadByte()),
                    TypeCode.Int16 => new MagicAttribute<short>(prop, reader.ReadShort()),
                    TypeCode.UInt16 => new MagicAttribute<ushort>(prop, reader.ReadUShort()),
                    TypeCode.Int32 => new MagicAttribute<int>(prop, reader.ReadInt()),
                    TypeCode.UInt32 => new MagicAttribute<uint>(prop, reader.ReadUInt()),
                    TypeCode.Int64 => new MagicAttribute<long>(prop, reader.ReadLong()),
                    TypeCode.UInt64 => new MagicAttribute<ulong>(prop, reader.ReadULong()),
                    TypeCode.Char => new MagicAttribute<char>(prop, reader.ReadChar()),
                    TypeCode.Single => new MagicAttribute<float>(prop, reader.ReadFloat()),
                    TypeCode.Double => new MagicAttribute<double>(prop, reader.ReadDouble()),
                    TypeCode.Decimal => new MagicAttribute<decimal>(prop, reader.ReadDecimal()),
                    TypeCode.Boolean => new MagicAttribute<bool>(prop, reader.ReadBool()),
                    _ => throw new ArgumentException("Type is not a recognized serializable unmanaged type")
                }
            };
        }
    }

    public interface IMagicMod<out T> : IMagicValue where T : unmanaged
    {
        public T Target { get; }
        public static IReadOnlyDictionary<T, Dictionary<string, string>> NormalToCursedMap;
        
        void IMagicValue.Serialize(IGenericWriter writer)
        {
            writer.Write((int) Prop);
            object value = default(T);

            switch (this)
            {
                case MagicAttribute<T> _:
                    writer.Write((int)Type.GetTypeCode(typeof(T)));
                    value = Target;
                    break;
                case MagicSkillMod skillMod:
                    writer.Write((int)skillMod.Target);
                    value = skillMod.Value;
                    break;
                case MagicStatMod statMod:
                    writer.Write((int)statMod.Target);
                    value = statMod.Offset;
                    break;
            }
            
            switch(value)
            {
                case sbyte v:
                    writer.Write(v);
                    break;
                case byte v:
                    writer.Write(v);
                    break;
                case short v:
                    writer.Write(v);
                    break;
                case ushort v:
                    writer.Write(v);
                    break;
                case int v:
                    writer.Write(v);
                    break;
                case uint v:
                    writer.Write(v);
                    break;
                case long v:
                    writer.Write(v);
                    break;
                case ulong v:
                    writer.Write(v);
                    break;
                case char v:
                    writer.Write(v);
                    break;
                case float v:
                    writer.Write(v);
                    break;
                case double v:
                    writer.Write(v);
                    break;
                case decimal v:
                    writer.Write(v);
                    break;
                case bool v:
                    writer.Write(v);
                    break;
                case Enum v:
                    writer.Write(Convert.ToInt32(v));
                    break;
                default:
                    throw new ArgumentException("Type is not a recognized serializable unmanaged type", typeof(T).Name);
            };
        }
    }
}