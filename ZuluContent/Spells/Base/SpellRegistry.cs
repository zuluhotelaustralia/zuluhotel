using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Server.Spells
{
    public static class SpellRegistry
    {
        public static Dictionary<Type, SpellInfo> SpellInfos => ZhConfig.Spells.SpellInfos;
        public static Dictionary<SpellEntry, Type> SpellTypes => ZhConfig.Spells.SpellTypes;
        public static readonly Dictionary<Type, Func<Mobile, Item, Spell>> SpellCreators =
            SpellInfos.Keys.ToDictionary(k => k, SpellCreatorLambda);

        public static Spell Create(SpellEntry spellId, Mobile caster, Item scroll)
        {
            return !SpellTypes.TryGetValue(spellId, out var t) ? null : Create(t, caster, scroll);
        }

        public static Spell Create(string name, Mobile caster, Item scroll)
        {
            if (Enum.TryParse(typeof(SpellEntry), name, true, out var result) && result is SpellEntry entry)
                return Create(entry, caster, scroll);

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

        public static SpellInfo GetInfo(SpellEntry spellEntry)
        {
            return SpellInfos[SpellTypes[spellEntry]];
        }

        private static Func<Mobile, Item, Spell> SpellCreatorLambda(Type type)
        {
            var mobileArg = Expression.Parameter(typeof(Mobile), "caster");
            var itemArg = Expression.Parameter(typeof(Item), "item");

            var constructor = type.GetConstructor(new[] {typeof(Mobile), typeof(Item)});

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