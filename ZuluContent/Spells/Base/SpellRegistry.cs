using System;
using System.Collections.Generic;
using System.Linq;

namespace Server.Spells
{
    public static class SpellRegistry
    {
        private static readonly Dictionary<Type, Func<Mobile, Item, Spell>> SpellCreators = 
            new Dictionary<Type, Func<Mobile, Item, Spell>>();

        public static readonly Dictionary<SpellType, Type> SpellTypes = 
            new Dictionary<SpellType, Type>();
        
        public static void Register<TSpell>(SpellType spellType, Func<Mobile, Item, TSpell> creator)
            where TSpell : Spell
        {
            SpellCreators.TryAdd(typeof(TSpell), creator);
            SpellTypes.TryAdd(spellType, typeof(TSpell));
        }

        public static Spell Create(int spellId, Mobile caster, Item scroll)
        {

            if (!SpellTypes.TryGetValue((SpellType)spellId, out var t))
                return null;

            try
            {
                return (Spell) Activator.CreateInstance(t, caster, scroll);
            }
            catch
            {
                return null;
            }
        }

        private static readonly string[] CircleNames =
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

        public static Spell Create(string name, Mobile caster, Item scroll)
        {
            if (Enum.TryParse(typeof(SpellType), name, true, out var result)
                && result is SpellType spellType
                && SpellTypes.TryGetValue(spellType, out var type)
                && SpellCreators.TryGetValue(type, out var creator))
            {
                return creator(caster, scroll);
            }

            return null;
        }

        public static Spell Create(Type t, Mobile caster, Item scroll)
        {
            return SpellCreators.TryGetValue(t, out var creator) ? creator(caster, scroll) : null;
        }

        public static T Create<T>(Mobile caster, Item scroll) where T : Spell
        {
            return (T) Create(typeof(T), caster, scroll);
        }
    }
}