using System;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic
{
    public abstract class EnchantmentInfo : IEnchantmentInfo
    {
        public abstract string Description { get; protected set;}
        public abstract EnchantNameType Place { get; protected set;}
        public abstract string[,] Names { get; protected set; }
        public abstract int Hue { get; protected set;}
        public abstract int CursedHue { get; protected set;}
        
        public virtual string GetName(int index, bool cursed = false)
        {
            return index < Names.GetLength(0) ? Names[index, cursed ? 1 : 0] : string.Empty;
        }

        public virtual string GetName(Enum target, bool cursed = false)
        {
            return GetName((int) (object) target, cursed);
        }
        
        protected static string[,] MakeSkillNames(string profession, string[,] defaults)
        {
            var result = new string[defaults.GetLength(0), 2];
            for (var i = 0; i < defaults.GetLength(0); i++)
            {
                result[i, 0] = $"{defaults[i, 0]} {profession}'s";
                result[i, 1] = $"{defaults[i, 1]} {profession}'s";
            }

            return result;
        }
        
        protected static string[,] MakeProtNames(string prot, string[,] defaults)
        {
            var result = new string[defaults.GetLength(0), 2];
            for (var i = 0; i < defaults.GetLength(0); i++)
            {
                if(!string.IsNullOrEmpty(defaults[i, 0]))
                    result[i, 0] = $"Elemental {prot} {defaults[i, 0]}";
                if(!string.IsNullOrEmpty(defaults[i, 1]))
                    result[i, 1] = $"Elemental {prot} {defaults[i, 1]}";
            }

            return result;
        }
    }
}