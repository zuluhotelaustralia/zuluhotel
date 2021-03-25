using System;
using System.Collections.Generic;
using Server;
using Server.Misc;
using Server.Items;
using static Server.Mobiles.CreatureProp;
using Server.Engines.Magic;
using Server.Engines.Harvest;

namespace Server.Mobiles
{
    public class FlamingGargoyle : BaseCreature
    {
        static FlamingGargoyle()
        {
            CreatureProperties.Register<FlamingGargoyle>(new CreatureProperties
            {
                // cast_pct = 30,
                // DataElementId = flamegargoyle,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = flamegargoyle,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x177 /* Weapon */,
                // hostile = 1,
                LootTable = "38",
                LootItemChance = 60,
                LootItemLevel = 2,
                // MissSound = 0x239 /* Weapon */,
                // num_casts = 8,
                // script = spellkillpcs,
                // speech = 54,
                // Speed = 35 /* Weapon */,
                // spell = flamestrike,
                // spell_0 = fireball,
                // spell_1 = meteorswarm,
                // spell_2 = magicarrow,
                // TrueColor = 232,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysMurderer = true,
                BaseSoundID = 372,
                Body = 0x4,
                CorpseNameOverride = "corpse of a flaming gargoyle",
                CreatureType = CreatureType.Gargoyle,
                DamageMax = 64,
                DamageMin = 8,
                Dex = 95,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                HitsMax = 250,
                Hue = 232,
                Int = 335,
                ManaMaxSeed = 100,
                Name = "a flaming gargoyle",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Third.FireballSpell),
                    typeof(Spells.First.MagicArrowSpell)
                },
                ProvokeSkillOverride = 105,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Fire, 100},
                    {ElementalType.PermMagicImmunity, 3}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 80},
                    {SkillName.Tactics, 90},
                    {SkillName.Macing, 150},
                    {SkillName.Magery, 120}
                },
                StamMaxSeed = 75,
                Str = 250,
                VirtualArmor = 35
            });
        }


        [Constructible]
        public FlamingGargoyle() : base(CreatureProperties.Get<FlamingGargoyle>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Flame Gargoyle Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x177,
                MissSound = 0x239
            });
        }

        [Constructible]
        public FlamingGargoyle(Serial serial) : base(serial)
        {
        }


        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int) 0);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);
            var version = reader.ReadInt();
        }
    }
}