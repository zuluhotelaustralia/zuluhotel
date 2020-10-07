using System;
using System.Collections.Generic;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic
{
    public interface IEnchantmentInfo
    {
        public static readonly Dictionary<MagicProp, IEnchantmentInfo> MagicEnchantMap =
            new Dictionary<MagicProp, IEnchantmentInfo>();

        public static readonly string[,] DefaultElementalProtectionNames =
        {
            {string.Empty, string.Empty},
            {"Bane", "Weakness"},
            {"Warding", "Vulnerability"},
            {"Protection", "Amplification"},
            {"Immunity", "Defenselessness"},
            {"Attunement", "Exposure"},
            {"Absorbsion", "Endangerment"},
        };
        
        public static string[,] MakeNames(string profession, string[,] defaults)
        {
            var result = new string[defaults.GetLength(0), 2];
            for (var i = 0; i < defaults.GetLength(0); i++)
            {
                result[i, 0] = $"{defaults[i, 0]} {profession}'s";
                result[i, 1] = $"{defaults[i, 1]} {profession}'s";
            }

            return result;
        }
        
        public delegate void MiddlewareAction<T>(ref T value, Action next);
        
        string Description { get; }
        EnchantNameType Place { get; }
        string[,] Names { get; }
        int Hue { get; }
        int CursedHue { get; }
        string GetName(int index, bool cursed = false);
        string GetName(Enum target, bool cursed = false);
    }
}