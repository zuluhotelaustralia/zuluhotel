using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Server.Spells
{
    public static class SpellRegistry
    {
        private static Dictionary<Type, SpellInfo> _spellInfos;
        private static Dictionary<SpellEntry, Type> _spellTypes;

        public static Dictionary<Type, SpellInfo> SpellInfos => _spellInfos ??=
            ZhConfig.Magic.Spells.ToDictionary(kv => kv.Value.Type, kv => kv.Value);

        public static Dictionary<SpellEntry, Type> SpellTypes => _spellTypes ??=
            ZhConfig.Magic.Spells.ToDictionary(kv => kv.Key, kv => kv.Value.Type);

        public static readonly Dictionary<Type, Func<Mobile, Item, Spell>> SpellCreators =
            SpellInfos.Keys.ToDictionary(k => k, SpellCreatorLambda);

        public static Spell Create(SpellEntry spellId, Mobile caster, Item scroll = null)
        {
            return !SpellTypes.TryGetValue(spellId, out var t) ? null : Create(t, caster, scroll);
        }

        public static Spell Create(string name, Mobile caster, Item scroll = null)
        {
            if (Enum.TryParse(typeof(SpellEntry), name, true, out var result) && result is SpellEntry entry)
                return Create(entry, caster, scroll);

            return null;
        }

        public static Spell Create(Type t, Mobile caster, Item scroll = null)
        {
            return SpellCreators.TryGetValue(t, out var creator) ? creator(caster, scroll) : null;
        }

        public static T Create<T>(Mobile caster, Item scroll = null) where T : Spell
        {
            return (T)Create(typeof(T), caster, scroll);
        }

        public static SpellInfo GetInfo(SpellEntry spellEntry)
        {
            return SpellInfos[SpellTypes[spellEntry]];
        }

        private static Func<Mobile, Item, Spell> SpellCreatorLambda(Type type)
        {
            var mobileArg = Expression.Parameter(typeof(Mobile), "caster");
            var itemArg = Expression.Parameter(typeof(Item), "item");

            var constructor = type.GetConstructor(new[] { typeof(Mobile), typeof(Item) });

            if (constructor is null)
            {
                throw new ArgumentOutOfRangeException(nameof(type), "No constructor matching (Mobile, Item) found");
            }

            return Expression.Lambda<Func<Mobile, Item, Spell>>(
                Expression.New(
                    constructor,
                    mobileArg,
                    itemArg
                ),
                mobileArg,
                itemArg
            ).Compile();
        }
    }
}