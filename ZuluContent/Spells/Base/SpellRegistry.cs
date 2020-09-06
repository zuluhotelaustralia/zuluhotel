using System;
using System.Collections.Generic;
using System.Linq;

namespace Server.Spells
{
    public class SpellRegistry
    {
        private static int m_Count;

        private static Dictionary<Type, Int32> m_IDsFromTypes = new Dictionary<Type, Int32>(700);
        
        public static Type[] Types { get; } = new Type[700];

        public static int Count
        {
            get
            {
                if (m_Count == -1)
                {
                    m_Count = 0;

                    foreach (var t in Types)
                        if (t != null)
                            ++m_Count;
                }

                return m_Count;
            }
        }

        public static void Register(int spellId, Type type)
        {
            if (spellId < 0 || spellId >= Types.Length)
                return;

            if (Types[spellId] == null)
                ++m_Count;

            Types[spellId] = type;

            if (!m_IDsFromTypes.ContainsKey(type))
                m_IDsFromTypes.Add(type, spellId);
        }

        private static object[] m_Params = new object[2];

        public static Spell NewSpell(int spellID, Mobile caster, Item scroll)
        {
            if (spellID < 0 || spellID >= Types.Length)
                return null;

            Type t = Types[spellID];

            if (t != null)
            {
                m_Params[0] = caster;
                m_Params[1] = scroll;

                try
                {
                    return (Spell) Activator.CreateInstance(t, m_Params);
                }
                catch
                {
                }
            }

            return null;
        }

        private static string[] m_CircleNames = new[]
        {
            "First",
            "Second",
            "Third",
            "Fourth",
            "Fifth",
            "Sixth",
            "Seventh",
            "Eighth"
        };

        public static Spell NewSpell(string name, Mobile caster, Item scroll)
        {
            for (int i = 0; i < m_CircleNames.Length; ++i)
            {
                Type t = AssemblyHandler.FindFirstTypeForName($"Server.Spells.{m_CircleNames[i]}.{name}");

                if (t != null)
                {
                    m_Params[0] = caster;
                    m_Params[1] = scroll;

                    try
                    {
                        return (Spell) Activator.CreateInstance(t, m_Params);
                    }
                    catch
                    {
                    }
                }
            }

            return null;
        }

        public static Spell NewSpell(Type t, Mobile caster, Item scroll)
        {
            if (!Types.Contains(t))
                return null;

            if (t != null)
            {
                m_Params[0] = caster;
                m_Params[1] = scroll;

                try
                {
                    return (Spell) Activator.CreateInstance(t, m_Params);
                }
                catch
                {
                }
            }

            return null;
        }

        public static T NewSpell<T>(Mobile caster, Item scroll) where T : Spell
        {
            return (T) NewSpell(typeof(T), caster, scroll);
        }
    }
}