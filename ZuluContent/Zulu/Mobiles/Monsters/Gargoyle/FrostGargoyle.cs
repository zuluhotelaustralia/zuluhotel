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
    public class FrostGargoyle : BaseCreature
    {
        static FrostGargoyle()
        {
            CreatureProperties.Register<FrostGargoyle>(new CreatureProperties
            {
                // cast_pct = 25,
                // CProp_PermMagicImmunity = i3,
                // DataElementId = frostgargoyle,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = frostgargoyle,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x177 /* Weapon */,
                // hostile = 1,
                // lootgroup = 38,
                // MagicItemChance = 25,
                // MagicItemLevel = 2,
                // MissSound = 0x239 /* Weapon */,
                // num_casts = 5,
                // script = spellkillpcs,
                // speech = 54,
                // Speed = 35 /* Weapon */,
                // spell = paralyze,
                // spell_0 = lightning,
                // spell_1 = curse,
                // spell_2 = weaken,
                // spell_3 = ebolt,
                // TrueColor = 1154,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysMurderer = true,
                BaseSoundID = 372,
                Body = 0x4,
                CorpseNameOverride = "corpse of a frost gargoyle",
                CreatureType = CreatureType.Gargoyle,
                DamageMax = 64,
                DamageMin = 8,
                Dex = 95,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                HitsMax = 250,
                Hue = 1154,
                Int = 285,
                ManaMaxSeed = 85,
                Name = "a frost gargoyle",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Fifth.ParalyzeSpell),
                    typeof(Spells.Fourth.LightningSpell),
                    typeof(Spells.Fourth.CurseSpell),
                    typeof(Spells.First.WeakenSpell),
                    typeof(Spells.Sixth.EnergyBoltSpell)
                },
                ProvokeSkillOverride = 105,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Water, 75}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 80},
                    {SkillName.Tactics, 120},
                    {SkillName.Macing, 135},
                    {SkillName.Magery, 100}
                },
                StamMaxSeed = 75,
                Str = 250,
                VirtualArmor = 35
            });
        }


        [Constructible]
        public FrostGargoyle() : base(CreatureProperties.Get<FrostGargoyle>())
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
        public FrostGargoyle(Serial serial) : base(serial)
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